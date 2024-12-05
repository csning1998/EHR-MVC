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

        const modifyButton = document.createElement('button');
        modifyButton.type = 'button';
        modifyButton.className = 'btn btn-warning';
        modifyButton.textContent = 'Modify';
        modifyButton.addEventListener('click', () => showModifyingList(patient));

        const deleteButton = document.createElement('button');
        deleteButton.type = 'button';
        deleteButton.className = 'btn btn-danger';
        deleteButton.textContent = 'Delete';
        deleteButton.addEventListener('click', () => Delete(patient.patientId));

        actionCell.appendChild(modifyButton);
        actionCell.appendChild(deleteButton);

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
    document.getElementById('editForm').style.display = "block"; 
    document.getElementById('submitButton').style.display = "block";

    document.getElementById("PatientID").value = patient.patientId;
    document.getElementById("FamilyName").value = patient.familyName || '';
    document.getElementById("GivenName").value = patient.givenName || '';
    document.getElementById("IdNo").value = patient.idNo || '';
    document.getElementById("Birthday").value = new Date(patient.birthday).toISOString().split('T')[0];
    document.getElementById("Gender").value = patient.gender || '';
    document.getElementById("Address").value = patient.address || '';
    document.getElementById("Telecom").value = patient.telecom || '';

    console.log("patient", patient)
}

function Delete(patientId) {
    console.log(`Delete patient with ID: ${patientId}`);
}
