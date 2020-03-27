$(document).ready(function () {
    document.getElementById('loaderring').style.display = "none";
    RegisterHandler();
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

