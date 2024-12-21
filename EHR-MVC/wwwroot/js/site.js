// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
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
