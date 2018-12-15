$(document).ready(function () {

    $('#search-result').hide();
    $('.processing').hide();

    $("#signup-form").validate({
        rules: {
            Firstname: {
                required: true
            },
            Lastname: {
                required: true
            },
            EmailAddress: {
                required: true,
                email: true
            },
            PhoneNumber: {
                required: true,
                number: true,
                minlength: 11,
                maxlength: 15,
            },
            Password: {
                required: true,
                //pwcheck: true,
                minlength: 8
            },
            Conf_Password: {
                required: true,
                equalTo: '#Password'
            },
        },
        messages: {
            Firstname: "Your Firstname is required.",
            Lastname: "Your Lastname is required.",
            EmailAddress: {
                required: "Your Email Address is required.",
                email: 'Please enter a valid email address.',
            },
            PhoneNumber: {
                required: "Please enter your vehicle value",
                number: 'Please enter a valid phone number.',
                minlength: 'Phone Number cannot be less than 11 digits.',
                maxlength: 'Phone Number cannot be more than 15 digits.',
            },
            Password: {
                required: "Please enter your password",
                minlength: 'Minimum password length is 8.',
            },
            Conf_Password: {
                required: "Please confirm your password",
                equalTo: 'Password does not match.',
            }

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
            inquirydata = $('#signup-form').serialize();
            $.ajax({
                type: "POST",
                url: '/Users/PostSignUpDetails',
                data: inquirydata,
                dataType: "json",
                beforSend: function (data) {
                    $('#search-found').hide();
                    $('.processing').show();
                },
                success: function (data) {
                    console.log(data.result);
                },
                complete: function (data) {
                    $('#search-result').show();
                    $('.processing').hide();
                },
                error: function (data) {
                    //console.log(data);
                }
            });
        }
    });

    $('[data-toggle="tooltip"]').tooltip();

});