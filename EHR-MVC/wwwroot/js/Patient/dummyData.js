
const dummyPatients = {
    "1": {
        FamilyName: "O'Sullivan",
        GivenName: "Ronnie",
        IdNo: "A123456789",
        Birthday: "1975-12-05",
        Gender: "M",
        Telecom: "0912345678",
        Address: "Wordsley, West Midlands, England"
    },
    "2": {
        FamilyName: "Uncle",
        GivenName: "Roger",
        IdNo: "AB12345678",
        Birthday: "1991-03-15",
        Gender: "F",
        Telecom: "0987654321",
        Address: "7th Floor Pavilion KL, Pavilion Elite, Bukit Bintang, 55100, Kuala Lumpur, Malaysia 55100"
    },
    "3": {
        FamilyName: "馬斯克",
        GivenName: "伊隆",
        IdNo: "CE12345678",
        Birthday: "1971-06-28",
        Gender: "F",
        Telecom: "0913467985",
        Address: "美國德克薩斯州布朗斯維爾星港"
    },
    "4": {
        FamilyName: "WrongData4Test",
        GivenName: "ModifyToTest",
        IdNo: "A!12345678",
        Birthday: "1985-05-15",
        Gender: "F",
        Telecom: "0487654321",
        Address: "This is the invalid data for test purpose only"
    }
};

function fillDummyData() {
    const selectedValue = document.getElementById("dummyPatientSelect").value;
    if (dummyPatients[selectedValue]) {
        const patient = dummyPatients[selectedValue];
        document.getElementById("FamilyName").value = patient.FamilyName;
        document.getElementById("GivenName").value = patient.GivenName;
        document.getElementById("IdNo").value = patient.IdNo;
        document.getElementById("Birthday").value = patient.Birthday;
        document.getElementById("Gender").value = patient.Gender;
        document.getElementById("Telecom").value = patient.Telecom;
        document.getElementById("Address").value = patient.Address;
    } else {
        document.getElementById("FamilyName").value = "";
        document.getElementById("GivenName").value = "";
        document.getElementById("IdNo").value = "";
        document.getElementById("Birthday").value = "";
        document.getElementById("Gender").value = "";
        document.getElementById("Telecom").value = "09";
        document.getElementById("Address").value = "";
    }
}
