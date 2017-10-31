$(document).ready(function () {

    $("a").click(function (e) {
        var tipoVentana = $("#modoVentana").val();
        var mozoElegido = $("#cboMozos").val();
        if (tipoVentana == "ventaNueva") {
            if(mozoElegido != ""){
                e.preventDefault();
            }
        };
    });

    $(".link-descriptivo").click(function () {
        var descriptivo = $(this).closest(".card").children(".bodyCartaContainer").children(".card-body").text();
        mozoEstadoElegido();
        leerEstadoMesa(descriptivo);
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
        $("#cboTiposDeProducto").val("");
        $("#cboProductos").val("");
        $("#cantPedido").val("1");
        $("#comPedido").val("");
        $("#modoVentana").val("seleccionarMesa");
        ajaxLimpiarSession();
    });
    $("#cancelar").click(function () {
        ajaxLimpiarSession();
    });


});

function leerEstadoMesa(descriptivo) {
    var tipoVentana = $("#modoVentana").val();
    if (descriptivo.trim() == "") {
        $("#avisoSeleccioneMesa").css("display", "none");
        $("#avisoDeVacio").css("display", "block");
        $("#tablaVenta").css("display", "none");
        $("#cboContainer").show();
        $("#modoVentana").val("ventaVacia");
    } else {
        if(tipoVentana != "ventaNueva"){
            $("#avisoSeleccioneMesa").css("display", "none");
            $("#avisoDeVacio").css("display", "none");
            $("#tablaVenta").css("display", "block");
            $("#cboContainer").hide();
            $("#modoVentana").val("ventaSeleccionada");
        }
    }
};

function mozoEstadoElegido() {
    var mozoElegido = $("#cboMozos").val();
    var tipoVentana = $("#modoVentana").val();
    if (mozoElegido != null && mozoElegido != "") {
        if (tipoVentana == "ventaNueva") {
            $("#modalConfirmarSalida").modal("show");
        };
    };
};