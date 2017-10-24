$(document).ready(function () {
    $(".link-descriptivo").click(function () {
        var id = $(this).closest(".card").attr("id");
        $("#mesaSeleccionada").val(id);
        var tipoVentana = $("#modoVentana").val();
        if (tipoVentana != "ventaNueva") {
            ajaxLlenarTabla(id);
        }
    });
});

function ajaxLlenarTabla(codigoMesa) {
    $("#tbody-menu").empty();
    $.ajax({
        url: "MenuBind",
        data: {
            idMesa : codigoMesa
        },
        type: "POST",
        success: function (data) {
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
            }
            $.each(detallesVentaVar, function (i, v1) {
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
        },
        error: function (xhr, statusText, err) {
            alert("error" + xhr.status);
        }
    });
}