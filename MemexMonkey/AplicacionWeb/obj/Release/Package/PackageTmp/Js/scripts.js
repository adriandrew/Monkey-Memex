function MuestraOculta ( id )
{

    // Se obtiene el id.

     if ( document.getElementById ) {

         // Se define la variable "comentarioUsuarios" igual a nuestro div.

        var comentarioUsuario = document.getElementById ( id ); 

        // Si la variable comentarioUsuario no viene nula o indefinida se aplica la propiedad.

        if ( typeof comentarioUsuario !== 'undefined' && comentarioUsuario !== null ) {

            // Damos un atributo display:none que oculta el div.

            comentarioUsuario.style.display = ( comentarioUsuario.style.display == 'none' ) ? 'block' : 'none'; 

        }

    }

}

function InvocarFancybox() {

    //Aplicando efectos a todos los enlaces con el id iframe.

    $ ( "a.iframe" ) .fancybox ( {

        'width': '75%',

        'height': '75%',

        'autoScale': false,

        'transitionIn': 'elastic',

        'transitionOut': 'elastic',

        //'hideOnContentClick': 'false',        

        'overlayOpacity': '.8',

        'overlayColor': 'black',

        'type': 'iframe'

    } );

}

window.onload = function ()
{

    // Hace que se cargue la función lo que predetermina que div estará oculto hasta llamar a la función nuevamente. 

    MuestraOculta ( 'ContentPlaceHolder_uiPluginComentariosFacebook' );

    // El parametro es el nombre que le dimos al DIV. 

}

// TODO. Intento de flappy bird al iniciar la pagina de error.
//$(document).ready(function () {

//    $("a").fancybox();

//    $("#iframe").click();

//});