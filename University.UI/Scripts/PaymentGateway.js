$(document).ready(function () {
    //document.getElementById('loaderring').style.display = "none";
    RegisterHandler();
});

function RegisterHandler() {
    $('#PaymentGatewayForm').submit(function () {
        return false;
    });

    $('#btnPaymentGateway').click(function () {
        //var formValid = $('#PaymentGatewayForm').validate().form();
        //if (!formValid)
        //    return false;
        //else {
            //if ($('#agreeTerms').is(':checked')) {
                //document.getElementById('loaderring').style.display = "block";
                $.ajax({
                    type: "POST",
                    url: "/PaymentGateway/PaymentInfo",
                    data: $('#PaymentGatewayForm').serialize(),
                    success: function (data) {
                        if (data.result == true) {
                            _showSuccessMessage(data.Message);
                            //document.getElementById('loaderring').style.display = "none";
                            //setTimeout(function () { window.location.href = data.url; }, 2000);
                        }
                        else {
                            _showErrorMessage(data.Message);
                            //document.getElementById('loaderring').style.display = "none";
                            //setTimeout(function () { window.location.href = data.url; }, 2000);
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