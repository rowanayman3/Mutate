// Get references to the buttons and hidden sections
const showButtons = document.querySelectorAll(".showButton");
const sections = document.querySelectorAll(".hidden-section");

// Add event listeners to the buttons
showButtons.forEach(function(button) {
  button.addEventListener("click", function() {
    // Get the target section based on the data-target attribute
    const targetSectionId = button.getAttribute("data-target");
    const targetSection = document.getElementById(targetSectionId);

    // Hide all sections
    sections.forEach(function(section) {
      section.style.display = "none";
    });

    // Show the target section
    targetSection.style.display = "block";
  });
});
const localstorage = JSON.parse(localStorage.getItem('auth'));

const users = [
  { Id:1, name:'islam', userName: "user1", password: "123456", email:'user1@gmail.com' },
  { Id:2, name:'Mohamed', userName: "user2", password: "123456", email:'user2@gmail.com' },
  { Id:3, name:'Yassen', userName: "user3", password: "123456", email:'user3@gmail.com' },
  { Id:4, name:'Hamada', userName: "user4", password: "123456", email:'user4@gmail.com' },
  localstorage
];

const showUsers = () => {
  const usersList = document.getElementById('usersList');
  usersList.innerHTML = '';
  console.log(localstorage)
 
  users.forEach(user => {
    const li = document.createElement('li');
    li.innerHTML = `
      <span>Name: ${user.name || user.userName}, Email: ${user.email}, Username: ${user.userName}</span>
      <input type="text" id="nameInput-${user.Id}" value="${user.name}">
      <input type="text" id="emailInput-${user.Id}" value="${user.email}">
      <input type="text" id="usernameInput-${user.Id}" value="${user.userName}">
      <button onclick="editUser(${user.Id})">Edit</button>
    `;
    usersList.appendChild(li);
  });
};

showUsers()

// Fetch and display all users
// function showUsers() {
//     fetch('api/users')
//       .then(response => response.json())
//       .then(users => {
//         const usersList = document.getElementById('usersList');
//         usersList.innerHTML = '';
//         users.forEach(user => {
//           const li = document.createElement('li');
//           li.innerHTML = `
//             <span>Name: ${user.Name}, Email: ${user.Email}, Username: ${user.Username}</span>
//             <input type="text" id="nameInput-${user.Id}" value="${user.Name}">
//             <input type="text" id="emailInput-${user.Id}" value="${user.Email}">
//             <input type="text" id="usernameInput-${user.Id}" value="${user.Username}">
//             <button onclick="editUser(${user.Id})">Edit</button>
//           `;
//           usersList.appendChild(li);
//         });
//       })
//       .catch(error => {
//         console.error('Error fetching users:', error);
//       });
//   }
  // Edit user
function editUser(userId) {
    const nameInput = document.getElementById(`nameInput-${userId}`);
    const emailInput = document.getElementById(`emailInput-${userId}`);
    const usernameInput = document.getElementById(`usernameInput-${userId}`);
  
    const updatedUser = {
      Name: nameInput.value,
      Email: emailInput.value,
      Username: usernameInput.value
    };
  
    fetch(`api/users/${userId}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(updatedUser)
    })
      .then(response => {
        if (response.ok) {
          console.log('User updated successfully');
        } else {
          console.error('Error updating user:', response.status);
        }
      })
      .catch(error => {
        console.error('Error updating user:', error);
      });
  }

  // Create a new user
function createUser() {
  const nameInput = document.getElementById('newUserName');
  const emailInput = document.getElementById('newUserEmail');
  const passwordInput = document.getElementById('newUserPassword');
  const usernameInput = document.getElementById('newUserUsername');

  const newUser = {
    Name: nameInput.value,
    Email: emailInput.value,
    Password: passwordInput.value,
    Username: usernameInput.value
  };

  fetch('https://your-api-endpoint.com/api/users', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(newUser)
  })
    .then(response => {
      if (response.ok) {
        console.log('User created successfully');
        // Optionally, you can reset the form fields
        nameInput.value = '';
        emailInput.value = '';
        passwordInput.value = '';
        usernameInput.value = '';
      } else {
        console.error('Error creating user:', response.status);
      }
    })
    .catch(error => {
      console.error('Error creating user:', error);
    });
}

// Fetch and display all categories
function showCategories() {
  fetch('api/categories') 
    .then(response => response.json())
    .then(categories => {
      const categoriesList = document.getElementById('categoriesList');
      categoriesList.innerHTML = '';
      categories.forEach(category => {
        const li = document.createElement('li');
        li.textContent = `Name: ${category.CategoryName}, Description: ${category.Description}`;
        categoriesList.appendChild(li);
      });
    })
    .catch(error => {
      console.error('Error fetching categories:', error);
    });
}

// Edit category
function editCategory(categoryId) {
    const categoryNameInput = document.getElementById(`categoryNameInput-${categoryId}`);
    const categoryDescriptionInput = document.getElementById(`categoryDescriptionInput-${categoryId}`);
  
    const updatedCategory = {
      CategoryName: categoryNameInput.value,
      Description: categoryDescriptionInput.value
    };
  
    fetch(`api/categories/${categoryId}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(updatedCategory)
    })
      .then(response => {
        if (response.ok) {
          console.log('Category updated successfully');
        } else {
          console.error('Error updating category:', response.status);
        }
      })
      .catch(error => {
        console.error('Error updating category:', error);
      });
  }
  
  // Create a new category
function createCategory() {
  const nameInput = document.getElementById('newCategoryName');
  const descriptionInput = document.getElementById('newCategoryDescription');

  const newCategory = {
    CategoryName: nameInput.value,
    Description: descriptionInput.value
  };

  fetch('https://your-api-endpoint.com/api/categories', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(newCategory)
  })
    .then(response => {
      if (response.ok) {
        console.log('Category created successfully');
        // Optionally, you can reset the form fields
        nameInput.value = '';
        descriptionInput.value = '';
      } else {
        console.error('Error creating category:', response.status);
      }
    })
    .catch(error => {
      console.error('Error creating category:', error);
    });
}


// Fetch and display all products
function showProducts() {
  fetch('api/products') 
    .then(response => response.json())
    .then(products => {
      const productsList = document.getElementById('productsList');
      productsList.innerHTML = '';
      products.forEach(product => {
        const li = document.createElement('li');
        li.textContent = `Name: ${product.Name}, Description: ${product.Description}, Price: ${product.price}`;
        productsList.appendChild(li);
      });
    })
    .catch(error => {
      console.error('Error fetching products:', error);
    });
}

// Edit product
function editProduct(productId) {
    const nameInput = document.getElementById(`nameInput-${productId}`);
    const descriptionInput = document.getElementById(`descriptionInput-${productId}`);
    const priceInput = document.getElementById(`priceInput-${productId}`);
  
    const updatedProduct = {
      Name: nameInput.value,
      Description: descriptionInput.value,
      price: priceInput.value
    };
  
    fetch(`api/products/${productId}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(updatedProduct)
    })
      .then(response => {
        if (response.ok) {
          console.log('Product updated successfully');
        } else {
          console.error('Error updating product:', response.status);
        }
      })
      .catch(error => {
        console.error('Error updating product:', error);
      });
  }
  // Create a new product
function createProduct() {
  const nameInput = document.getElementById('newProductName');
  const descriptionInput = document.getElementById('newProductDescription');
  const priceInput = document.getElementById('newProductPrice');

  const newProduct = {
    Name: nameInput.value,
    Description: descriptionInput.value,
    price: parseFloat(priceInput.value)
  };

  fetch('https://your-api-endpoint.com/api/products', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(newProduct)
  })
    .then(response => {
      if (response.ok) {
        console.log('Product created successfully');
        // Optionally, you can reset the form fields
        nameInput.value = '';
        descriptionInput.value = '';
        priceInput.value = '';
      } else {
        console.error('Error creating product:', response.status);
      }
    })
    .catch(error => {
      console.error('Error creating product:', error);
    });
}


  // Open User Modal
function openUserModal() {
  const userModal = document.getElementById('userModal');
  userModal.style.display = 'block';
}

// Close User Modal
function closeUserModal() {
  const userModal = document.getElementById('userModal');
  userModal.style.display = 'none';
}

// Open Product Modal
function openProductModal() {
  const productModal = document.getElementById('productModal');
  productModal.style.display = 'block';
}

// Close Product Modal
function closeProductModal() {
  const productModal = document.getElementById('productModal');
  productModal.style.display = 'none';
}

// Open Category Modal
function openCategoryModal() {
  const categoryModal = document.getElementById('categoryModal');
  categoryModal.style.display = 'block';
}

// Close Category Modal
function closeCategoryModal() {
  const categoryModal = document.getElementById('categoryModal');
  categoryModal.style.display = 'none';
}

// Close Modal When Clicked Outside
window.onclick = function(event) {
  const userModal = document.getElementById('userModal');
  const productModal = document.getElementById('productModal');
  const categoryModal = document.getElementById('categoryModal');
  if (event.target == userModal) {
    userModal.style.display = 'none';
  } else if (event.target == productModal) {
    productModal.style.display = 'none';
  } else if (event.target == categoryModal) {
    categoryModal.style.display = 'none';
  }
};

  