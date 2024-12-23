// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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
        // Save theme to LocalStorage
        localStorage.setItem("theme", newTheme);

        setTimeout(() => {
            html.classList.remove("theme-transition");
        }, 1000);
    });
});

/**
 * Replace 'alert' globally
 * @param {string} message
 */
function customAlert(message) {
    const modal = document.getElementById('customAlertModal');
    const messageElement = document.getElementById('customAlertMessage');
    messageElement.textContent = message;

    modal.style.zIndex = '9999';

    const bootstrapModal = new bootstrap.Modal(modal);
    bootstrapModal.show();

    modal.addEventListener('hidden.bs.modal', function () {
        modal.style.zIndex = '';
    });
}

window.alert = customAlert;

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
