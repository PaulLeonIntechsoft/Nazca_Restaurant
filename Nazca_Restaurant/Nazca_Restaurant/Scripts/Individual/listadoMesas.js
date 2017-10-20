$(document).ready(function () {
    ajaxListadoMesas();
    var lst = setInterval(ajaxListadoMesas, 3000);
});

function ajaxListadoMesas() {
    $.ajax({
        url: 'MesasBind',
        data: {},
        type: 'POST',
        success: function (data) {
            var v = '';
            v += '<div id="accordion" role="tablist">';
            $.each(data, function (i, v1) {
                v += '<div class="card">';
                /**/ v += '<div class="card-header" role="tab" id="heading' + i + '">';
                /*----*/ v += '<h5 class="mb-0">';
                /*--------*/ v += '<a data-toggle="collapse" href="#collapse' + i + '" aria-expanded="false" aria-controls="collapse' + i + '">';;
                /*------------*/ v += v1.chrDesMesa;
                /*--------*/ v += '</a>';
                /*----*/ v += '</h5>';
                /**/ v += '</div>';
                /**/ v += '<div id="collapse'+ i +'" class="collapse show" role="tabpanel" aria-labelledby="heading' + i + '" data-parent="#accordion">';
                /*----*/ v += '<div class="card-body">';
                /*--------*/ v += 'Mozo : ' + v1.chrNomMoz + ' ' + v1.chrApeMoz + '<br>';
                /*--------*/ v += 'Inicio : ' + v1.chrHorVen;
                /*----*/ v += '</div>';
                /**/ v += '</div>';
                v += '</div>';
            });
            v += '</div>';
            $("#listaMesas").html(v);
        },
        error: function (xhr, statusText, err) {
            alert("error" + xhr.status);
        }
    });
}