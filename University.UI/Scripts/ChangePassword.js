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

        if (NewPass == '' && ConfirmPass == '') {
            //alert("Please enter Password and Confirm Password");
        }
        else if (NewPass == ConfirmPass) {
            $.ajax({
                type: "POST",
                url: "/Login/ChangePassword",
                data: $('#ChangePassForm').serialize(),
                success: function (result) {
                    _showSuccessMessage(result.Message);
                    //alert(result.Message);
                    setTimeout(function () { window.location.href = result.url; }, 1000);
                }
            });
        }
        else {
            _showErrorMessage("Password and Confirm Password does not match");
            //alert("Password and Confirm Password does not match");
        }
    });
}