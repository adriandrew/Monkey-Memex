
function showLoad() {
    $('#divLoadProgress').remove();

    var $loadDiv = $('<div class="modalDialog" id="divLoadProgress" style="display: none;"><div><img src="Images/loading.gif"></div></div>').appendTo('body'); ;
    $loadDiv.fadeIn();
}

function hideLoad() {
    $('#divLoadProgress').remove();
}