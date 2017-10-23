$(document).ready(function () {
    $(".link-descriptivo").click(function () {
        var id = $(this).closest(".card").attr("id");
        console.log(id);
        ajaxLlenarTabla(id);
    });
});

function ajaxLlenarTabla(codigoMesa) {
    $.ajax({
        url: "MenuBind",
        data: {
            idMesa : codigoMesa
        },
        type: "POST",
        success: function (data) {
            var datosVentaVar = data.datosVenta;
            var detallesVentaVar = data.detallesVenta;
            console.log(data);
            console.log(datosVentaVar);
            console.log(detallesVentaVar);
            var v = '';
            $.each(detallesVentaVar, function (i, v1) {
                v += '<tr>';
                v += '<td class="text-center">';
                v += v1.chrCodPro;
                v += '</td>';
                v += '<td class="text-center">';
                v += v1.chrCodPro;
                v += '</td>';
                v += '<td class="text-center">';
                v += v1.chrComPro;
                v += '</td>';
                v += '<td class="text-center">';
                v += v1.numPreVen;
                v += '</td>';
                v += '<td class="text-center">';
                v += v1.intCanPro;
                v += '</td>';
                v += '<td class="text-center">';
                v += v1.chrCodPro;
                v += '</td>';
                v += '<td class="text-center">';
                v += v1.chrCodPro;
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