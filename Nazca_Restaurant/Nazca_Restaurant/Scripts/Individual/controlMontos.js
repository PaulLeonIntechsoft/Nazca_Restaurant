$(document).ready(function () {
    sumarMontos();
    var monto = setInterval(sumarMontos, 500)
});

function sumarMontos() {
    var arr = $('td[name=aSumar]');
    var montoTotal = 0;
    $.each(arr, function (i, v1) {
        var indp = v1;
        var montosUnicos = $(indp).text();
        montoTotal += parseFloat(montosUnicos);
    });
    $("#numSTotal").val(parseFloat(montoTotal).toFixed(2));

    var montoIGV = 0;
    montoIGV = parseFloat(montoTotal * 18 / 100).toFixed(2);
    $("#montoIgv").val(parseFloat(montoIGV).toFixed(2));

    var montoSubTotal = 0;
    montoSubTotal = parseFloat(montoTotal) - parseFloat(montoIGV);
    $("#montoSubTotal").val(parseFloat(montoSubTotal).toFixed(2));

    var tipoCambio = $("#tipoCambioText").val();
    if (tipoCambio != null && tipoCambio != '' && tipoCambio != '0.00') {
        var montoDolares = 0;
        montoDolares = parseFloat(montoTotal) / parseFloat(tipoCambio);
        $("#montoDolares").val(parseFloat(montoDolares).toFixed(2));
    } else {
        $("#montoDolares").val(parseFloat("00.00").toFixed(2));
    }

}