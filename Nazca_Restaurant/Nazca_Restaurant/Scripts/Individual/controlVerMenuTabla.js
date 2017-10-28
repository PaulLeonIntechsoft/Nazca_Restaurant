$(document).ready(function () {
    cargarTabla();
    var n = setInterval(cargarTabla, 500);
});

function cargarTabla() {
    $.ajax({
        url: 'llenarTabla',
        type: 'POST',
        success: function (data) {
            var v = '';
            $.each(data, function (i, v1) {
                v += "<tr ";
                v += "id=\"" + v1.chrCodPro + "\" class=\"rowTipoProducto\"  onclick=\"datosPlato(this)\">";
                v += "<td>";
                v += v1.chrDesTipo;
                v += "</td>";
                v += "<td>";
                v += v1.chrDesPro;
                v += "</td>";
                v += "<td>";
                v += v1.numPrecio;
                v += "</td>";
                v += "<td>";
                v += v1.cantStock;
                v += "</td>";
                v += "</tr>";
            });
            $("#tbody-menus").html(v);
        },
        error: function (mistake) {
            alert(mistake);
        }
    });
}

function datosPlato(fila) {
    var idRow = $(fila).attr("id");
    $.get("detallesPlato", { codProd: idRow }, function (data) {
        $("#nomPlato").html(data.chrDesPro);
        $("#descriptivoPlato").html(data.chrComentario);
        $("#nroPlatosVendidos").html(data.cantVendido);
    });
}