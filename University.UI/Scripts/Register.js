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
                    success: function (result) {
                        _showSuccessMessage(result.Message);
                        document.getElementById('loaderring').style.display = "none";
                        setTimeout(function () { window.location.href = result.url; }, 1000);
                    }
                });
            }
            else {
                _showErrorMessage("Please agree to terms");
            }
        }
    });
}