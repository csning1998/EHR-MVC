function AuthenticatedRequest() {
    const token = localStorage.getItem("jwtToken");
    if (!token) {
        alert("Permission Denied");
        window.location.href = "/User/Login";
        return;
    }

    $.ajax({
        url: '/',
        method: 'GET',
        headers: {
            "Authorization": `Bearer ${token}`
        },
        success: function (result) {
            console.log("Authorized request successful:", result);
        },
        error: function (err) {
            alert("Permission Denied");
            window.location.href = "/User/Login";
        }
    });
}
