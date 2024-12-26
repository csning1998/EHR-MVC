document.addEventListener("DOMContentLoaded", () => {
    const userActions = document.getElementById("userActions");
    const token = localStorage.getItem("jwtToken");

    if (token) {
        userActions.innerHTML = `
                <p class="lead">Welcome back, <strong>User</strong>!</p>
                <a href="#" class="btn btn-warning btn-lg" id="logoutButton">Logout</a>
            `;
    }
});

/**
 * Logout function to clear token and reload page
 */
function logout() {
    console.log("logout")
    localStorage.removeItem("jwtToken");
    window.location.reload();
}

// Example: Bind logout to dynamically created button
document.addEventListener("DOMContentLoaded", () => {
    const logoutButton = document.getElementById("logoutButton");
    if (logoutButton) {
        logoutButton.addEventListener("click", logout);
    }
});
