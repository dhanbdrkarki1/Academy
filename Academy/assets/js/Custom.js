$(document).ready(function () {
    console.log("yes");
    var url = window.location;
    $('.navbar .nav li').find('.active').removeClass('active');
    $('.navbar .nav li a').each(function () {
        if (this.href == url) {
            $(this).addClass('active');
            console.log("clicked");
        }
    });
});