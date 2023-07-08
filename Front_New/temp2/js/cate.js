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