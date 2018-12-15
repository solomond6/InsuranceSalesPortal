$(document).ready(function () {

    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^]*)').exec(window.location.href);
        if (results == null) {
            return null;
        }
        else {
            return results[1] || 0;
        }
    }

    $('#search-result').hide();
    $('.processing').hide();

    $("#login-form").validate({
        rules: {
            EmailAddress: {
                required: true,
                email: true
            },
            Password: {
                required: true,
                minlength: 8
            },
        },
        messages: {
            EmailAddress: {
                required: "Your Email Address is required.",
                email: 'Please enter a valid email address.',
            },
            Password: {
                required: "Please enter your password",
                minlength: 'Minimum password length is 8.',
            },
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).parents(".input-field").addClass("has-error").removeClass("has-success");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents(".input-field").addClass("has-success").removeClass("has-error");
        },
        submitHandler: function (form) {
            inquirydata = $('#login-form').serialize();
            $.ajax({
                type: "POST",
                url: '/Users/PostUserLogin',
                data: inquirydata,
                dataType: "json",
                beforSend: function (data) {
                    $('#search-found').hide();
                    $('.processing').show();
                },
                success: function (data) {
                    console.log(data);
                    if (data.result == "Ok") {
                        $(".processing").show().text("Redirecting");
                        if (!$.urlParam('ReturnUrl')) {
                            window.location.replace('/users/myprofile');
                        } else {
                            window.location.replace(decodeURIComponent($.urlParam('ReturnUrl')));
                        }
                    } else {
                        $(".processing").show().text("Email Adress/Password");
                    }
                },
                complete: function (data) {
                    //$('#search-result').show();
                    //$('.processing').hide();
                },
                error: function (data) {
                    //console.log(data);
                }
            });
        }
    });

    $('[data-toggle="tooltip"]').tooltip();

});