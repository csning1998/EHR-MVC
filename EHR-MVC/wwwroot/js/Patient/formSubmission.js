function Submit() {
    const isTelecomValid = regexValidator('Telecom');
    const isIdNoValid = regexValidator('IdNo');
    const isBirthdayValid = regexValidator('Birthday');

    if (!isTelecomValid || !isIdNoValid || !isBirthdayValid) {
        alert("Please correct the highlighted fields before submitting the form.");
        return;
    }
    
    const formData = {
        IdNo: sanitizeInput(document.getElementById("IdNo").value),
        Active: true,
        FamilyName: sanitizeInput(document.getElementById("FamilyName").value, true),
        GivenName: sanitizeInput(document.getElementById("GivenName").value, true),
        Telecom: sanitizeInput(document.getElementById("Telecom").value),
        Gender: sanitizeInput(document.getElementById("Gender").value),
        Birthday: sanitizeInput(document.getElementById("Birthday").value),
        Address: sanitizeInput(document.getElementById("Address").value, true)
    };

    $.ajax({
        url: '/Patient/Save',
        method: 'POST',
        dataType: 'json',
        data: { PatientViewModel: formData },
        success: function (result) {
            if (result > 0) {
                alert("Save Successful");
            } else if (result.status === "Error") {
                alert(result.error);
            } else {
                alert(result.error);
            }
        },
        error: function (err) {
            alert(err.responseText);
        }
    });
}
