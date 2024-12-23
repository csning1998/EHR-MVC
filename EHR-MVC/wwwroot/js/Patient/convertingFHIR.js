function generateFHIRFormat(patient) {
    console.log(`Generating FHIR format for patient ID: ${patient.patientId}`);
    // Simulate FHIR generation logic here
    alert(`FHIR format for patient ID: ${patient.patientId} generated successfully.`);
}

async function uploadToFhir(patientId) {
    try {
        const response = await fetch('/Patient/GetFhirJson', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ PatientId: patientId })
        });

        const result = await response.json();
        if (!response.ok) {
            alert("Error: " + (result.Message || "Failed to get FHIR JSON"));
            return;
        }

        const formattedJson = JSON.stringify(JSON.parse(result.fhirJson), null, 4);

        document.getElementById('fhirModalBody').textContent = formattedJson;

        window.fhirToken = result.token;
        console.log("window.fhirToken ", window.fhirToken )

        const fhirModal = new bootstrap.Modal(document.getElementById('fhirModal'));
        fhirModal.show();

    } catch (err) {
        console.error("Error fetching FHIR JSON:", err);
        alert("Failed to fetch FHIR JSON.");
    }
}


function showFhirModal(fhirJson) {
    // Show FHIR data in a pop-up
    const fhirModalBody = document.getElementById('fhirModalBody');
    fhirModalBody.textContent = JSON.stringify(JSON.parse(fhirJson), null, 2); // Formatting

    const fhirModal = new bootstrap.Modal(document.getElementById('fhirModal'));
    fhirModal.show();
}

async function submitFhirJson() {
    try {
        if (!window.fhirToken) {
            alert("No FHIR token found. Please fetch JSON again.");
            return;
        }

        const response = await fetch('/Patient/SubmitFhirJson', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ Token: window.fhirToken })
        });

        const result = await response.json();
        if (!response.ok) {
            alert("Error: " + (result.Message || "Failed to submit FHIR JSON"));
            return;
        }

        alert(result.Message || "FHIR JSON submitted successfully.");

    } catch (err) {
        console.error("Error submitting FHIR JSON:", err);
        alert("Failed to submit FHIR JSON.");
    }
}



//async function uploadToFhir(patientId) {
//    try {
//        const response = await fetch('/Patient/UploadToFhir?patientId=' + encodeURIComponent(patientId), {
//            method: 'GET',
//        });

//        const result = await response.json();
//        console.log(result);

//        if (result.statusCode === 200) {
//            alert('Successfully uploaded to FHIR Server.');
//        } else {
//            alert(result.message || 'Failed to upload to FHIR Server.');
//        }
//    } catch (err) {
//        console.error('Error:', err);
//        alert('An error occurred.');
//    }
//}
