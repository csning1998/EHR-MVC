function generateRandomId(prefix) {
    return `${prefix}${Date.now()}${Math.random().toString(36).substring(2, 8).toUpperCase()}`;
}

const dummyOutpatients = {
    medicalInstitutions: [
        {
            id: generateRandomId("MI"),
            name: "Royal London Hospital",
            code: "RLH001",
            address: "Whitechapel Rd, London E1 1FR, United Kingdom",
            telecom: "+44 20 7377 7000"
        },
        {
            id: generateRandomId("MI"),
            name: "St. Mary’s Hospital",
            code: "SMH002",
            address: "Praed St, London W2 1NY, United Kingdom",
            telecom: "+44 20 3312 6666"
        },
        {
            id: generateRandomId("MI"),
            name: "Mayo Clinic",
            code: "MAYO003",
            address: "200 First St SW, Rochester, MN 55905, USA",
            telecom: "+1 507-284-2511"
        },
        {
            id: generateRandomId("MI"),
            name: "Johns Hopkins Hospital",
            code: "JHH004",
            address: "1800 Orleans St, Baltimore, MD 21287, USA",
            telecom: "+1 410-955-5000"
        },
        {
            id: generateRandomId("MI"),
            name: "Singapore General Hospital",
            code: "SGH005",
            address: "Outram Rd, Singapore 169608",
            telecom: "+65 6222 3322"
        },
        {
            id: generateRandomId("MI"),
            name: "National Taiwan University Hospital",
            code: "NTUH001",
            address: "No. 7, Chung Shan S. Rd., Taipei City, Taiwan",
            telecom: "+886 2 2312 3456"
        },
        {
            id: generateRandomId("MI"),
            name: "Cathay General Hospital",
            code: "CGH002",
            address: "No. 280, Sec. 4, Renai Rd., Taipei City, Taiwan",
            telecom: "+886 2 2708 2121"
        },
        {
            id: generateRandomId("MI"),
            name: "Shin Kong Wu Ho-Su Memorial Hospital",
            code: "SKH003",
            address: "No. 95, Wen Chang Rd., Taipei City, Taiwan",
            telecom: "+886 2 2833 2211"
        }
    ],
    medicationOrders: [
        {
            id: generateRandomId("MO"),
            name: "Paracetamol 500mg",
            code: "PAR500",
            daysOfAdministration: 7,
            totalQuantity: 14,
            unit: "tablets"
        },
        {
            id: generateRandomId("MO"),
            name: "Ibuprofen 200mg",
            code: "IBU200",
            daysOfAdministration: 5,
            totalQuantity: 10,
            unit: "tablets"
        },
        {
            id: generateRandomId("MO"),
            name: "Amoxicillin 250mg",
            code: "AMOX250",
            daysOfAdministration: 10,
            totalQuantity: 30,
            unit: "capsules"
        },
        {
            id: generateRandomId("MO"),
            name: "Cetirizine 10mg",
            code: "CET10",
            daysOfAdministration: 3,
            totalQuantity: 6,
            unit: "tablets"
        },
        {
            id: generateRandomId("MO"),
            name: "Metformin 500mg",
            code: "MET500",
            daysOfAdministration: 30,
            totalQuantity: 60,
            unit: "tablets"
        },
        {
            id: generateRandomId("MO"),
            name: "Lisinopril 10mg",
            code: "LIS10",
            daysOfAdministration: 14,
            totalQuantity: 28,
            unit: "tablets"
        }
    ],
    nonMedicationOrders: [
        {
            id: generateRandomId("NMO"),
            name: "Physical Therapy - 30min session",
            code: "PT30",
            totalQuantity: 3,
            unit: "sessions"
        },
        {
            id: generateRandomId("NMO"),
            name: "X-ray Chest PA",
            code: "XRAY001",
            totalQuantity: 1,
            unit: "scan"
        },
        {
            id: generateRandomId("NMO"),
            name: "MRI Brain",
            code: "MRI002",
            totalQuantity: 1,
            unit: "scan"
        },
        {
            id: generateRandomId("NMO"),
            name: "Ultrasound Abdomen",
            code: "US001",
            totalQuantity: 1,
            unit: "scan"
        },
        {
            id: generateRandomId("NMO"),
            name: "Blood Test - CBC",
            code: "BT001",
            totalQuantity: 1,
            unit: "test"
        },
        {
            id: generateRandomId("NMO"),
            name: "ECG - Electrocardiogram",
            code: "ECG001",
            totalQuantity: 1,
            unit: "test"
        }
    ]
};


// Functions to populate dummy data
function fillMedicalInstitutionDropdown() {
    const select = document.getElementById("organizationName");
    select.innerHTML = '<option value="">Select organization</option>';
    dummyOutpatients.medicalInstitutions.forEach((institution) => {
        const option = document.createElement("option");
        option.value = institution.id;
        option.textContent = institution.name;
        select.appendChild(option);
    });
}

function addMedicationOrderRow() {
    const table = document.getElementById("medicationTable").querySelector("tbody");
    const row = document.createElement("tr");
    row.innerHTML = `
        <td>${table.rows.length + 1}</td>
        <td>
            <select class="form-select" onchange="updateMedicationCode(this)">
                <option value="">Select medication</option>
                ${dummyOutpatients.medicationOrders.map(
                    (order) =>
                        `<option value="${order.id}" data-code="${order.code}">${order.name}</option>`
                ).join("")}
            </select>
        </td>
        <td><input type="text" class="form-control" readonly></td>
        <td><input type="number" class="form-control"></td>
        <td>
            <input type="number" class="form-control" style="display:inline; width:60%">
            <select class="form-select" style="display:inline; width:30%">
                ${["tablets", "ml", "units"].map((unit) => `<option>${unit}</option>`).join("")}
            </select>
        </td>
        <td><button type="button" class="btn btn-danger" onclick="removeRow(this)">Remove</button></td>
    `;
    table.appendChild(row);
}

// Populate the dropdown for medical institutions
function populateMedicalInstitutions() {
    const institutionSelect = document.getElementById("OrganizationName");
    institutionSelect.innerHTML = '<option value="">Select organization</option>';

    dummyOutpatients.medicalInstitutions.forEach(institution => {
        const option = document.createElement("option");
        option.value = institution.id;
        option.textContent = institution.name;
        institutionSelect.appendChild(option);
    });
}

// Update organization details based on selected option
function updateOrganizationDetails() {
    const selectedId = document.getElementById("OrganizationName").value;
    const institution = dummyOutpatients.medicalInstitutions.find(inst => inst.id === selectedId);

    if (institution) {
        document.getElementById("OrganizationCode").value = institution.code;
    } else {
        document.getElementById("OrganizationCode").value = "";
    }
}

// Add a new row to the medication orders table
function addMedicationRow() {
    const table = document.getElementById("MedicationTable").getElementsByTagName("tbody")[0];
    const row = table.insertRow();

    const medication = dummyOutpatients.medicationOrders[0]; // Example: First medication order

    row.insertCell(0).textContent = table.rows.length; // Row number
    row.insertCell(1).textContent = medication.name;
    row.insertCell(2).textContent = medication.code;
    row.insertCell(3).textContent = medication.daysOfAdministration;
    row.insertCell(4).textContent = `${medication.totalQuantity} ${medication.unit}`;

    const actionCell = row.insertCell(5);
    const deleteButton = document.createElement("button");
    deleteButton.textContent = "Remove";
    deleteButton.classList.add("btn", "btn-danger");
    deleteButton.onclick = () => table.deleteRow(row.rowIndex - 1);
    actionCell.appendChild(deleteButton);
}

// Add a new row to the non-medication orders table
function addNonMedicationRow() {
    const table = document.getElementById("NonMedicationTable").getElementsByTagName("tbody")[0];
    const row = table.insertRow();

    const nonMedication = dummyOutpatients.nonMedicationOrders[0]; // Example: First non-medication order

    row.insertCell(0).textContent = table.rows.length; // Row number
    row.insertCell(1).textContent = nonMedication.name;
    row.insertCell(2).textContent = nonMedication.code;
    row.insertCell(3).textContent = `${nonMedication.totalQuantity} ${nonMedication.unit}`;

    const actionCell = row.insertCell(4);
    const deleteButton = document.createElement("button");
    deleteButton.textContent = "Remove";
    deleteButton.classList.add("btn", "btn-danger");
    deleteButton.onclick = () => table.deleteRow(row.rowIndex - 1);
    actionCell.appendChild(deleteButton);
}

// Extract table data for submission
function extractTableData(tableId) {
    const table = document.getElementById(tableId).getElementsByTagName("tbody")[0];
    return Array.from(table.rows).map(row => ({
        name: row.cells[1].textContent,
        code: row.cells[2].textContent,
        daysOfAdministration: row.cells[3]?.textContent || null,
        totalQuantity: row.cells[4]?.textContent || null
    }));
}

// Fill dummy patient data into the form
function fillDummyData(patientKey = "1") {
    const patientData = dummyPatients[patientKey];
    if (!patientData) return;

    document.getElementById("PatientName").value = `${patientData.FamilyName} ${patientData.GivenName}`;
    document.getElementById("MedicalRecordNumber").value = "MR123456"; // Example
    document.getElementById("Gender").value = patientData.Gender.toLowerCase();
    document.getElementById("NationalID").value = patientData.IdNo;
    document.getElementById("Birthday").value = patientData.Birthday;
}

// Submit the outpatient form data
function submitOutpatientData() {
    const formData = {
        patientName: document.getElementById("PatientName").value,
        medicalRecordNumber: document.getElementById("MedicalRecordNumber").value,
        gender: document.getElementById("Gender").value,
        nationalId: document.getElementById("NationalID").value,
        birthday: document.getElementById("Birthday").value,
        visitDate: document.getElementById("VisitDate").value,
        organizationName: document.getElementById("OrganizationName").options[document.getElementById("OrganizationName").selectedIndex].text,
        organizationCode: document.getElementById("OrganizationCode").value,
        dispensingDate: document.getElementById("DispensingDate").value,
        serialNumber: document.getElementById("SerialNumber").value,
        medications: extractTableData("MedicationTable"),
        nonMedications: extractTableData("NonMedicationTable")
    };

    console.log("Submitted Data:", formData);
    alert("Outpatient data submitted successfully!");
}

// Populate medical institution dropdown on page load
document.addEventListener("DOMContentLoaded", () => {
    populateMedicalInstitutions();
});