$(document).ready(function () {
    cargarDolares();
    $(".link-descriptivo").click(function () {
        var id = $(this).closest(".card").attr("id");
        var codMesa = $(this).closest(".card").attr("estMesa");
        $("#mesaSeleccionada").val(id);
        $("#mesaSeleccionada").attr('estMesa', codMesa);
        var tipoVentana = $("#modoVentana").val();
        if (tipoVentana != "ventaNueva") {
            ajaxLlenarTabla(id);
        }
    });
});

function ajaxLlenarTabla(codigoMesa) {
    $("#modoVentana").val("ventaSeleccionada");
    $("#tbody-menu").empty();
    $.ajax({
        url: "MenuBind",
        data: {
            idMesa: codigoMesa
        },
        type: "POST",
        success: function (data) {
            if (data != null) {
                var datosVentaVar = data.datosVenta;
                var detallesVentaVar = data.detallesVenta;
                var v = '';
                if (datosVentaVar.bytPagTar == 1) {
                    $("#bytPagTar").attr('selected', 'selected');
                }
                if (datosVentaVar.bytPropina == 1) {
                    $("#bytPropina").attr('selected', 'selected');
                    $("#numPropina").val(datosVentaVar.numPropina);
                }

                if (datosVentaVar.chrForPag.trim() == 'F') {
                    $("#radioFactura").prop('checked', true);
                } else if (datosVentaVar.chrForPag.trim() == 'B') {
                    $("#radioBoleta").prop('checked', true);
                } else if (datosVentaVar.chrForPag.trim() == 'T') {
                    $("#radioTicket").prop('checked', true);
                }
                $.each(detallesVentaVar, function (i, v1) {
                    if (v1.paraEliminar == '0') {
                        v += '<tr>';
                        v += '<td class="text-center" style="padding-left: 0px;padding-right: 0px;width: 57px;">';
                        if (v1.bytAteCoc == '0') {
                            v += '<button class="referenciaNula" onclick="eliminarPedido(this)" id="' + v1.chrCodPro + '" type="button" tipoControl="' + v1.tipoControl + ' ">';
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
                            v += '<input name="chkPedidos" class="form-check-input" type="checkbox" value=""  disabled/>';
                        } else if (v1.bytAteCoc == '1') {
                            v += '<input name="chkPedidos" class="form-check-input" type="checkbox" value="" selected="selected" disabled/>';
                        }
                        v += '<label>';
                        v += '</div>';
                        v += '</td>';
                        v += '</tr>';
                    };
                });
                $("#tbody-menu").html(v);
            };
            if ($("#mesaSeleccionada").attr('estMesa') == 'C') {
                $(".referenciaNula").prop('disabled', true);
                $("#btnAgregarPedido").prop('disabled', true);
                $("#procesarVenta").prop('disabled', true);
                $("#finalizarVenta").prop('disabled', true);
            };
        },
        error: function (xhr, statusText, err) {
            alert("error" + xhr.status);
        }
    });
};

function cargarDolares() {
    $.ajax({
        url: 'CargarDolares',
        type: 'POST',
        success: function (data) {
            if (data != null && data != "") {
                $("#tipoCambioText").val(data);
            }
        },
        error: function (xhr, statusText, err) {
            alert("error" + xhr.status);
        }
    });
}