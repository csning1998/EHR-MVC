function submitPatientData() {
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

function queryPatientData() {
    $.ajax({
        url: '/Patient/Query',
        method: 'GET',
        dataType: 'json',
        data: {
            patientId: document.getElementById("SearchPatientId").value,
            idNo: document.getElementById("SearchIdNo").value,
            familyName: document.getElementById("SearchFamilyName").value,
            givenName: document.getElementById("SearchGivenName").value
        },

        success: function (result) {
            if (result.status !== 'error') {
                alert("Successfully inquired.");
                if (result.length > 0) {
                    document.getElementById("patientList").style.display = "block";
                    document.getElementById("noData").style.display = "none";
                    searchList(result);
                } else {
                    document.getElementById("patientList").style.display = "none";
                    document.getElementById("noData").style.display = "block";
                }
                return;
            }
            alert(result.message);
        },
        error: function (err) {
            console.error(`Error ${err.status}: ${err.statusText}`);
            alert(err.statusText || "An unexpected error occurred.");
        }
    });
}
