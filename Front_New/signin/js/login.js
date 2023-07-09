const baseURL = 'https://localhost:44350/'


$("#login").click(() => {
    const email = $("#email").val();
    const password = $("#password").val();
    const data = {
        email,
        password
    }
    console.log({ data });
    axios({
        method: 'post',
        url: `${baseURL}/api/Authenticate/login`,
        data: data,
        headers: { 'Content-Type': 'application/json; charset=UTF-8' }
    }).then(function (response) {
        console.log({ response });
        const { title, token } = response.data
        if (title === "") {
            localStorage.setItem('token', token);
            window.location.href = '../admin/admin.html';
        } else {
            console.log("In-valid email or password");
            alert("In-valid email or password")
        }
    }).catch(function (error) {
        console.log(error);
    });
})

 