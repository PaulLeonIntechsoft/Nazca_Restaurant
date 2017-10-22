$(document).ready(function () {
    $(".link-descriptivo").click(function () {
        var descriptivo = $(this).closest(".card").children(".bodyCartaContainer").children(".card-body").text();
        leerEstadoMesa(descriptivo);
        mozoEstadoElegido(descriptivo);
    });
});

function leerEstadoMesa(descriptivo) {
    if (descriptivo.trim() == "") {
        $("#avisoSeleccioneMesa").css("display", "none");
        $("#avisoDeVacio").css("display", "block");
        $("#tablaVenta").css("display", "none");
    } else {
        $("#avisoSeleccioneMesa").css("display", "none");
        $("#avisoDeVacio").css("display", "none");
        $("#tablaVenta").css("display", "block");
    }
};

function mozoEstadoElegido(descriptivo) {
    var mozoElegido = $("#cboMozos").val();
    console.log(mozoElegido);
    if (descriptivo.trim() == "") {
        if (mozoElegido != null && mozoElegido != "") {
            $("#modalConfirmarSalida").modal("show");
        };
    };
};