// Add event listener to all "Add to Cart" buttons
var addToCartButtons = document.getElementsByClassName('add-to-cart-btn');
for (var i = 0; i < addToCartButtons.length; i++) {
  addToCartButtons[i].addEventListener('click', addToCart);
}

// Function to handle adding item to the cart
function addToCart(event) {
// event.preventDefault(); // Prevent the default link behavior

// Get the product details from the clicked element's parent container
var productContainer = event.target.closest('.col-lg-4');
var productName = productContainer.querySelector('.card-title').textContent;
var productPrice = productContainer.querySelector('.mt-4').textContent;
var productImg = productContainer.querySelector('.productImg').getAttribute('src');

// Create a new cart item object
var cartItem = {
name: productName,
price: productPrice,
src: productImg
};

// Check if cart items exist in the local storage
var cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];

// Add the new item to the cart items array
cartItems.push(cartItem);
alert("Item Added")
// Store the updated cart items back in the local storage
localStorage.setItem('cartItems', JSON.stringify(cartItems));

}
