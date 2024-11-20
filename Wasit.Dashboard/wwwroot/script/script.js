$(document).ready(function () {
    // close the side bar
    $(".menu svg").click(function () {
        $(".menu").toggleClass("rotate");
        $(".side-menu").toggleClass("toggle-side-menu");
        $(".main-container").toggleClass("toggle-main-container");
    })
});


$(document).ready(function () {
    function checkWidth() {
            var windowWidth = $(window).width();
    
            if (windowWidth < 992) {
                $('.side-menu').addClass('toggle-side-menu');
                $('.main-container').addClass('toggle-main-container');
                $('.menu').addClass('rotate');
            } else {
                $(".side-menu").removeClass('toggle-side-menu');
                $(".main-container").removeClass('toggle-main-container');
                $('.menu').removeClass('rotate');
            }
        }
        $('.nav-link[data-bs-toggle="collapse"]').on('click', function() {
    
            // Handle collapse
            var target = $(this).attr('href');
            $(target).collapse('toggle');
            $('.collapse').not(target).collapse('hide');
        });
        checkWidth();
        $(window).resize(function() {
            checkWidth();
        });
        //loader
        
        $(".loader-container").show();
        setTimeout(function () {
            $(".loader-container").fadeOut();
        }, 1000);


    $(".side-menu .nav-link, .c-link , .gc-link").each(
        function () {
            if (window.location.href.includes($(this).attr('href'))) {
                $(this).addClass("active");
                $(this).parents(".child-list").addClass("show").parents(".nav-item").find(".nav-link").addClass("active");
                $(this).parents(".grandchild-list").addClass("show").parents(".nav-item").find(".nav-link").addClass("active");
            }
        }
    );

})