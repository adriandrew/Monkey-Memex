var Skip = 0;
var Take = 3; 
function Load(Skip, Take) {

    showLoad();

    //return new records from server
    $.ajax({
        type: "POST",
        url: "Default.aspx/MostrarImagenes3",
        data: "{ Skip:" + Skip + ", Take:" + Take + " }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                $('#ContentPlaceHolder_idImagenes').append(data.d);
            }
            hideLoad();
        },
        error: function (data) {
            hideLoad();
        }

    });

};

$(document).ready(function () {
    Load(Skip, Take);

    //When scroll down we attach a function to fire the Load function
    $(window).scroll(function () {

        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            if (Skip == 0) {
                Skip = Take;
            }
            else {
                Skip = Skip + Take;
            }
            Load(Skip, Take);
            
        }
    });

});
