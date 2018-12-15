$(document).on("ready", function () {

    $('.carousel[data-type="multi"] .item').each(function () {
        var next = $(this).next();
        if (!next.length) {
            next = $(this).siblings(':first');
        }
        next.children(':first-child').clone().appendTo($(this));
        for (var i = 0; i < 4; i++) {
            next = next.next();
            if (!next.length) {
                next = $(this).siblings(':first');
            }
            next.children(':first-child').clone().appendTo($(this));
        }
    });

    //$(':checkbox:checked').prop('checked', false);

    $("#myModal").wizard({
        onfinish: function () {
            console.log("Hola mundo");
        }
    });

    $.ajax({
        type: "GET",
        url: "/Home/GetInsuranceType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.result, function (index, item) {
                $('#insuranceType').append($('<option>', {
                    value: item.ID,
                    text: item.Name
                }));
            });
        }
    });

    $.ajax({
        type: "GET",
        url: "/Home/GetCompany",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.result, function (index, item) {
                $(".carousel-inner").append("<div class='item active'><div class='col-md-2 col-sm-6 col-xs-12'><a href='#'><img src="+item.Icon+" class='img-responsive' alt="+item.Name+"/></a></div></div>");
            });
        }
    });

    $(document).on('click', '.mycheckbox', function () {
        var insureValue = parseInt($('#VehicleValue').val());
        var total = 0;
        $(this).closest('tr').find('input[type=checkbox]:checked').each(function () {
            total += isNaN((parseInt($(this).val())/100)*insureValue) ? 0 : (((parseInt($(this).val())/100)*insureValue));
        });
        var iTotal = $(this).closest('tr').find('.iTotal');
        iTotal.text('N' + total.toFixed(2));
    });

    $('#VehicleValue').change(function () {
        $(this).closest('tr').find('input[type=checkbox]').removeAttr('checked');
        $('input:checkbox').removeAttr('checked');
        var iTotal = $('span.iTotal');
        iTotal.text('');
    });
});

$(document).ready(function(){

    $('#search-result').hide();
    $('.processing').hide();
        
    $("#inquiryForm").validate({
        rules: {
            insuranceType: {
                required: true
            },
            insuranceCategory: {
                required: true
            },
            duration: {
                required: true
            },
            vehicleValue: {
                required: true,
                number: true,
                digits: true
            }
        },
        messages: {
            insuranceType: "Please select an Insurance Type",
            insuranceCategory: "Please select a Category",
            duration: "Please enter the duration",
            vehicleValue: {
                required: "Please enter your vehicle value",
                number: 'Please enter only digits.',
            }
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function(error, element) {
            if(element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        },
        highlight: function ( element, errorClass, validClass ) {
            $(element).parents(".input-field").addClass("has-error").removeClass("has-success");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents(".input-field").addClass("has-success").removeClass("has-error");
        },
        submitHandler: function (form) {
            inquirydata = $('#inquiryForm').serialize();
            $.ajax({
                type: "POST",
                url: '/Home/GetInsuranceTypeComponent',
                data: inquirydata,
                dataType: "json",
                beforSend: function (data) {
                    $('#search-found').hide();
                    $('.processing').show();
                },
                success: function (data) {
                    
                    let newArr = data.result.reduce((acc, curr) => {
                        if (acc.some(obj => obj.Company.ID === curr.Company.ID)) {
                            acc.forEach(obj => {
                                if (obj.Company.ID === curr.Company.ID) {
                                    obj.Company.Name = obj.Company.Name;
                                    obj.Components.Name = obj.Components.Name + "," + curr.Components.Name;
                                    obj.Percentage = obj.Percentage + "," + curr.Percentage;
                                }
                            });
                        } else {
                            acc.push(curr);
                        }
                        return acc;
                    }, []);

                    $(".componentsTable tr").remove();
                    //$(".componentsTable th").remove();
                    //console.log(newArr[0].InsuranceType.ID)
                    if (newArr[0].InsuranceType.ID == $('#insuranceType').val()) {
                        var header = newArr[0].Components.Name;
                        $.each(header.split(','), function (i, h) {
                            $('.no-border').find("th:last").append('<th class="text-center">' + $.trim(h) + '</th>');
                        });

                        $.each(newArr, function (k1, v1) {
                            var percentages = v1.Percentage;
                            $('.no-border-y').append('<tr><td>' + v1.Company.Name + '</td></tr>');
                            $.each(percentages.split(','), function (i, v) {
                                $('.no-border-y').find("td:last").after("<td class='text-center primary-emphasis'><input type='checkbox' name='mycheckbox' class='mycheckbox' value='" + parseFloat(v) + "' id='mycheckbox'/></td>");
                            });
                            $('.no-border-y').find("td:last").after('<td class="text-center"><span class="iTotal" style="font-size:24px;font-weight:bold;color:#158cba;"></span></td><td class="text-center"><a href="#" class="btn blue sm buynow"> BUY NOW</a></td>');
                        });
                    } else {
                        $('.no-border-y').append('<tr><td class="text-center"><h3>No Record Found</h3></td></tr>');
                    }
                },
                complete: function (data) {
                    $('#search-result').show();
                    $('.processing').hide();
                    $('html,body').animate({ scrollTop: $("#search-result").offset().top }, 'slow');
                    //console.log(data);
                },
                error: function (data) {
                    //console.log(data);
                }
            });
        }
    });

    $('#search-found').click(function(){
            
    });

    $("#retreive-vehicle-details").click(function () {
        if ($("#plateNo").val() == "") {
            alert("Please Enter Plate No");
            return;
        }
        $("#engineNo").val("123456789");
        $("#chasisNo").val("123456789");
        $("#vehicleColor").val("blue");
        $("#vehicleMake").val("toyota");
        $("#vehicleModel").val("corolla");
        $("#vehicleType").val("fallon");
        $("#yearOfMake").val("2019");
        // $("#chasisNo").val("");
    });
    $('[data-toggle="tooltip"]').tooltip();

});