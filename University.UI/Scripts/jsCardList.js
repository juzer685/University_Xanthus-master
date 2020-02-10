$(document).ready(function () {
    //document.getElementById('loaderring').style.display = "none";
    RegisterHandler();
});

function RegisterHandler() {

    $('.btnPaymentGateway').click(function () {
        //var formValid = $('#UserRegistrationForm').validate().form();
        //if (!formValid)
        //    return false;
        //else {
        //if ($('#agreeTerms').is(':checked')) {
        //var k = $("input[name='radioCardNumber']").click(function () {
        //    if ($('input:radio[name=type]:checked').val() == "walk_in") {
        //        alert($('input:radio[name=type]:checked').val());
        //        //$('#select-table > .roomNumber').attr('enabled',false);
        //    }
        //});
        var Obj = {
            //CustomerProfileId: $('.CustomerProfileId').val(),
            //CustomerPaymentProfileId: $('.CustomerPaymentProfileId').val()
            CustomerProfileId: $("input[name='radioCardNumber']:checked").siblings('.CustomerProfileId').val(),
            CustomerPaymentProfileId: $("input[name='radioCardNumber']:checked").siblings('.CustomerPaymentProfileId').val()
        };
        //document.getElementById('loaderring').style.display = "block";
        $.ajax({
            type: "POST",
            url: "/PaymentGateway/MakePaymentUsingProfile",
            data: Obj,
            success: function (data) {
                if (data.result == true) {
                    _showSuccessMessage(data.Message);
                    //document.getElementById('loaderring').style.display = "none";
                    setTimeout(function () { window.location.href = '/Home/Index'; }, 2000);
                }
                else {
                    _showErrorMessage(data.Message);
                    //document.getElementById('loaderring').style.display = "none";
                    setTimeout(function () { window.location.href = '/Home/Index'; }, 2000);
                }
            }
        });
        //}
        //else {
        //    _showErrorMessage("Please agree to terms");
        //}
        //}
    });
}

