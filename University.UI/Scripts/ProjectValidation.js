
$(document).on('keypress', '.txtOnly', function (event) {
    var regex = new RegExp("^[a-zA-Z ]*$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});
$(document).on('keypress', '.txtOnlyspl', function (event) {
    var regex = new RegExp("^[a-zA-Z -!@#$%&*? ]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});


$(document).ready(function () {
    //called when key is pressed in textbox
    $(".onlynumbers").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            //display error message
            $("#errmsg").html("Digits Only").show().fadeOut("slow");
            return false;
        }
    });
});


$('.number').keypress(function (event) {
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});

$('.capitalize').keyup(function (evt) {
    var txt = $(this).val();


    // Regex taken from php.js (http://phpjs.org/functions/ucwords:569)
    $(this).val(txt.replace(/^(.)|\s(.)/g, function ($1) { return $1.toUpperCase(); }));
});

('.splandalph').keypress(function (e) {
   // var regex = new RegExp("^[a-zA-Z-!@#$%&*?]+$");
    var regex = new RegExp("^[0-9@!#\$\^%&*()+=\-\[\]\\\';,\.\/\{\}\|\":<>\? ]");
    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (regex.test(str)) {
        return true;
    }

    e.preventDefault();
    return false;
});
