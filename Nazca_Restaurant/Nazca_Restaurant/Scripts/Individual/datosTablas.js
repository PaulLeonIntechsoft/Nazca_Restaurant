$(document).ready(function () {

    $(".link-descriptivo").click(function () {
        var descriptivo = $(this).closest(".card").children(".bodyCartaContainer").children(".card-body").text();
        mozoEstadoElegido(this);
        leerEstadoMesa(descriptivo);
    });

    $("a").click(function () {
        var tipoVentana = $("#modoVentana").val();
        if (tipoVentana == "ventaNueva") {
            return false;
        };
    });

    $("#img-agregar").click(function () {
        $("#avisoSeleccioneMesa").css("display", "none");
        $("#avisoDeVacio").css("display", "none");
        $("#tablaVenta").css("display", "block");
        $("#modoVentana").val("ventaNueva");
    });

    $("#confirmarSalidaButton").click(function () {
        $("#avisoSeleccioneMesa").css("display", "block");
        $("#avisoDeVacio").css("display", "none");
        $("#tablaVenta").css("display", "none");
        $("#cboMozos").val("");
    });

});

function leerEstadoMesa(descriptivo) {
    if (descriptivo.trim() == "") {
        $("#avisoSeleccioneMesa").css("display", "none");
        $("#avisoDeVacio").css("display", "block");
        $("#tablaVenta").css("display", "none");
        $("#cboContainer").css("display", "block");
        $("#modoVentana").val("ventaVacia");
    } else {
        $("#avisoSeleccioneMesa").css("display", "none");
        $("#avisoDeVacio").css("display", "none");
        $("#tablaVenta").css("display", "block");
        $("#cboContainer").css("display", "none");
        $("#modoVentana").val("ventaSeleccionada");
    }
};

function mozoEstadoElegido(linkSeleccionado) {
    var mozoElegido = $("#cboMozos").val();
    var tipoVentana = $("#modoVentana").val();
    if (mozoElegido != null && mozoElegido != "") {
        if (tipoVentana == "ventaNueva") {
            $("#modalConfirmarSalida").modal("show");
        };
    };
};

function ocultarCollapse(linkSeleccionado) {
    $(linkSeleccionado).closest(".card").children(".bodyCartaContainer").collapse("hide");
}