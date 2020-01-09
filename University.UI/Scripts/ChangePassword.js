$(document).ready(function () {
    RegisterHandler();
});

function RegisterHandler() {
    $('#ChangePassForm').submit(function () {
        return false;
    });

    $('#btnChangePass').click(function () {
        var NewPass = $('input[name=NewPassword]').val();
        var ConfirmPass = $('input[name=ConfirmPassword]').val();

        if (NewPass == ConfirmPass) {
            $.ajax({
                type: "POST",
                url: "/Login/ChangePassword",
                data: $('#ChangePassForm').serialize(),
                success: function (result) {
                    alert(result.Message);
                    //_showSuccessMessage(result.Message);
                    setTimeout(function () { window.location.href = result.url; }, 1000);
                }
            });
        }
        else {
            _showSuccessMessage("Password and Confirm Password does not match");
        }
    });
}