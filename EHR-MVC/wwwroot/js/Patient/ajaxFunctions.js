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

function modifyPatientData() {
    console.log("currentPatient", currentPatient)
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
            console.log("AJAX call successful");
            console.log("Result:", result);

            alert("Patient data saved successfully.");
        },
        error: function (err) {
            console.error("Error:", err);
            alert(`Failed to save patient data: ${err.responseText}`);
        }
    });
}
