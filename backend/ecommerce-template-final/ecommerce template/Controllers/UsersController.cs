using ecommerce_template.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ecommerce_template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public Response Register(Users users)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection= new SqlConnection(_configuration.GetConnectionString("EcommerceCS").ToString());
            response=dal.Register(users, connection);
            return response;
        }

        [HttpPost]
        [Route("login")]
        public Response Login(Users users) 
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EcommerceCS").ToString());
            Response response=dal.Login(users, connection);
            return response;
        }

        [HttpPost]
        [Route("viewuser")]
        public Response viewuser(Users users) 
        {
            DAL dAL = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EcommerceCS").ToString());
            Response response = dAL.viewuser(users, connection);
            return response;
        }

        [HttpPost]
        [Route("updateprofile")]
        public Response updateprofile(Users users) 
        {
            DAL dAL=new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EcommerceCS").ToString());
            Response response = dAL.updateprofile(users, connection);
            return response;
        }

        
    }
}
