function register() {
    let formData = {
        FamilyName: document.getElementById("FamilyName").value,
        GivenName: document.getElementById("GivenName").value,
        UserEmail: document.getElementById("UserEmail").value,
        Password: document.getElementById("Password").value,
        ConfirmPassword: document.getElementById("ConfirmPassword").value
    };

    if (!formData.FamilyName || !formData.GivenName || !formData.UserEmail || !formData.Password || !formData.ConfirmPassword) {
        alert("All fields are required!");
        return;
    }

    $.ajax({
        url: '/User/Register',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData),
        success: function (result) {
            if (result > 0) {
                alert("Successfully Registered!");
            } else if (result.status === "Error") {
                alert(result.error)
            } else {
                alert(result.error)
            }
        },
        error: function (err) {
            alert("Error: " + (err.responseJSON?.message || err.responseText || "Unknown error"));
        }
    });
}

function login() {
    let formData = {
        UserEmail: document.getElementById("UserEmail").value,
        Password: document.getElementById("Password").value,
    }
    if (!formData.UserEmail || !formData.Password) {
        alert("All fields are required!");
        return;
    }

    $.ajax({
        url: '/User/Login',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData),
        success: function (result) {
            if (result > 0) {
                alert("Successfully Login!");
                window.location.href = "/Home/Index";
            } else if (result.status === "Error") {
                alert(result.message || "Failed to login.");
            } else {
                alert(result.error)
            }
        },
        error: function (err) {
            alert("Error: " + (err.responseJSON?.message || err.responseText || "Unknown error"));
        }
    });

}