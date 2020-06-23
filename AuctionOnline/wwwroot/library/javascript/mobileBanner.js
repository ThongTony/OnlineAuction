class MobileBanner {
    constructor() {
        this.bannerFile =
            "https://artisancollective.co.nz/media/uploads/2019_01/LIMERENCE_SKY_3_gQkdzsI.jpg.1200x780_q75_crop_upscale.jpg"
        this.iosLink =
            "https://itunes.apple.com/us/app/sohot-si%C3%AAu-th%E1%BB%8B-n%E1%BB%99i-th%E1%BA%A5t/id1447511881?ls=1&mt=8"
        this.androidLink = "https://play.google.com/store/apps/details?id=com.sohot"
    }

    getMobileOS() {
        var userAgent = navigator.userAgent || navigator.vendor || window.opera
        if (/windows phone/i.test(userAgent)) {
            return "Windows Phone"
        }

        if (/android/i.test(userAgent)) {
            return "Android"
        }

        if (/iPad|iPhone|iPod/.test(userAgent) && !window.MSStream) {
            return "iOS"
        }

        return "unknown"
    }

    storeLink() {
        var os = this.getMobileOS()
        if (os == "Android") return this.androidLink
        else if (os == "iOS") return this.iosLink
    }

    render() {
        if (!this.storeLink()) return ""

        return (
            "<a href='" +
            this.storeLink() +
            "' target='_blank'><img src='" +
            this.bannerFile +
            "'></a>"
        )
    }
}

var bn = new MobileBanner()
if (bn.render()) document.writeln(bn.render())