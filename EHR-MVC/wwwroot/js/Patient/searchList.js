function searchList(result) {
    const tableBody = document.getElementById("patientList").querySelector("tbody");

    tableBody.innerHTML = "";

    // Clear existing rows
    // while (tableBody.firstChild) {
    //     tableBody.removeChild(tableBody.firstChild);
    // }

    // Populate table with new data
    result.forEach(patient => {
        const row = tableBody.insertRow();

        // Create cells
        const cell1 = row.insertCell(0);
        const cell2 = row.insertCell(1);
        const cell3 = row.insertCell(2);
        const cell4 = row.insertCell(3);
        const cell5 = row.insertCell(4);
        const cell6 = row.insertCell(5);

        // Populate cells
        cell1.innerHTML = `
                <button type="button" class="btn btn-warning" onClick="Update(${patient.patientId})">Modify</button>
                <button type="button" class="btn btn-danger" onClick="Delete(${patient.patientId})">Delete</button>`;
        cell2.innerHTML = patient.patientId;
        cell3.innerHTML = `${patient.familyName} ${patient.givenName}`;
        cell4.innerHTML = (patient.gender === 'M') ? 'Male' : 'Female';
        cell5.innerHTML = patient.idNo;
        cell6.innerHTML = new Date(patient.birthday).toLocaleDateString('zh-tw', {
            year: 'numeric',
            month: '2-digit',
            day: '2-digit'
        });
    });

    $('#patientList').removeClass('d-none');
    $('#noData').addClass('d-none')
};