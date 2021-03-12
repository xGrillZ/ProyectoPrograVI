$(function () {
    owlcarousel();
});

function owlcarousel() {
    $('.owl-carousel').owlCarousel({
        loop: true,
        autoplay: true,
        autoplayTimeout: 2000,
        autoplayHoverPause: true,
        items: 1
    });
}