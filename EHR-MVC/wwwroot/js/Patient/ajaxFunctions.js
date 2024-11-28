function submitPatientData() {
    const isTelecomValid = regexValidator('Telecom');
    const isIdNoValid = regexValidator('IdNo');
    const isBirthdayValid = regexValidator('Birthday');

    if (!isTelecomValid || !isIdNoValid || !isBirthdayValid) {
        alert("Please correct the highlighted fields before submitting the form.");
        return;
    }
    
    const formData = {
        PatientId: sanitizeInput(document.getElementById("PatientID").value),
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
                //alert("Successfully inquired.");
                //if (result.length > 0) {
                //    document.getElementById("patientList").style.display = "block";
                //    document.getElementById("noData").style.display = "none";
                //    searchList(result);
                //} else {
                //    document.getElementById("patientList").style.display = "none";
                //    document.getElementById("noData").style.display = "block";
                //}
                if (result.status != 'error') {
                    if (result.length > 0) {
                        document.getElementById("patientList").style.display = "block";
                        document.getElementById("noData").style.display = "none";
                        alert("Successful");
                        searchList(result);
                    } else {
                        document.getElementById("patientList").style.display = "none";
                        document.getElementById("noData").style.display = "block";
                        alert("No Data is found");
                    }
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

function modifyPatientData(id) {
    $.ajax({
        url: '/Patient/Save',
        method: 'POST',
        dataType: 'json',
        data: { PatientId: id },
        success: function (result) {
            if (result) {
                document.getElementById('editForm').style.display = "block";
                document.getElementById('submitButton').style.display = "block";
                document.getElementById("PatientID").value = result[0].patientId;
                document.getElementById("FamilyName").value = result[0].familyName;
                document.getElementById("GivenName").value = result[0].familyName;
                document.getElementById("IdNo").value = result[0].givenName;
                document.getElementById("Birthday").value = new Date(result[0].Birthday).toLocaleDateString('en-CA');
                document.getElementById("Gender").value = result[0].Gender;
                document.getElementById("Address").value = result[0].Address;
                document.getElementById("Telecom").value = result[0].Telecom;
                return;
            }
            alert(result.message);
        },
        error: function (err) {
            alert(err.responseText);
        }
    });


}