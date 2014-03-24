//a
function GetValue(y, z, l, r) {
    box = document.getElementById(y);
    x = '';
    if (l == '#') {
        x = ' ' + "UP_GETVARIABLE{'" + box.options[box.selectedIndex].value + "'}" + ' ';
    } else {
        x = ' ' + l + box.options[box.selectedIndex].text + r + ' ';
    }
    textedit = document.getElementById(z);
    if (textedit.createTextRange && textedit.caretPos) {
        var caretPos = textedit.caretPos;
        caretPos.text =
		caretPos.text.charAt(caretPos.text.length - 1) == ' ' ?
		x + ' ' : x;
    }
    else {
        textedit.value = textedit.value + x;
    }
}

function fieldNumber(evt) {
    var evento_key = evt.keyCode;
    switch (evento_key) {
        case 48:
        case 49:
        case 50:
        case 51:
        case 52:
        case 53:
        case 54:
        case 55:
        case 56:
        case 57: break;
        default: evt.keyCode = 0;
            return false;
    }
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}