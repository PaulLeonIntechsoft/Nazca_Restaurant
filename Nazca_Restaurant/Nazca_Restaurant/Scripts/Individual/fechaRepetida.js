$(document).ready(function () {
    $("#dtmFecAper").change(function () {
        var fecha = $(this).val();
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear();
        var fecha2 = "";
        if (dd < 10) {
            dd = '0' + dd
        }

        if (mm < 10) {
            mm = '0' + mm
        }
        fecha2 = yyyy + '-' + mm + '-' + dd;
        if (fecha != fecha2) {
            $("#btnGrabar").prop('disabled', true);
            $("#fechaRepetida").html("No es permitido actualizar apertura en fecha distinta a la actual.");
        } else {
            $("#btnGrabar").prop('disabled', false);
            $("#fechaRepetida").html("");
        }
    });
});