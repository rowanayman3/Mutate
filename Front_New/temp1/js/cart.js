// Get the cart items from local storage
var cartItems = JSON.parse(localStorage.getItem('cartItems')) || [];

// Create a function to generate the cart item HTML
function generateCartItemHTML(item, index) {
  return `
    <div class="cart-item">
      <img src="${item.src}" alt="Product Image" class="cart-item-image" />
      <div class="item-details">
        <div class="item-name">${item.name}</div>
        <div class="item-price">${item.price}</div>
      </div>
      <button class="btn btn-danger btn-sm remove-item-btn" data-index="${index}">
        Remove Item
      </button>
    </div>
  `;
}

// Function to render the cart items
function renderCartItems() {
  var cartItemsContainer = document.getElementById('cart-items');
  cartItemsContainer.innerHTML = ''; // Clear the cart items container
  for (var i = 0; i < cartItems.length; i++) {
    var cartItemHTML = generateCartItemHTML(cartItems[i], i);
    cartItemsContainer.innerHTML += cartItemHTML;
  }
}

// Function to update the cart item count
function updateCartItemCount() {
  var cartItemCountElement = document.getElementById('cart-item-count');
  cartItemCountElement.innerText = cartItems.length;
}

// Function to update the total price display
function updateTotalPrice() {
  var totalPriceElement = document.createElement('div');
  totalPriceElement.classList.add('total-price');
  var totalPrice = 0;
  for (var i = 0; i < cartItems.length; i++) {
    totalPrice += parseFloat(cartItems[i].price);
  }
  totalPriceElement.innerText = 'Total Price: ' + totalPrice.toFixed(2) + ' EGP';

  var cartItemsContainer = document.getElementById('cart-items');
  cartItemsContainer.appendChild(totalPriceElement);
}

// Function to handle removing item from the cart
function removeCartItem(index) {
  if (index !== undefined) {
    cartItems.splice(index, 1);
    localStorage.setItem('cartItems', JSON.stringify(cartItems));
    renderCartItems();
    updateCartItemCount();
    updateTotalPrice();
  }
}

// Insert the selected items into the cart
if (cartItems.length > 0) {
  renderCartItems();
  updateCartItemCount();
  updateTotalPrice();
}

// Add event listener using event delegation for remove item buttons
document.addEventListener('click', function(event) {
  if (event.target && event.target.classList.contains('remove-item-btn')) {
    var index = event.target.dataset.index;
    removeCartItem(index);
  }
});