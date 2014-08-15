var posicionImagenes = 0;

var cantidadImagenes = 2;

function CargarContenido ( posicionImagenes, cantidadImagenes ) {

    MostrarCarga();

    // Inyecta los nuevos registros devueltos del servidor. 

    $.ajax ( {

        type: "POST",

        url: "Default.aspx/MostrarImagenes",

        data: "{ posicionImagenes:" + posicionImagenes + ", cantidadImagenes:" + cantidadImagenes + " }",

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        success: function ( data ) {

            if ( data != "" ) {

                $ ( '#imagenes' ) .append ( data.d );

            }

            OcultarCarga();

        },

        error: function (data) {

            OcultarCarga();

        }

    } );

};

function MostrarCarga() {

    $ ( '#divLoadProgress' ).remove();

    var $loadDiv = $('<div class="modalDialog" id="divLoadProgress" style="display: none;"><div><img src="Images/loading.gif"></div></div>').appendTo('body');

    $loadDiv.fadeIn();

}

function OcultarCarga() {

    $ ( '#divLoadProgress' ).remove();

}

function ControlarScroll() {

    // Cuando el scroll baja se invoca la funcion CargarContenido.

    $ ( window ) .scroll ( function () {        
        
        if ( $ ( window ) .scrollTop() == $ ( document ).height() - $ ( window ).height() ) {

            if ( posicionImagenes == 0 ) {

                posicionImagenes = cantidadImagenes;

            }
            else {

                posicionImagenes = posicionImagenes + cantidadImagenes;

            }

            CargarContenido ( posicionImagenes, cantidadImagenes );
            
        }
        
    });

}

$ ( document ) .ready ( function () {
   
    CargarContenido ( posicionImagenes, cantidadImagenes );

    ControlarScroll();

} );