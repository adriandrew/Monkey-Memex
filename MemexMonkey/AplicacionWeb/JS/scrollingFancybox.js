var posicionImagenes = 0;
var cantidadImagenes = 1;

function cargarContenido(posicionImagenes, cantidadImagenes) {

    mostrarCarga();

    // Inyecta los nuevos registros devueltos del servidor. 
    $.ajax({
        type: "POST",
        url: "Default.aspx/MostrarImagenes3",
        data: "{ posicionImagenes:" + posicionImagenes + ", cantidadImagenes:" + cantidadImagenes + " }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                $('#contenedorImagenes').append(data.d);
            }
            ocultarCarga();
        },
        error: function (data) {
            ocultarCarga();
        }

    });

};

function mostrarCarga() {

    $('#divLoadProgress').remove();

    var $loadDiv = $('<div class="modalDialog" id="divLoadProgress" style="display: none;"><div><img src="Images/loading.gif"></div></div>').appendTo('body');;
    $loadDiv.fadeIn();

}

function ocultarCarga() {

    $('#divLoadProgress').remove();

}

function invocarFancybox() {

    //Aplicando efectos a todos los enlaces con el id iframe.
    $("a.iframe").fancybox({
        'width': '75%',
        'height': '75%',
        'autoScale': false,
        'transitionIn': 'elastic',
        'transitionOut': 'elastic',
        //'hideOnContentClick': 'false',        
        'overlayOpacity': '.8',
        'overlayColor': 'black',
        'type': 'iframe'
    });

}

function controlarScroll() {

    // Cuando el scroll baja se invoca la funcion cargarContenido.
    $(window).scroll(function () {        
        
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {

            if (posicionImagenes == 0) {
                posicionImagenes = cantidadImagenes;
            }
            else {
                posicionImagenes = posicionImagenes + cantidadImagenes;
            }

            cargarContenido(posicionImagenes, cantidadImagenes);
            
        }

        invocarFancybox();
        
    });

}

$(document).ready(function () {
   
    invocarFancybox();

    cargarContenido(posicionImagenes, cantidadImagenes);

    controlarScroll();

});

$(window).load(function () {

    invocarFancybox();

});

