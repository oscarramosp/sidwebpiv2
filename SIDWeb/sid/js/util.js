//a
function esFechaValida(strValue) {
    var objRegExp = /^([0-9]){2}(\/|-){1}([0-9]){2}(\/|-)([0-9]){4}$/;
    if (!objRegExp.test(strValue)) {
        return false;
    }
    else {
        var strSeparator = strValue.substring(2, 3)
        var arrayDate = strValue.split(strSeparator);

        var arrayLookup = { '01': 31,
            '03': 31, '04': 30,
            '05': 31, '06': 30,
            '07': 31, '08': 31,
            '09': 30, '10': 31,
            '11': 30, '12': 31
        }

        var intDay = parseInt(arrayDate[0], 10);
        var intMonth = parseInt(arrayDate[1], 10);
        var intYear = parseInt(arrayDate[2], 10);

        if (arrayLookup[arrayDate[1]] != null) {
            if (intDay <= arrayLookup[arrayDate[1]] && intDay != 0 /*&& intYear > 1975 && intYear < 2050*/)
                return true;
        }

        if (intMonth == 2) {
            var intYear = parseInt(arrayDate[2]);

            if (intDay > 0 && intDay < 29) {
                return true;
            }
            else if (intDay == 29) {
                if ((intYear % 4 == 0) && (intYear % 100 != 0) || (intYear % 400 == 0)) {
                    return true;
                }
            }
        }
    }
    return false;
}

function esFechaMayorIgual(fechaMayor, fechaMenor) {
    var fecha1 = fechaMayor.substring(3, 5) + '/' + fechaMayor.substring(0, 2) + '/' + fechaMayor.substring(6);
    var fecha2 = fechaMenor.substring(3, 5) + '/' + fechaMenor.substring(0, 2) + '/' + fechaMenor.substring(6);
    var date1 = new Date(fecha1);
    var date2 = new Date(fecha2);
    return (date1 >= date2);
}

function esEmailValido(strValue) {
    var objRegExp = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
    return objRegExp.test(strValue);
}

function soloEnteros(evt) {
    var charCode = (evt.which) ? evt.which : window.event.keyCode;
    if (charCode <= 13) {
        return true;
    }
    else {
        var keyChar = String.fromCharCode(charCode);
        var reg = /[0-9]/;
        return reg.test(keyChar);
    }
}

function esEnteroValido(str) {
    var reg = /[0-9]/;
    return reg.test(str);
}

function soloAlfaEspacio(evt) {
    var charCode = (evt.which) ? evt.which : window.event.keyCode;
    if (charCode <= 13) {
        return true;
    }
    else {
        var keyChar = String.fromCharCode(charCode);
        var reg = /[a-zA-Z]|[ñ]|[Ñ]|[á]|[é]|[í]|[ó]|[ú]|[Á]|[É]|[Í]|[Ó]|[Ú]|[\s]/;
        return reg.test(keyChar);
    }
}

function evaluarAlfaEspacio(texto) {
    var reg = /^(([a-zA-Z]|[ñ]|[Ñ]|[á]|[é]|[í]|[ó]|[ú]|[Á]|[É]|[Í]|[Ó]|[Ú]|[,]|[\s])*)$/;
    return reg.test(texto);
}

function right(cadena, c) {
    return cadena.substring(cadena.length - c);
}

function esFechaMayorHoy(fechaValidar) {
    var date1 = new Date();
    var fecha1 = fechaValidar.substring(3, 5) + '/' + fechaValidar.substring(0, 2) + '/' + fechaValidar.substring(6);
    var fecha2 = (date1.getMonth() + 1) + '/' + date1.getDate() + '/' + date1.getFullYear();
    date1 = new Date(fecha1);
    var date2 = new Date(fecha2);
    return (date2 < date1);
}