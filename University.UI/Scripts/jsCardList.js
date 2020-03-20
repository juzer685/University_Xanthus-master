

$(document).ready(function () {
    //document.getElementById('loaderring').style.display = "none";
    RegisterHandler();
});

function RegisterHandler() {

    $('.btnPaymentGateway').click(function () {

       
            var Obj = {
               
                //Amount: parseFloat($('.amount').html()),
                Amount: $(this).parent().find(".amount").val().replace("$",""),
                CVV: $(this).prev().find(".datacvv").val(),
                CardNumber: $(this).parent().parent().parent().find('.dataCardNumber').html(),
                isProductbuy: $("input[name='radioCardNumber']:checked").siblings('.isProductbuy').val(),
                isbuy: $("input[name='radioCardNumber']:checked").siblings('.isbuy').val(),
                VideoId: $("input[name='radioCardNumber']:checked").siblings('.VideoId').val(),
                ProductId: $("input[name='radioCardNumber']:checked").siblings('.ProductId').val(),
                SubCatID: $("input[name='radioCardNumber']:checked").siblings('.SubCatID').val(),
                createby: $("input[name='radioCardNumber']:checked").siblings('.createby').val(),
                ProductName: $("input[name='radioCardNumber']:checked").siblings('.ProductName').val(),
                CustomerFName: $("input[name='radioCardNumber']:checked").siblings('.CustomerFName').val(),
                CardHolderName: $("input[name='radioCardNumber']:checked").siblings('.CardHolderName').val(),
                Month: $("input[name='radioCardNumber']:checked").siblings('.Month').val(),
                Year: $("input[name='radioCardNumber']:checked").siblings('.Year').val(),
                CustomerProfileId: $("input[name='radioCardNumber']:checked").siblings('.CustomerProfileId').val(),
                CustomerPaymentProfileId: $("input[name='radioCardNumber']:checked").siblings('.CustomerPaymentProfileId').val(),

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

