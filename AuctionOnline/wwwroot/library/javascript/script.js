//Facebook Pixel Code

!function (f, b, e, v, n, t, s) {
    if (f.fbq) return; n = f.fbq = function () {
        n.callMethod ?
            n.callMethod.apply(n, arguments) : n.queue.push(arguments)
    };
    if (!f._fbq) f._fbq = n; n.push = n; n.loaded = !0; n.version = '2.0';
    n.queue = []; t = b.createElement(e); t.async = !0;
    t.src = v; s = b.getElementsByTagName(e)[0];
    s.parentNode.insertBefore(t, s)
}
    (window, document, 'script', 'https://connect.facebook.net/en_US/fbevents.js');
fbq('init', '274749230065889');
fbq('track', 'PageView');

// End Facebook Pixel Code

// Fb Async
window.fbAsyncInit = function () {
    FB.init({
        appId: '586511584891073',
        xfbml: true,
        version: 'v2.6'
    });
};

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement(s); js.id = id;
    js.src = "/connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));
// properties get safely
function getSafe(fn) {
    try {
        return fn();
    } catch (e) {
        return undefined;
    }
}
//end fb async

// scroll list
$(function () {
    $('.topiclist').on('click', '.control-item', function (e) {
        e.stopPropagation()
        e.preventDefault()
        var button = $(this).parent().parent().parent().children('button');
        var dropdown = $(button).parent().children('.dropdown-menu');
        var url = $(this).attr('href');

        var m = url.match(/topic_id\=([0-9]+)/)
        if (m) topicID = m[1]

        $.get(url, function (data) {
            var res = $.parseJSON(data);

            if (getSafe(() => res.data.upCount))
                location.reload();
            else if (res.error)
                alert("Bạn đã hết lượt up");

            var upHTML = '<li><a href="' + url.replace('/sold=[0-9+]/', 'up=1') + '" class="control-item"><i style="margin-right:10px" class="fa fa-arrow-up"></i>Up bài</a></li>';
            var upSchedule = '<li><a href="/mua-ban/upschedule?topic_id=' + topicID + '"><i style="margin-right:10px" class="far fa-clock"></i>Đặt lịch up</a></li>';
            upHTML += upSchedule
            if (getSafe(() => res.data.sold) && res.data.sold == 1) {
                $(button).removeClass('btn-success');
                $(button).addClass('btn-default');
                $(button).html('Hết hàng <div class="caret"></div>');
                $(dropdown).html(upHTML + '<li><a href="' + url.replace('sold=1', 'sold=0') + '" class="control-item">Còn hàng</a></li>');
            }
            else {
                $(button).removeClass('btn-default');
                $(button).addClass('btn-success');
                $(button).html('Còn hàng <div class="caret"></div>');
                $(dropdown).html(upHTML + '<li><a href="' + url.replace('sold=0', 'sold=1') + '" class="control-item">Hết hàng</a></li>');
            }
        })
    });
})

$(function () {
    $('#mainTopiclist').infiniteScroll({
        path: '#nextpage',
        append: '.item.grid',
        history: false,
    })
})
//end scroll list

//sidebar
function sideBarToggle() {
    $('#sidebar').toggle()
    // show sidebar
    if ($('#sidebar').is(":visible")) {
        $('#overlay').fadeIn()
        $('#wrapper').css("position", "absolute")
        $('#wrapper').css("width", "100%")
        $('#hamburger-close').show()
        $('#hamburger-hamburger').hide()
    }
    // hide sidebar
    else {
        $('#overlay').fadeOut()
        $('#wrapper').css("position", "static")
        $('#wrapper').css("width", "auto")
        $('#hamburger-close').hide()
        $('#hamburger-hamburger').show()
    }
}
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});
$(function () {
    $("#marketsearch").submit(function (event) {
        var keyword = $('input[name="keyword"]').val().replace(/\s+/g, '+')
        var catId = $('select[name="cat_id"]').val()
        var price = $('select[name="price"]').val()
        var url = '/toan-quoc/search/' + keyword

        var params = {}
        if (catId && catId != 0) params['cat_id'] = catId
        if (price && price != 0) params['price'] = price
        if (params) url += '?' + jQuery.param(params);
        location.href = url
        event.preventDefault()
    })
})
//endsidebar

// change seller, city
$(function () {
    $('.change-seller').on('click', function (e) {
        var seller = $(this).attr('data-value');
        if (!seller) $.removeCookie('seller', { path: '/' });
        else $.cookie('seller', seller, { expires: 30, path: '/' });
    });
    $('.change-city').on('click', function (e) {
        var city = $(this).attr('data-value');
        if (!city) $.removeCookie('city', { path: '/' });
        else $.cookie('city', city, { expires: 30, path: '/' });
    });
});

//////////////////////////////
window.dataLayer = window.dataLayer || [];
function gtag() { dataLayer.push(arguments); }
gtag('js', new Date());

gtag('config', 'UA-1594947-31');


//details page 
function saveProduct() {
    $.ajax({
        url: "https://sohot.vn/mua-ban/save?id=110",
        context: document.body
    }).done(function () {
        alert("Tin mua bán đã được lưu thành công")
    });
}
function changeBigPhoto(no, t) {
    $('.slick').slick('slickGoTo', no);
    $('#thumblist .thumb').removeClass('selected')
    $(t).parent().addClass('selected')
}
function up(topicID) {
    url = 'https://sohot.vn/market/submit?target=topic&up=1&topic_id=' + topicID
    $.get(url, function (data) {
        var res = $.parseJSON(data);

        if (getSafe(() => res.data.upCount))
            alert("Chủ đề đã đuợc up thành công")
        else if (res.error)
            alert("Bạn đã hết lượt up");
    }
    )
}

$(function () {
    $('#openMap').click(function () {
        var addr = $(this).text()
        $("#map iframe").attr('src', '//www.google.com/maps/embed/v1/place?q=' + encodeURIComponent(addr) + '&key=AIzaSyArNP5avnQhme2_X3kkFh8ek_BgKlajtZ0')
        $("#map").modal()
    });
    if ($('body').innerWidth() > 980) {
        $('#seller-info').affix({
            offset: {
                top: 0,
                bottom: function () {
                    return (this.bottom = $(document).height() - $('#related-topics').offset().top + 40)
                }
            }
        })
    } else {
        // remove spying
        $('#seller-info').removeAttr('data-spy');
    }
    // enable slick
    $('.slick').slick({
        lazyLoad: 'ondemand',
        dots: false,
        infinite: true,
        arrows: true,
        speed: 300,
        slidesToShow: 1,
        slidesToScroll: 1
    });
    $('.slick').on('afterChange', function (event, slick, currentSlide) {
        $('.slick-counter').html((currentSlide + 1) + ' / ' + $(".slick").slick("getSlick").slideCount);
    });
});

//end details page

//remember me
function rememberme(a) {
    if ($('#remember').is(":checked"))
        $('#facebooklogin').attr('href', 'https://sohot.vn/user/action/login?facebook=1&remember=1');
    else
        $('#facebooklogin').attr('href', 'https://sohot.vn/user/action/login?facebook=1');
}

//approvecheckitem
function itemApproveWaiting() {
    alert("Item created successfully! Your item will be approved within 24 hours.");
}
