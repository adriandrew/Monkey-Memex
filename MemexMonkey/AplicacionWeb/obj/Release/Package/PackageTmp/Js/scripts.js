function MuestraOculta ( id )
{

    if (document.getElementById) { // Se obtiene el id.

        var comentarioUsuario = document.getElementById(id); // Se define la variable "comentarioUsuarios" igual a nuestro div.

        comentarioUsuario.style.display = (comentarioUsuario.style.display == 'none') ? 'block' : 'none'; // Damos un atributo display:none que oculta el div.

    }

}

window.onload = function ()
{

    /* Hace que se cargue la función lo que predetermina que div estará oculto hasta llamar a la función nuevamente. */

    MuestraOculta('ContentPlaceHolder_uiPluginComentariosFacebook'); /* "ContentPlaceHolder_uiPluginComentariosFacebook" es el nombre que le dimos al DIV. */

}
