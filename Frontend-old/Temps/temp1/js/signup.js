


const baseURL = "https://crud-z6zs.onrender.com";

function isPasswordStrong(password) {
  if (password.length < 8) return false;
  if (!/[A-Z]/.test(password)) return false;
  if (!/[a-z]/.test(password)) return false;
  if (!/[0-9]/.test(password)) return false;
  if (!/[$@$!%*?&]/.test(password)) return false;
  return true;
}

$("#password").on("input", function() {
  const password = $(this).val();
  const strengthIndicator = $("#passwordStrength");

  if (isPasswordStrong(password)) {
    strengthIndicator.text("Strong");
    strengthIndicator.css("color", "green");
  } else {
    strengthIndicator.text("Weak");
    strengthIndicator.css("color", "red");
  }
});

$("#signUp").click(() => {
  const userName = $("#userName").val();
  const email = $("#email").val();
  const password = $("#password").val();
  const cPassword = $("#cPassword").val();

  // Perform the password strength check
  if (!isPasswordStrong(password)) {
    alert("Password is not strong enough.");
    return;
  }

  const data = {
    userName,
    email,
    password,
    cPassword,
  };
  console.log({ data });


  $("#SocialLogin").click(() => {
    axios({
      method: "get",
      url: `http://localhost:5000/google`,
      headers: { "Content-Type": "application/json; charset=UTF-8" },
    })
      .then(function (response) {
        console.log({ response });
      })
      .catch(function (error) {
        console.log(error);
      });
  });

  axios({
    method: "post",
    url: `${baseURL}/auth/signup`,
    data: data,
    headers: { "Content-Type": "application/json; charset=UTF-8" },
  })
    .then(function (response) {
      console.log({ response });
      const { message } = response.data;
      if (message == "Done") {
        window.location.href = "login.html";
      } else if (message == "Email exist") {
        alert("Email exist");
      } else {
        console.log("Fail to signup");
        alert(message);
      }
    })
    .catch(function (error) {
      console.log(error);
    });
});
