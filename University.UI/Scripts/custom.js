$(document).ready(function () {

    $(document).on("click", "#smartSearch", function () {
        var text = $("[aria-label='Smart Search']").val();
        if (text) {
            var form = $(document.createElement('form'));
            $(form).attr("action", "/Home/SmartSearch");
            $(form).attr("method", "POST");

            var input = $("<input>")
                .attr("type", "hidden")
                .attr("name", "freeText")
                .val(text);


            $(form).append($(input));

            form.appendTo(document.body)

            $(form).submit();
        }

    });

    $(document).on("click", "#smartSearch_home", function () {
        var text = $("[aria-label='Smart Search Home']").val();
        if (text) {
            var form = $(document.createElement('form'));
            $(form).attr("action", "/Home/SmartSearch");
            $(form).attr("method", "POST");

            var input = $("<input>")
                .attr("type", "hidden")
                .attr("name", "freeText")
                .val(text);


            $(form).append($(input));

            form.appendTo(document.body)

            $(form).submit();
        }

    });


    $(".backToTopPos").click(function (event) {
        event.preventDefault();
        $("html, body").animate({ scrollTop: 0 }, "slow");
        return false;
    });

    $(".sidebar-dropdown > a").click(function () {

        //$(".sidebar-submenu").slideUp(200);

        //if ($(this).parent().hasClass("active")) {
        //    $(this).parent().removeClass("active");
        //    $(this).parent().removeClass("active");
        //    $(this).next(".sidebar-submenu").slideUp(200);
        //} else {
        //    $(this).parent().removeClass("active");
        //    $(this).next(".sidebar-submenu").slideDown(200);
        //    $(this).parent().addClass("active");
        //}
        if ($(this).parent().hasClass("Parent")) {
            if ($(this).parent().hasClass("active")) {
                //$(this).parent().removeClass("active");
                $(this).parent().removeClass("active");
                $(this).next(".sidebar-submenu").slideUp(200);
            } else {
                $(".sidebar-dropdown.Parent.active").removeClass("active");
                $(".sidebar-submenu").slideUp(200);
                $(".sidebar-dropdown.FirstChild.active").removeClass("active");
                //$(this).parent().removeClass("active");
                $(this).next(".sidebar-submenu").slideDown(200);
                $(this).parent().addClass("active");
            }
        }
        else {
            if ($(this).parent().hasClass("active")) {
                //$(this).parent().removeClass("active");
                $(this).parent().removeClass("active");
                $(this).next(".sidebar-submenu").slideUp(200);
            } else {
                $(".sidebar-dropdown.FirstChild.active").removeClass("active");
                $(".FirstChild .sidebar-submenu").slideUp(200);
                //$(this).parent().removeClass("active");
                $(this).next(".sidebar-submenu").slideDown(200);
                $(this).parent().addClass("active");
            }
        }

    });


    //$("#close-sidebar").click(function () {
    //    $(".page-wrapper").removeClass("toggled");
    //});
    ////$('#sidebar').mouseleave(function () {
    ////    $(".page-wrapper").removeClass("toggled");
    ////});
    //$("#show-sidebar").click(function () {
    //    $(".page-wrapper").toggleClass("toggled");
    //});


    $(document).on('click', '#close-sidebar', function () {
        $(".page-wrapper").removeClass("toggled");
    });
    $(document).on('click', '#show-sidebar', function () {
        $(".page-wrapper").toggleClass("toggled");
    });


    $(document).ready(function () {

        var _URL = window.URL || window.webkitURL;

        $(document).on("change", "input[type='file'].validateHeightWidth", function (e) {
            var file = $(this)[0].files[0];
            var _this = $(this);
            img = new Image();
            var imgwidth = 0;
            var imgheight = 0;
            var maxwidth = 250;
            var maxheight = 250;

            img.src = _URL.createObjectURL(file);
            img.onload = function () {
                imgwidth = this.width;
                imgheight = this.height;

                if (imgwidth == maxwidth && imgheight == maxheight) {
                    return true;
                } else {
                    $('.overlaySizeAlert').css({ "visibility": "visible", "opacity": "1" });
                    //alert("This image does not meet the size and format requirements. Please choose another image and try again");
                    $(_this).val('');
                    $(_this).parent().parent().parent().find('img.imgStd').attr('src', null);
                   // $(_this).parent().parent().find('input.iamgeselect').attr('input', null);
                   
                    $("#DeleteImage").hide();
                }
            };
            img.onerror = function () {

                //$("#response").text("not a valid file: " + file.type);
            }

        });
    });


    //$(document).on("change", "input[type='file']", function (e) {
    //    //Do something
    //    e.preventDefault();
    //    e.stopPropagation();
    //    return false;
    //});

    //$(document).ready(function () {

    //    var _URL = window.URL || window.webkitURL;

    //    $(document).on("change", "input[type='file'].validateHeightWidth", function (e) {
    //        console.log("custom-file-input");
    //        var file = $(this)[0].files[0];
    //        var _this = $(this);
    //        img = new Image();
    //        var imgwidth = 0;
    //        var imgheight = 0;
    //        var maxwidth = 250;
    //        var maxheight = 250;

    //        img.src = _URL.createObjectURL(file);
    //        img.onload = function () {
    //            imgwidth = this.width;
    //            imgheight = this.height;

    //            if (imgwidth == maxwidth && imgheight == maxheight) {
    //                return true;
    //            } else {
    //                //_showImageSizeValidationMessage();
    //               // $("#image-preview").attr('src', null);
    //                $('.overlaySizeAlert').css({ "visibility": "visible", "opacity": "1" });
    //                //alert("This image does not meet the size and format requirements. Please choose another image and try again");
    //                $(_this).val('');
    //                $(_this).parent().find('img.imgStd').attr('src', null);
    //                $("#DeleteImage").hide();
    //            }
    //        };
    //        img.onerror = function () {

    //            //$("#response").text("not a valid file: " + file.type);
    //        };
    //    });
    //});

    $(document).on('click', '.overlay', function () {
        $('.MainVideo').attr("src", $(this).siblings('iframe').attr("src") + "?autoplay=1");
        $('.MainVideo').attr("allow", "autoplay");
        $('.MainVideo').siblings('div').html($(this).closest('a').next().html());
        $('.MainVideo').siblings('div').html($(this).closest('a').next().html());
    });

});


//function _showImageSizeValidationMessage() {
//    $(document).Toasts('create', {
//        class: 'bg-danger',
//        title: 'Image Size Validation',
//        autohide: true,
//        delay: 5000,
//        body: 'This image does not meet the size and format requirements. Please choose another image and try again'
//    });
//}

function _showSuccessMessage(message) {
    //$(document).Toasts('create', {
    //    class: 'bg-success',
    //    title: 'Success',
    //    //subtitle: 'Success Notification',
    //    body: message,//'Saved Successfully.'
    //});
    toastr.success(message);
}