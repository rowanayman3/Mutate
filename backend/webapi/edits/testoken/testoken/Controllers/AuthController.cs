using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using testoken.Data;
using testoken.Models;

namespace testoken.Controllers
{
    public class AuthController : Controller
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly DataContext _context;
        private readonly IEmailService _emailService;

        public AuthController(IConfiguration configuration , IUserService userService,
            DataContext context , IEmailService emailService)
        {
            _configuration = configuration;
            _userService = userService;
            _context = context;
            _emailService = emailService;
        }

      
         // claims retrive in controller 
        [HttpGet, Authorize , Route ("getinfo")]

        public ActionResult<object> GetInfo()
        {
            // this using service created 
            var UserName = _userService.getName();
            var UserRole = _userService.getRole();

            return Ok(new {UserName , UserRole}); 

            //var Username = User?.Identity?.Name;
            //var claimName = User.FindFirstValue(ClaimTypes.Name);
            //var claimRole = User.FindFirstValue(ClaimTypes.Role);

            //return Ok(new { Username, claimName, claimRole });

        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody]UserRegisterRequest request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest("Wrong Uasge");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash= passwordHash,
                PasswordSalt= passwordSalt,
                
            };
            string verifytoken = CreateRandomToken();
            user.VerificationToken = verifytoken;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            string verificationLink = $"https://example.com/verify?token={verifytoken}";
            string emailBody = $"Please click the following link to verify your account:" +
                $" <a href='{verificationLink}'>{verificationLink}</a>";

            var emailsender = new EmailDto
            {
                To = user.Email,
                Body= emailBody,
                Subject = "Account Verify Token",
            };

            _emailService.SendEmail(emailsender);


            //user.Username = request.Username;
            //user.PasswordHash = passwordHash;
            //user.PasswordSalt = passwordSalt;

            return Ok("go check your mail");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]UserLoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return BadRequest("enter correct user");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }
             
            if (user.VerifyAt == null) 
            {
                return BadRequest("not verified");
            }

            //if you want verify login with 
            //if (user.Username != request.username)
            //{
            //    return BadRequest("User not found.");
            //}

            string token = CreateToken(user);

            var refreshToken = GenerateRefresherToken();
            SetRefreshToken(refreshToken);

            Dictionary<string,string> response = new Dictionary<string,string>();
            response.Add("token", token);
            response.Add("refreshToken", refreshToken.Token);
            return Ok(response);

        }

        [HttpPost("verify")]
        public async Task<ActionResult> Verify(string TToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == TToken);

            if (user == null)
            {
                return BadRequest("not good token");
            }

            user.VerifyAt= DateTime.Now;
            await _context.SaveChangesAsync();
            

            return Ok("you can login now");
        }

        [HttpPost("forgetPass")]
        public async Task<ActionResult> ForgetPass(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email  == email);

            if (user == null)
            {
                return BadRequest("not good user");
            }

            string passres = CreateRandomToken();
            user.PasswordResetToken = passres;
            user.PasswordResetExpire = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            
            string emailBody = $"token = {passres}";


            var emailsender = new EmailDto
            {
                To= user.Email,
                Subject= "mail reset token",
                Body = emailBody 

            };

            _emailService.SendEmail(emailsender);

            return Ok("you can change pass");
        }

        [HttpPost("resetPass")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);

            if (user == null || user.PasswordResetExpire < DateTime.Now)
            {
                return BadRequest("not good user");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash= passwordHash;
            user.PasswordSalt= passwordSalt;
            user.PasswordResetToken = null;
            user.PasswordResetExpire = null ;

            await _context.SaveChangesAsync();

            return Ok("password changed");
        }

        [HttpPost("new-token")]

        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (!user.NewToken.Equals(refreshToken))
            {
                return Unauthorized("something milucious");
            }
            else if (user.TokenEnd < DateTime.Now) {
                return Unauthorized("token end");
            }
            string token = CreateToken(user);
            var newRefreshToken = GenerateRefresherToken();
            SetRefreshToken(newRefreshToken);
            return Ok(token);

        }    

        // new token is from class 
        private NewToken GenerateRefresherToken()
        {
            var newToken = new NewToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                End = DateTime.Now.AddDays(3),
                Start = DateTime.Now
            };
            return newToken;

        }
        private void SetRefreshToken(NewToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly= true,
                Expires = newRefreshToken.End

            };
            Response.Cookies.Append("refreshToken",newRefreshToken.Token,cookieOptions);
            user.NewToken = newRefreshToken.Token;
            user.TokenEnd = newRefreshToken.End;
            user.TokenStart = newRefreshToken.Start;

        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Creator")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        [HttpPost("sendmail")]
        public IActionResult SemdEmail(EmailDto request)
        {
            _emailService.SendEmail(request);

            return Ok();


        }

    }
}
