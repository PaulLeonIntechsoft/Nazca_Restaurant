$(document).ready(function () {
    ajaxListadoMesas();
    var lst = setInterval(ajaxListadoMesas, 500);
});

function ajaxListadoMesas() {
    $.ajax({
        url: 'MesasBind',
        data: {},
        type: 'POST',
        success: function (data) {
            $.each(data, function (i, v1) {
                $("#" + v1.chrCodMesa).addClass("bg-info");
                $("#" + v1.chrCodMesa).removeClass("bg-primary");
                $("#" + v1.chrCodMesa).removeClass("bg-warning");
                $("#" + v1.chrCodMesa).removeClass("bg-success");
                var v = '';
                var indice = i + 1;
                if (v1.chrNomMoz == '' || v1.chrNomMoz == null || v1.chrApeMoz == '' || v1.chrApeMoz == null) {
                    v = '';
                } else {
                    if (v1.chrCodEstado == "C") {
                        v = '';
                    } else {
                        v += 'Mozo :  ' + v1.chrNomMoz + ' ' + v1.chrApeMoz + '<br>';
                        v += 'Inicio :  ' + v1.chrHorVen;
                    }
                }
                $("#texto" + indice).html(v);
                if (v1.chrCodEstado == "A") {
                    $("#" + v1.chrCodMesa).removeClass("bg-info");
                    $("#" + v1.chrCodMesa).addClass("bg-success");
                } else if (v1.chrCodEstado == "B") {
                    $("#" + v1.chrCodMesa).removeClass("bg-info");
                    $("#" + v1.chrCodMesa).addClass("bg-warning");
                } else if (v1.chrCodEstado == "C") {
                    $("#" + v1.chrCodMesa).removeClass("bg-info");
                    $("#" + v1.chrCodMesa).addClass("bg-primary");
                } else {
                    $("#" + v1.chrCodMesa).removeClass("bg-info");
                    $("#" + v1.chrCodMesa).addClass("bg-primary");
                }
            });
        },
        error: function (xhr, statusText, err) {
            alert("error" + xhr.status);
        }
    });
}