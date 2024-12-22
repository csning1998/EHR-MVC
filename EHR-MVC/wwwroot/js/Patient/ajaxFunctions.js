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
        Address: sanitizeInput(document.getElementById("Address").value, true),
        Email: sanitizeInput(document.getElementById("Email").value),
        PostalCode: sanitizeInput(document.getElementById("PostalCode").value),
        Country: sanitizeInput(document.getElementById("Country").value),
        PreferredLanguage: sanitizeInput(document.getElementById("PreferredLanguage").value),
        EmergencyContactName: sanitizeInput(document.getElementById("EmergencyContactName").value, true),
        EmergencyContactRelationship: sanitizeInput(document.getElementById("EmergencyContactRelationship").value, true),
        EmergencyContactPhone: sanitizeInput(document.getElementById("EmergencyContactPhone").value)
    };

    console.log("formData", formData)

    $.ajax({
        url: '/Patient/Save',
        method: 'POST',
        dataType: 'json',
        data: { PatientViewModel: formData },
        success: function (result, _textStatus, xhr) {
            if (xhr.status === 200) {
                alert(result.Message || "Operation successful.");
            } else if (xhr.status === 400) {
                alert(result.Message || "Bad request. Validation failed or invalid data.");
            } else {
                alert("Unexpected success status: " + xhr.status);
            }
        },
        error: function (xhr) {
            if (xhr.status === 500) {
                const errorResponse = xhr.responseJSON;
                alert(errorResponse?.Message || "Server error occurred.");
            } else if (xhr.status === 400) {
                const errorResponse = xhr.responseJSON;
                alert(errorResponse?.Message || "Bad request: invalid data or validation failed.");
            } else {
                alert("Unexpected error: " + xhr.status + " - " + xhr.statusText);
            }
        }
    });

}

function queryPatientData() {
    console.log("Quering")
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
                const patientList = document.getElementById("patientList");
                if (result.length > 0) {
                    document.getElementById("patientList").style.display = "table";
                    document.getElementById("noData").style.display = "none";
                    searchList(result);
                } else {
                    document.getElementById("patientList").style.display = "none";
                    document.getElementById("noData").style.display = "table";
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

function modifyPatientData() {
    console.log("currentPatient", currentPatient);
    if (!currentPatient) {
        alert("No patient data available for modification.");
        return;
    }

    const updatedPatient = {
        PatientId: document.getElementById("PatientID").value,
        FamilyName: document.getElementById("FamilyName").value,
        GivenName: document.getElementById("GivenName").value,
        IdNo: document.getElementById("IdNo").value,
        Birthday: document.getElementById("Birthday").value,
        Gender: document.getElementById("Gender").value,
        Address: document.getElementById("Address").value,
        Telecom: document.getElementById("Telecom").value,
        Email: document.getElementById("Email").value,
        PostalCode: document.getElementById("PostalCode").value,
        Country: document.getElementById("Country").value,
        PreferredLanguage: document.getElementById("PreferredLanguage").value,
        EmergencyContactName: document.getElementById("EmergencyContactName").value,
        EmergencyContactRelationship: document.getElementById("EmergencyContactRelationship").value,
        EmergencyContactPhone: document.getElementById("EmergencyContactPhone").value
    };

    console.log("Updated Patient Data:", updatedPatient);

    if (!updatedPatient.PatientId) {
        alert("Patient ID is required.");
        return;
    }

    $.ajax({
        url: '/Patient/Save',
        method: 'POST',
        dataType: 'json',
        data: updatedPatient,
        success: function (result) {
            if (result.status === 200) {
                queryPatientData();
                alert("Patient data saved successfully.");
            } else if (result.status === "Error") {
                alert(result.error);
            } else {
                alert(result.message);
            }
        },
        error: function (err) {
            console.error("Error:", err);
            alert(`Failed to save patient data: ${err.responseText}`);
        }
    });
}
