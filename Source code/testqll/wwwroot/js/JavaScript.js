$(document).ready(function () {
    if ($(".contentCotainer").hasClass('left') == false) {
        $(".right").css("width", "100%")
    }
    $(".sub-menu").parent('li').addClass("has-child");

    $("#main-menu").on("click", "li.has-child > a", 500, function () {
        $(this).parent('li').children(".sub-menu").toggle(500, function () {
            $(this).parent("li").children("a").toggleClass("border-hide")
        })
    })

    $("#btn-menu").click(function () {
        if ($("#left").hasClass('check-login') == false) {
            $("#left").toggle(500, function () {
                $(".right").css("width", "100%")
            })
        }
        
    })
    $("#left").hover(function () {
        $(".left::-webkit-scrollbar").css("width", "20px");
    })
    window.onscroll = function () {
        // console.info(document.getElementById("left").scroll);
    }
})