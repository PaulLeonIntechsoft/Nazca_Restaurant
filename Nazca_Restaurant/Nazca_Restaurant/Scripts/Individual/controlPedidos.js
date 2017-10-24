$(document).ready(function () {
    $("#cboTiposDeProducto").change(function () {
        listarPlatos(this);
    });

    $("#btnAgregarPedido").click(function () {
        agregarPedido();
    });

    $("#procesarVenta").click(function () {
        procesarVenta();
    });

});

function listarPlatos(tipoSeleccionado) {
    var idTipo = $(tipoSeleccionado).val();
    $("#cboProductos").empty();
    $.ajax({
        url: 'Productos_Bind',
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
        url: 'ArmarMenu',
        data: {
            chrCodPro: codProd.trim(),
            chrComPro: comProd.trim(),
            intCanPro: cantProd.trim()
        },
        type: 'POST',
        success: function (data) {
            var v = '';
            $.each(data, function (i, v1) {
                v += '<tr>';
                v += '<td class="text-center">';
                v += v1.chrCodPro;
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
                v += '<input class="form-check-input" type="checkbox" value=""  disabled/>';
                v += '<label>';
                v += '</div>';
                v += '</td>';
                v += '</tr>';
            });
            $("#tbody-menu").html(v);
            $("#cboTiposDeProducto").val("");
            $("#cboProductos").val("");
            $("#cantPedido").val("");
            $("#comPedido").val("");
        },
        error: function (xhr, statusText, err) {
            alert("error" + xhr.status);
        }
    });
}

function procesarVenta() {
    var codigoMesa = $("#mesaSeleccionada").val();;
    var codigoMozo = $("#cboMozos").val();;
    var tarjCredito = '0';
    var recibePropina = '0';
    var montoPropina = '';
    var tipoPago = 'B';
    var montoTotal = $("#numSTotal").val();;
    
    if ($('#bytPagTar').is(':checked')) {
        tarjCredito = '1';
    }

    if ($('#bytPropina').is(':checked')) {
        recibePropina = '1';
        montoPropina = $("#numPropina").val();
    }

    if ($('#radioFactura').is(':checked')) {
        tipoPago = 'F';
    }

    $.ajax({
        url: 'Guardar_Cambios',
        data: {
            codMesa: codigoMesa,
            codMozo: codigoMozo,
            codTarjeta: tarjCredito,
            codProp: recibePropina,
            montoPropina: montoPropina,
            tipoPago: tipoPago,
            montoTotal: montoTotal
        },
        type: 'POST',
        success: function (data) {
            alert("funcionó");
        },
        error: function (xhr, statusText, err) {
            alert("error" + xhr.status);
        }
    });

}