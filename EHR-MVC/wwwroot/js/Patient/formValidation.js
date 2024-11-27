function sanitizeInput(input, allowApostrophe = false) {
    const illegalChars = allowApostrophe ? /[\";<>:\\|/?*]/g : /['\";<>:\\|/?*]/g;
    if (illegalChars.test(input)) {
        return input.replace(illegalChars, function (match) {
            return encodeURIComponent(match);
        });
    }
    return input;
}

function regexValidator(fieldId) {
    const value = document.getElementById(fieldId).value;
    let pattern;
    let errorMessage;
    let isValid = false;

    switch (fieldId) {
        case 'Telecom':
            pattern = /^09\d{8}$/;
            errorMessage = "Invalid Contact Number. The number must start with '09' and contain exactly 10 digits.";
            if (value.length < 10) {
                document.getElementById(fieldId).classList.add("is-invalid");
                return false;
            }
            isValid = pattern.test(value);
            break;
        case 'IdNo':
            pattern = /^([A-Z][1-2][0-9]{8}|[A-Z]{2}[0-9]{8})$/;
            errorMessage = "Invalid ID Format. Please check your data.";
            if (value.length < 10) {
                document.getElementById(fieldId).classList.add("is-invalid");
                return false;
            }
            isValid = pattern.test(value) && value.length === 10;
            break;
        case 'Birthday':
            pattern = /^\d{4}-\d{2}-\d{2}$/;
            errorMessage = "Invalid Birthday format. Please use YYYY-MM-DD format.";
            isValid = pattern.test(value);
            break;
        default:
            isValid = true;
            break;
    }

    if (!isValid) {
        console.error(`Invalid ${fieldId} Value: "${value}".`);
        alert(errorMessage);
        document.getElementById(fieldId).classList.add("is-invalid");
        return false;
    } else {
        document.getElementById(fieldId).classList.remove("is-invalid");
        return true;
    }
}
