document.addEventListener('DOMContentLoaded', function() {
    // Fetch user data from API
    var xhr = new XMLHttpRequest();
    xhr.open('GET', 'your_api_endpoint', true);
    xhr.onload = function() {
        if (xhr.status === 200) {
            var data = JSON.parse(xhr.responseText);
            // Fill placeholders with fetched data
            document.getElementById('UserName').placeholder = data.name;
            document.getElementById('UserEmail').placeholder = data.email;
            document.getElementById('UseruserName').placeholder = data.username;
            document.getElementById('UserPassword').placeholder = data.password;
        }
    };
    xhr.send();

    // Handle form submission
    document.getElementById('submitBtn').addEventListener('click', function(e) {
        e.preventDefault();
        
        // Retrieve the updated values from inputs
        var updatedData = {
            name: document.getElementById('UserName').value,
            email: document.getElementById('UserEmail').value,
            username: document.getElementById('UseruserName').value,
            password: document.getElementById('UserPassword').value
        };
        
        // Perform API request to update user data
        var xhr = new XMLHttpRequest();
        xhr.open('PUT', 'your_api_endpoint', true);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.onload = function() {
            if (xhr.status === 200) {
                // Handle successful update
                console.log('User data updated successfully!');
            } else {
                // Handle error
                console.log('Error updating user data:', xhr.statusText);
            }
        };
        xhr.send(JSON.stringify(updatedData));
    });
});