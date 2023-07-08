const baseURL = "https://mutate.com";
$("#signUp").click(() => {
  const userName = $("#userName").val();
  const email = $("#email").val();
  const password = $("#password").val();
  const cPassword = $("#cPassword").val();
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
      url: ``,
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
