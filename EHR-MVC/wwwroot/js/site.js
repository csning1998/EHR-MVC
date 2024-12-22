// Toggle Theme
document.addEventListener("DOMContentLoaded", () => {
    const html = document.getElementById("htmlPage");
    const checkbox = document.getElementById("checkbox");

    // Load theme from LocalStorage or set to default
    const savedTheme = localStorage.getItem("theme") || "light";
    html.setAttribute("data-bs-theme", savedTheme);
    checkbox.checked = savedTheme === "dark";

    // Theme toggle logic
    checkbox.addEventListener("change", () => {
        const newTheme = checkbox.checked ? "dark" : "light";
        html.setAttribute("data-bs-theme", newTheme);
        localStorage.setItem("theme", newTheme); // Save theme to LocalStorage
    });
});


// Json Web Token0
function checkToken() {
    const token = localStorage.getItem("jwtToken");
    console.log("Token is valid: Bearer " + token);
}

function setAjaxAuthorizationHeader() {
    const token = localStorage.getItem("jwtToken");
    if (token) {
        $.ajaxSetup({
            beforeSend: function (xhr) {
                const token = localStorage.getItem("jwtToken");
                if (token) {
                    xhr.setRequestHeader("Authorization", "Bearer " + token);
                }
            }
        });
        console.log("Ajax Authorization Header set: Bearer " + token);
    }
}

$(document).ready(function () {
    checkToken();
    setAjaxAuthorizationHeader();
});

