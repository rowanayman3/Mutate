const baseURL = "https://crud-z6zs.onrender.com";

$("#login").click(() => {
  const email = $("#email").val();
  const password = $("#password").val();
  const data = {
    email,
    password,
  };
  console.log({ data });
  axios({
    method: "post",
    url: `${baseURL}/auth/login`,
    data: data,
    headers: { "Content-Type": "application/json; charset=UTF-8" },
  })
    .then(function (response) {
      console.log({ response });
      const { message, token } = response.data;
      console.log(token)
      if (message == "Done") {
        localStorage.setItem("token", token);
        window.location.href = "index.html";
      } else {
        console.log("In-valid email or password");
        alert("In-valid email or password");
      }
    })
    .catch(function (error) {
      console.log(error);
    });
});
