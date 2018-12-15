$(document).ready(function(){
        $('.Insurance-logo-carousel').slick({
            slidesToShow:5,
            infinite:true,
            arrows:false,
            autoplay:true,
            autoplaySpeed:2000,
            speed:600,
            pauseOnHover:true,
            responsive:[{breakpoint:1000,settings:{slidesToShow:4}},
            {breakpoint:800,settings:{slidesToShow:3}},
            {breakpoint:600,settings:{slidesToShow:2}},
            {breakpoint:400,settings:{slidesToShow:1}}
            ]
    });
    //$('[data-toggle="tooltip"]').tooltip(); 
});