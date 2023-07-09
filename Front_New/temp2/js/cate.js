function addToCart(itemName, itemPrice, itemImage) {
    // Retrieve the cart data from localStorage or initialize an empty array
    var cart = JSON.parse(localStorage.getItem('cart')) || [];

    // Create an object representing the selected item
    var item = {
      name: itemName,
      price: itemPrice,
      image: itemImage
    };

    // Add the item to the cart
    cart.push(item);

    // Store the updated cart data in localStorage
    localStorage.setItem('cart', JSON.stringify(cart));

    // Optionally, you can redirect the user to the cart page
    // window.location.href = './cart.html';
  }

// Assuming the API endpoint is 'https://api.example.com/products'

// Function to fetch and process the product data
function fetchProducts() {
  fetch('https://api.example.com/products')
    .then(response => response.json())
    .then(data => {
      // Process the received product data
      const productContainer = document.getElementById('product');

      // Loop through the products and generate HTML dynamically
      data.forEach(product => {
        const box = document.createElement('div');
        box.className = 'box';

        const img = document.createElement('img');
        img.src = product.image;
        img.className = 'responsive';

        const h3 = document.createElement('h3');
        h3.textContent = product.name;

        const price = document.createElement('div');
        price.className = 'price';
        price.textContent = product.price;

        const addToCartBtn = document.createElement('a');
        addToCartBtn.className = 'btn';
        addToCartBtn.textContent = 'Add To Cart';
        addToCartBtn.href = '#';
        addToCartBtn.onclick = () => addToCart(product.name, product.price, product.image);

        box.appendChild(img);
        box.appendChild(h3);
        box.appendChild(price);
        box.appendChild(addToCartBtn);

        productContainer.appendChild(box);
      });
    })
    .catch(error => {
      // Handle any errors
      console.error('Error:', error);
    });
}

// Call the fetchProducts function to retrieve and process the product data
fetchProducts();
