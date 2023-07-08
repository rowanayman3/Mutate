document.addEventListener("DOMContentLoaded", function() {
    // Retrieve the cart data from localStorage
    var cart = JSON.parse(localStorage.getItem('cart')) || [];

    var cartItemsElement = document.getElementById("cart-items");
    var totalItems = 0;
    var totalPrice = 0;

    // Iterate over each item in the cart and display it in the table
    cart.forEach(function(item, index) {
      var row = document.createElement("tr");

      var imageCell = document.createElement("td");
      var image = document.createElement("img");
      image.src = item.image;
      image.alt = item.name;
      image.style.maxWidth = "100px";
      imageCell.appendChild(image);

      var nameCell = document.createElement("td");
      nameCell.textContent = item.name;

      var priceCell = document.createElement("td");
      priceCell.textContent = item.price;

      var actionCell = document.createElement("td");
      var removeButton = document.createElement("button");
      removeButton.classList.add("btn", "btn-danger", "remove-button"); // Add 'remove-button' class
      removeButton.textContent = "Remove";
      removeButton.addEventListener("click", function() {
        // Remove the item from the cart
        cart.splice(index, 1);
        // Update the cart in localStorage
        localStorage.setItem('cart', JSON.stringify(cart));
        // Re-render the cart items
        renderCartItems();
      });
      actionCell.appendChild(removeButton);

      row.appendChild(imageCell);
      row.appendChild(nameCell);
      row.appendChild(priceCell);
      row.appendChild(actionCell);

      cartItemsElement.appendChild(row);

      totalItems++;
      totalPrice += item.price;
    });

    // Update the total item count
    var itemCountElement = document.getElementById("item-count");
    itemCountElement.textContent = totalItems.toString();

    // Update the total price
    var totalPriceElement = document.getElementById("total-price");
    totalPriceElement.textContent = totalPrice.toString();

    function renderCartItems() {
      cartItemsElement.innerHTML = "";
      totalItems = 0;
      totalPrice = 0;

      cart.forEach(function(item, index) {
        var row = document.createElement("tr");

        var imageCell = document.createElement("td");
        var image = document.createElement("img");
        image.src = item.image;
        image.alt = item.name;
        image.style.maxWidth = "100px";
        imageCell.appendChild(image);

        var nameCell = document.createElement("td");
        nameCell.textContent = item.name;

        var priceCell = document.createElement("td");
        priceCell.textContent = item.price;

        var actionCell = document.createElement("td");
        var removeButton = document.createElement("button");
        removeButton.classList.add("btn", "btn-danger", "remove-button"); // Add 'remove-button' class
        removeButton.textContent = "Remove";
        removeButton.addEventListener("click", function() {
          // Remove the item from the cart
          cart.splice(index, 1);
          // Update the cart in localStorage
          localStorage.setItem('cart', JSON.stringify(cart));
          // Re-render the cart items
          renderCartItems();
        });
        actionCell.appendChild(removeButton);

        row.appendChild(imageCell);
        row.appendChild(nameCell);
        row.appendChild(priceCell);
        row.appendChild(actionCell);

        cartItemsElement.appendChild(row);

        totalItems++;
        totalPrice += item.price;
      });

      itemCountElement.textContent = totalItems.toString();
      totalPriceElement.textContent = totalPrice.toString();
    }
  });