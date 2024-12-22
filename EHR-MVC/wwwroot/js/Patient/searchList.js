function searchList(result) {
    const tableBody = document.getElementById("patientList").querySelector("tbody");
    tableBody.innerHTML = "";

    result.forEach(patient => {
        const row = tableBody.insertRow();

        const actionCell = row.insertCell(0);
        const idCell = row.insertCell(1);
        const nameCell = row.insertCell(2);
        const genderCell = row.insertCell(3);
        const idNoCell = row.insertCell(4);
        const birthdayCell = row.insertCell(5);

        const viewButton = document.createElement('button');
        viewButton.type = 'button';
        viewButton.className = 'btn btn-warning';
        viewButton.textContent = 'View';
        viewButton.addEventListener('click', () => showModifyingList(patient));

        const fhirButton = document.createElement('button');
        fhirButton.type = 'button';
        fhirButton.className = 'btn btn-success';
        fhirButton.textContent = 'FHIR';
        fhirButton.addEventListener('click', () => generateFHIRFormat(patient));

        actionCell.appendChild(viewButton);
        actionCell.appendChild(fhirButton);

        idCell.textContent = patient.patientId;
        nameCell.textContent = `${patient.familyName} ${patient.givenName}`;
        genderCell.textContent = (patient.gender === 'M') ? 'Male' : 'Female';
        idNoCell.textContent = patient.idNo;
        birthdayCell.textContent = new Date(patient.birthday).toLocaleDateString('en-CA');
    });

    document.getElementById('patientList').classList.remove('d-none');
    document.getElementById('noData').classList.add('d-none');
}


let currentPatient = null; // Global variable to store the current patient
function showModifyingList(patient) {
    currentPatient = patient; // Store the patient object

    //document.getElementById('editForm').style.display = "block"; 
    //document.getElementById('submitButton').style.display = "block";

    document.getElementById("PatientID").value = patient.patientId;
    document.getElementById("FamilyName").value = patient.familyName || '';
    document.getElementById("GivenName").value = patient.givenName || '';
    document.getElementById("IdNo").value = patient.idNo || '';
    document.getElementById("Birthday").value = new Date(patient.birthday).toISOString().split('T')[0];
    document.getElementById("Gender").value = patient.gender || '';
    document.getElementById("Address").value = patient.address || '';
    document.getElementById("Telecom").value = patient.telecom || '';
    document.getElementById("Email").value = patient.email || '';
    document.getElementById("PostalCode").value = patient.postalCode || '';
    document.getElementById("Country").value = patient.country || '';
    document.getElementById("PreferredLanguage").value = patient.preferredLanguage || '';
    document.getElementById("EmergencyContactName").value = patient.emergencyContactName || '';
    document.getElementById("EmergencyContactRelationship").value = patient.emergencyContactRelationship || '';
    document.getElementById("EmergencyContactPhone").value = patient.emergencyContactPhone || '';

    console.log("patient", patient)

    setReadonly(true);

    const patientModal = new bootstrap.Modal(document.getElementById('patientModal'));
    patientModal.show();
}

function Delete(patientId) {
    console.log(`Delete patient with ID: ${patientId}`);
}

function setReadonly(isReadonly) {
    const inputs = document.querySelectorAll("#patientModal input, #patientModal textarea, #patientModal select");
    inputs.forEach(input => {
        input.readOnly = isReadonly;
        input.disabled = isReadonly;
    });

    document.getElementById("editButton").classList.toggle("d-none", !isReadonly);
    document.getElementById("saveButton").classList.toggle("d-none", isReadonly);
    document.getElementById("cancelButton").classList.toggle("d-none", isReadonly);
}

function enableEditing() {
    setReadonly(false);
}

function cancelEditing() {
    setReadonly(true);

}

function confirmDelete() {
    if (currentPatient) {
        if (confirm(`Are you sure you want to delete patient ID: ${currentPatient.patientId}?`)) {
            Delete(currentPatient.patientId);
            const patientModal = bootstrap.Modal.getInstance(document.getElementById('patientModal'));
            patientModal.hide();
        }
    } else {
        alert("No patient selected.");
    }
}

function generateFHIRFormat(patient) {
    console.log(`Generating FHIR format for patient ID: ${patient.patientId}`);
    // Simulate FHIR generation logic here
    alert(`FHIR format for patient ID: ${patient.patientId} generated successfully.`);
}
