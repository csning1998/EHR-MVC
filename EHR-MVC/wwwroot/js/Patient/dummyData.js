
const dummyPatients = {
    "1": {
        FamilyName: "O'Sullivan",
        GivenName: "Ronnie",
        IdNo: "A123456789",
        Birthday: "1975-12-05",
        Gender: "M",
        Telecom: "0912345678",
        Address: "Wordsley, West Midlands, England",
        Email: "ronnie.osullivan@example.com",
        PostalCode: "DY8",
        Country: "United Kingdom",
        PreferredLanguage: "English",
        EmergencyContactName: "Maria Sullivan",
        EmergencyContactRelationship: "Spouse",
        EmergencyContactPhone: "0911123456"
    },
    "2": {
        FamilyName: "Uncle",
        GivenName: "Roger",
        IdNo: "AB12345678",
        Birthday: "1991-03-15",
        Gender: "F",
        Telecom: "0987654321",
        Address: "7th Floor Pavilion KL, Pavilion Elite, Bukit Bintang, Kuala Lumpur, Malaysia",
        Email: "roger.uncle@example.com",
        PostalCode: "55100",
        Country: "Malaysia",
        PreferredLanguage: "English",
        EmergencyContactName: "Auntie Helen",
        EmergencyContactRelationship: "Sister",
        EmergencyContactPhone: "0987111222"
    },
    "3": {
        FamilyName: "馬斯克",
        GivenName: "伊隆",
        IdNo: "CE12345678",
        Birthday: "1971-06-28",
        Gender: "M",
        Telecom: "0913467985",
        Address: "美國德克薩斯州布朗斯維爾星港",
        Email: "elon.musk@example.com",
        PostalCode: "78521",
        Country: "United States",
        PreferredLanguage: "English",
        EmergencyContactName: "Maye Musk",
        EmergencyContactRelationship: "Mother",
        EmergencyContactPhone: "0913123456"
    },
    "4": {
        FamilyName: "OYang",
        GivenName: "Jimmy",
        IdNo: "DY12345678",
        Birthday: "1987-06-11",
        Gender: "M",
        Telecom: "0928765432",
        Address: "Los Angeles, California, USA",
        Email: "jimmy.oyang@example.com",
        PostalCode: "90001",
        Country: "United States",
        PreferredLanguage: "English",
        EmergencyContactName: "Mrs. OYang",
        EmergencyContactRelationship: "Mother",
        EmergencyContactPhone: "0928123456"
    },
    "5": {
        FamilyName: "Chen",
        GivenName: "Brett",
        IdNo: "TT56712345",
        Birthday: "1992-07-21",
        Gender: "M",
        Telecom: "0932111234",
        Address: "Taipei, Taiwan",
        Email: "brett.chen@example.com",
        PostalCode: "100",
        Country: "Taiwan",
        PreferredLanguage: "Mandarin",
        EmergencyContactName: "Eddy Chen",
        EmergencyContactRelationship: "Brother",
        EmergencyContactPhone: "0932111111"
    },
    "6": {
        FamilyName: "Chen",
        GivenName: "Eddy",
        IdNo: "VV89012345",
        Birthday: "1993-09-09",
        Gender: "M",
        Telecom: "0921234567",
        Address: "Melbourne, Australia",
        Email: "eddy.chen@example.com",
        PostalCode: "3000",
        Country: "Australia",
        PreferredLanguage: "English",
        EmergencyContactName: "Brett Chen",
        EmergencyContactRelationship: "Brother",
        EmergencyContactPhone: "0921999999"
    },
    "7": {
        FamilyName: "Oliver",
        GivenName: "Jamie",
        IdNo: "FF12347891",
        Birthday: "1975-05-27",
        Gender: "M",
        Telecom: "0987321654",
        Address: "Essex, England",
        Email: "jamie.oliver@example.com",
        PostalCode: "CM13",
        Country: "United Kingdom",
        PreferredLanguage: "English",
        EmergencyContactName: "Jools Oliver",
        EmergencyContactRelationship: "Spouse",
        EmergencyContactPhone: "0987654321"
    },
    "8": {
        FamilyName: "Ramsay",
        GivenName: "Gordon",
        IdNo: "GR45612378",
        Birthday: "1966-11-08",
        Gender: "M",
        Telecom: "0978654321",
        Address: "London, England",
        Email: "gordon.ramsay@example.com",
        PostalCode: "WC2N",
        Country: "United Kingdom",
        PreferredLanguage: "English",
        EmergencyContactName: "Tana Ramsay",
        EmergencyContactRelationship: "Spouse",
        EmergencyContactPhone: "0978111111"
    },
    "9": {
        FamilyName: "He",
        GivenName: "Steven",
        IdNo: "SH09876123",
        Birthday: "1994-08-13",
        Gender: "M",
        Telecom: "0912233445",
        Address: "New York, USA",
        Email: "steven.he@example.com",
        PostalCode: "10001",
        Country: "United States",
        PreferredLanguage: "English",
        EmergencyContactName: "Mr. He",
        EmergencyContactRelationship: "Father",
        EmergencyContactPhone: "0912123456"
    },
    "10": {
        FamilyName: "Smith",
        GivenName: "Will",
        IdNo: "WS12345678",
        Birthday: "1968-09-25",
        Gender: "M",
        Telecom: "0967451238",
        Address: "Calabasas, California, USA",
        Email: "will.smith@example.com",
        PostalCode: "91302",
        Country: "United States",
        PreferredLanguage: "English",
        EmergencyContactName: "Jada Pinkett Smith",
        EmergencyContactRelationship: "Spouse",
        EmergencyContactPhone: "0967445678"
    },
    "11": {
        FamilyName: "Lee",
        GivenName: "Bruce",
        IdNo: "BL19320227",
        Birthday: "1940-11-27",
        Gender: "M",
        Telecom: "0912345678",
        Address: "San Francisco, California, USA",
        Email: "bruce.lee@example.com",
        PostalCode: "94109",
        Country: "United States",
        PreferredLanguage: "English",
        EmergencyContactName: "Linda Lee",
        EmergencyContactRelationship: "Spouse",
        EmergencyContactPhone: "0912111222"
    },
    "12": {
        FamilyName: "Trump",
        GivenName: "Donald",
        IdNo: "DT12345678",
        Birthday: "1946-06-14",
        Gender: "M",
        Telecom: "0911122334",
        Address: "725 Fifth Avenue, New York, NY, USA",
        Email: "donald.trump@example.com",
        PostalCode: "10022",
        Country: "United States",
        PreferredLanguage: "English",
        EmergencyContactName: "Melania Trump",
        EmergencyContactRelationship: "Spouse",
        EmergencyContactPhone: "0911234567"
    },
    "13": {
        FamilyName: "Biden",
        GivenName: "Joe",
        IdNo: "JB56781234",
        Birthday: "1942-11-20",
        Gender: "M",
        Telecom: "0912456789",
        Address: "1600 Pennsylvania Avenue NW, Washington, DC, USA",
        Email: "joe.biden@example.com",
        PostalCode: "20500",
        Country: "United States",
        PreferredLanguage: "English",
        EmergencyContactName: "Jill Biden",
        EmergencyContactRelationship: "Spouse",
        EmergencyContactPhone: "0912123456"
    },
    "14": {
        FamilyName: "WrongData4Test",
        GivenName: "ModifyToTest",
        IdNo: "A!12345678",
        Birthday: "1985-05-15",
        Gender: "F",
        Telecom: "0487654321",
        Address: "This is the invalid data for test purpose only",
        Email: "invalid-email@",
        PostalCode: "ABCDE",
        Country: "Unknown",
        PreferredLanguage: "UnknownLanguage",
        EmergencyContactName: "",
        EmergencyContactRelationship: "Undefined",
        EmergencyContactPhone: "12345"
    }
};

function fillDummyData() {
    const selectedValue = document.getElementById("dummyPatientSelect").value;
    if (dummyPatients[selectedValue]) {
        const patient = dummyPatients[selectedValue];
        const fields = [
            "FamilyName",
            "GivenName",
            "IdNo",
            "Birthday",
            "Gender",
            "Telecom",
            "Address",
            "Email",
            "PostalCode",
            "Country",
            "PreferredLanguage",
            "EmergencyContactName",
            "EmergencyContactRelationship",
            "EmergencyContactPhone"
        ];

        fields.forEach(field => {
            document.getElementById(field).value = patient[field] || "";
        });
    } else {
        const resetFields = [
            "FamilyName",
            "GivenName",
            "IdNo",
            "Birthday",
            "Gender",
            "Telecom",
            "Address",
            "Email",
            "PostalCode",
            "Country",
            "PreferredLanguage",
            "EmergencyContactName",
            "EmergencyContactRelationship",
            "EmergencyContactPhone"
        ];

        resetFields.forEach(field => {
            document.getElementById(field).value = "";
        });

        document.getElementById("Telecom").value = "09";
    }
}
