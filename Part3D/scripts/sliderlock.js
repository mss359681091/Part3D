$(document).ready(function () {
    $(".chkhk").attr("disabled", "disabled");
});


function refreshSwatch() {
    debugger
    $SliderValue = $('.sliderLock').slider("value");
    if ($SliderValue == 100) {
        $('.sliderLock').slider("value", 86);
        $(".chkhk").removeAttr("disabled");
        $(".slider_tip").text("通过验证√").css("color", "green");
        $(".sliderLock").unbind();
        return;
    }
}
$(function () {
    $(".sliderLock").slider({
        change: refreshSwatch
    });
    // 上面说的你要是直接使用官方的ui js 文件, 要加入以代码
    // $("#slider").html('<span class="glyphicon glyphicon-arrow-right"></span>');
});