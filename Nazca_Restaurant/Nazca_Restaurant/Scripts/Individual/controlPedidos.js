$(document).ready(function () {
    $("#cboTiposDeProducto").change(function () {
        listarPlatos(this);
    });

    $(".noVacio").change(function () {
        $("#mensajeError").empty();
        $(".referenciaNula").prop('disabled', false);
        $("#btnAgregarPedido").prop('disabled', false);
        $("#procesarVenta").prop('disabled', false);
        $("#finalizarVenta").prop('disabled', false);
    });

    $("#btnAgregarPedido").click(function () {
        if (validarCampos()) {
            agregarPedido();
        }
    });

    $("#procesarVenta").click(function () {
        var tipoVentana = $("#modoVentana").val();
        if (tipoVentana == 'ventaSeleccionada') {
            procesarEdiciones();
        } else {
            procesarVenta();
        }
        $("#cboContainer").hide();
    });

    $("#finalizarVenta").click(function () {
        finalizarPedido();
    });

});

function listarPlatos(tipoSeleccionado) {
    var idTipo = $(tipoSeleccionado).val();
    $("#cboProductos").empty();
    $.ajax({
        url: 'ProductosBind',
        data: {
            idTipo: idTipo
        },
        type: 'POST',
        success: function (data) {
            var v = '';
            v += '<select id="cboProductos" name="cboProductos" class="custom-select custom-select-sm">';
            v += '<option value="" selected> -- Seleccionar -- </option>';
            $.each(data, function (i, v1) {
                v += '<option value="' + v1.chrCodPro + '">';
                v += v1.chrDesPro + ' - S/.' + parseFloat(v1.numPrecio).toFixed(2);
                v += '</option>';
            });
            v += '</select>';
            $("#cboProductos").html(v);
        },
        error: function (xhr, statusText, err) {
            alert("error" + xhr.status);
        }
    });
};

function agregarPedido() {
    var codProd = $("#cboProductos").val();
    var cantProd = $("#cantPedido").val();
    var comProd = $("#comPedido").val();
    $.ajax({
        url: 'AgregarPedido',
        data: {
            chrCodPro: codProd.trim(),
            chrComPro: comProd.trim(),
            intCanPro: cantProd.trim()
        },
        type: 'POST',
        success: function (data) {
            if (data != null) {
                var v = '';
                $.each(data, function (i, v1) {
                    if (v1.paraEliminar == '0') {
                        v += '<tr>';
                        v += '<td class="text-center" style="padding-left: 0px;padding-right: 0px;width: 57px;">';
                        if (v1.bytAteCoc == '0') {
                            v += '<button  onclick="eliminarPedido(this)" id="' + v1.chrCodPro + '" type="button" tipoControl="' + v1.tipoControl + ' ">';
                            v += '<span>&times;</span>';
                            v += '</button>';
                        }
                        v += '</td>';
                        v += '<td class="text-center">';
                        v += v1.chrDesPro;
                        v += '</td>';
                        v += '<td class="text-center">';
                        v += v1.chrComPro;
                        v += '</td>';
                        v += '<td class="text-center">';
                        v += parseFloat(v1.numPreVen).toFixed(2);
                        v += '</td>';
                        v += '<td class="text-center">';
                        v += v1.intCanPro;
                        v += '</td>';
                        v += '<td name="aSumar" class="text-center">';
                        v += '' + parseFloat(parseFloat(v1.numPreVen) * parseFloat(v1.intCanPro)).toFixed(2);
                        v += '</td>';
                        v += '<td class="text-center">';
                        v += '<div class="form-check">';
                        v += '<label class="form-check-label">';
                        if (v1.bytAteCoc == '0') {
                            v += '<input class="form-check-input" type="checkbox" value=""  disabled/>';
                        } else if (v1.bytAteCoc == '1') {
                            v += '<input class="form-check-input" type="checkbox" value="" selected="selected" disabled/>';
                        }
                        v += '<label>';
                        v += '</div>';
                        v += '</td>';
                        v += '</tr>';
                    };
                });
                $("#tbody-menu").html(v);
                $("#cboProductos").val("");
                $("#cantPedido").val("1");
                $("#comPedido").val("");
            };
        },
        error: function (xhr, statusText, err) {
            alert("error" + xhr);
        }
    });
};

function eliminarPedido(boton) {
    var id = $(boton).attr('id');
    var tipoControl = $(boton).attr('tipoControl').toString();
    $.ajax({
        url: 'EliminarPedido',
        data: {
            codProd: id.trim(),
            tipoControl: tipoControl
        },
        type: 'POST',
        success: function (data) {
            if (data != null) {
                var v = '';
                $.each(data, function (i, v1) {
                    if (v1.paraEliminar == '0') {
                        v += '<tr>';
                        v += '<td class="text-center" style="padding-left: 0px;padding-right: 0px;width: 57px;">';
                        if (v1.bytAteCoc == '0') {
                            v += '<button  onclick="eliminarPedido(this)" id="' + v1.chrCodPro + '" type="button" tipoControl="' + v1.tipoControl + ' ">';
                            v += '<span>&times;</span>';
                            v += '</button>';
                        }
                        v += '</td>';
                        v += '<td class="text-center">';
                        v += v1.chrDesPro;
                        v += '</td>';
                        v += '<td class="text-center">';
                        v += v1.chrComPro;
                        v += '</td>';
                        v += '<td class="text-center">';
                        v += parseFloat(v1.numPreVen).toFixed(2);
                        v += '</td>';
                        v += '<td class="text-center">';
                        v += v1.intCanPro;
                        v += '</td>';
                        v += '<td name="aSumar" class="text-center">';
                        v += '' + parseFloat(parseFloat(v1.numPreVen) * parseFloat(v1.intCanPro)).toFixed(2);
                        v += '</td>';
                        v += '<td class="text-center">';
                        v += '<div class="form-check">';
                        v += '<label class="form-check-label">';
                        if (v1.bytAteCoc == '0') {
                            v += '<input class="form-check-input" type="checkbox" value=""  disabled/>';
                        } else if (v1.bytAteCoc == '1') {
                            v += '<input class="form-check-input" type="checkbox" value="" selected="selected" disabled/>';
                        }
                        v += '<label>';
                        v += '</div>';
                        v += '</td>';
                        v += '</tr>';
                    };
                });
                $("#tbody-menu").html(v);
            };
        },
        error: function (xhr, statusText, err) {
            alert("error" + xhr);
        }
    });
};

function procesarVenta() {
    var codigoMesa = $("#mesaSeleccionada").val();;
    var codigoMozo = $("#cboMozos").val();;
    var tarjCredito = '0';
    var tipoPago = 'T';
    var montoTotal = $("#numSTotal").val();;

    if ($('#bytPagTar').is(':checked')) {
        tarjCredito = '1';
    }

    if ($('#radioBoleta').is(':checked')) {
        tipoPago = 'B';
    }

    if ($('#radioFactura').is(':checked')) {
        tipoPago = 'F';
    }

    $.ajax({
        url: 'GuardarVenta',
        data: {
            codMesa: codigoMesa,
            codMozo: codigoMozo,
            codTarjeta: tarjCredito,
            tipoPago: tipoPago,
            montoTotal: montoTotal
        },
        type: 'POST',
        success: function (data) {
            var mesaElegida = $("#mesaSeleccionada").val();
            ajaxLimpiarSession();
            ajaxLlenarTabla(mesaElegida);
        },
        error: function (xhr, statusText, err) {
            alert("error" + xhr.status);
        }
    });

};

function procesarEdiciones() {
    var codigoMesa = $("#mesaSeleccionada").val();;
    var tarjCredito = '0';
    var tipoPago = 'T';
    var montoTotal = $("#numSTotal").val();;

    if ($('#bytPagTar').is(':checked')) {
        tarjCredito = '1';
    }

    if ($('#radioBoleta').is(':checked')) {
        tipoPago = 'B';
    }

    if ($('#radioFactura').is(':checked')) {
        tipoPago = 'F';
    }

    $.ajax({
        url: 'EditarVenta',
        data: {
            codMesa: codigoMesa,
            codTarjeta: tarjCredito,
            tipoPago: tipoPago,
            montoTotal: montoTotal
        },
        type: 'POST',
        success: function (data) {
            var mesaElegida = $("#mesaSeleccionada").val();
            ajaxLimpiarSession();
            ajaxLlenarTabla(mesaElegida);
        },
        error: function (xhr, statusText, err) {
            alert("error" + xhr.status);
        }
    });

};

function finalizarPedido() {
    var codigoMesa = $("#mesaSeleccionada").val();;
    $.ajax({
        url: 'FinalizarVenta',
        data: {
            codMesa: codigoMesa
        },
        type: 'POST',
        success: function (data) {
            ajaxLimpiarSession();
            ajaxLlenarTabla(codigoMesa);
        },
        error: function (xhr, statusText, err) {
            alert("error" + xhr.status);
        }
    });
};

function validarCampos() {
    $("#mensajeError").empty();
    var flag = true;
    if ($("#cboTiposDeProducto").val() == "") {
        flag = false;
    } else if ($("#cboProductos").val() == "") {
        flag = false;
    } else if ($("#cantPedido").val() == "") {
        flag = false;
    }

    if (flag == false) {
        $(".referenciaNula").prop('disabled', true);
        $("#btnAgregarPedido").prop('disabled', true);
        $("#procesarVenta").prop('disabled', true);
        $("#finalizarVenta").prop('disabled', false);
        $("#mensajeError").html('Todos los campos deben tener data.');
    }
    return flag;
}