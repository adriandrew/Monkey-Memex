function MuestraOculta ( id )
{

    if (document.getElementById) { //se obtiene el id

        var comentarioUsuario = document.getElementById(id); //se define la variable "equipos" igual a nuestro div

        comentarioUsuario.style.display = (comentarioUsuario.style.display == 'none') ? 'block' : 'none'; //damos un atributo display:none que oculta el div

    }

}

window.onload = function ()
{

    /*hace que se cargue la función lo que predetermina que div estará oculto hasta llamar a la función nuevamente*/

    //MuestraOculta('ContentPlaceHolder_uiPluginComentariosFacebook');/* "contenido_a_mostrar" es el nombre que le dimos al DIV */

}

$(document).ready(function () {


});
