$(document).ready(function () {
    document.getElementById('loaderring').style.display = "none";
    RegisterHandler();
});


$(document).ready(function () {
    //called when key is pressed in textbox
    $(".Password").keyup(function (e) {
       
            var p = document.getElementById('newPassword').value,
                errors = [];
            if (p.length < 8) {
                errors.push("Your password must be at least 8 characters");
            }
            if (p.search(/[a-z]/i) < 0) {
                errors.push("Your password must contain at least one letter.");
            }
            if (p.search(/[0-9]/) < 0) {
                errors.push("Your password must contain at least one digit.");      
            }
            if (errors.length > 0) {
                alert(errors.join("\n"));
                return false;
            }
            return true;
       
    });
});


function RegisterHandler() {
    $('#UserRegistrationForm').submit(function () {
        return false;
    });

    $('#UserRegisterationBtn').click(function () {
        var formValid = $('#UserRegistrationForm').validate().form();
        if (!formValid)
            return false;
        else {
            if ($('#agreeTerms').is(':checked')) {
                document.getElementById('loaderring').style.display = "block";
                $.ajax({
                    type: "POST",
                    url: "/Admin/User/Register",
                    data: $('#UserRegistrationForm').serialize(),
                    success: function (data) {
                        if (data.result == true) {
                            _showSuccessMessage(data.Message);
                            document.getElementById('loaderring').style.display = "none";
                            setTimeout(function () { window.location.href = data.url; }, 2000);
                        }
                        else {
                            _showErrorMessage(data.Message);
                            document.getElementById('loaderring').style.display = "none";
                            $("#Email").val("");
                            $("#Password").val("");
                            $("#ConfirmPassword").val("");
                            //setTimeout(function () { window.location.href = data.url; }, 2000);
                        }
                    }
                });
            }
            else {
                _showErrorMessage("Please agree to terms");
            }
        }
    });
}

