 
const auth = [
    { id: 1, username: "user1", password: "123456" },
    { id: 2, username: "user2", password: "123456" },
    { id: 3, username: "user3", password: "123456" },
    { id: 4, username: "user4", password: "123456" },
]  

const localstorage =  JSON.parse(localStorage.getItem('auth')) 

 
$("#login").click(() => {
  const email = $("#email").val();
  const password = $("#password").val();
  const data = {
    email,
    password,
  };
  console.log(auth)

  let isLocalStorageValid 
  if(data.email === localstorage.email && data.password === localstorage.password) {
    isLocalStorageValid = true
  }

  const user = auth.find(
    (user) => user.username === email && user.password === password
  );

  if (isLocalStorageValid || user) {
   
    window.location.replace("../admin/admin.html");
    alert("Successful login");
  } else {
    console.log("Invalid email or password");
    alert("Invalid email or password");
  }
});
