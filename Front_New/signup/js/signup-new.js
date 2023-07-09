$("#signUp").click(() => {
    const userName = $("#userName").val();
    const email = $("#email").val();
    const password = $("#password").val();
    const cPassword = $("#cPassword").val();
  
    // Create an object with the user data
    const userData = {
      userName,
      email,
      password,
      cPassword
    }
  
    // Store the user data in local storage
    localStorage.setItem('auth', JSON.stringify(userData));
  
    // Redirect to the sign-in page
    window.location.replace('../signin/signin.html');
    alert("Successful sign-up");
  });
  