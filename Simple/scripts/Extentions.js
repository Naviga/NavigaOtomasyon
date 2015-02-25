var enmPozisyonlar = { "Sol": 1, "Orta": 2, "Sag": 3, "OrtaSol": 4, "OrtaSag": 5, "OrtaUstSol": 9, "OrtaUstSag": 10, "OrtaUst": 11, "OrtaAlt": 12, "OrtaAltSol": 13, "OrtaAltSag": 14, "Ust": 15, "UstSol": 16, "UstOrta": 17, "UstSag": 18, "UstAlt": 19 };

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

$.fn.ToDate = function () {
    var t1 = this.replace("T", " ");
    var t2 = t1.split(' ');

    var time = t2[1];
    var hour = time.split(':')[0];
    var min = time.split(':')[1];

    var t3 = t2[0].split('-');

    var year = t3[0];
    var month = t3[1] - 1;
    var day = t3[2];

    var rndTar = new Date(year, month, day, hour, min);
    var sonuc = rndTar.format('dd.MM.yyyy');

    return sonuc;
}

String.prototype.replaceAll = function (eski, yeni) {

    var sonuc = "";

    var arr = this.toString().split('');

    for (var i = 0; i < arr.length; i++) {
        sonuc += arr[i].replace(eski, yeni);
    }

    return sonuc;
}

String.prototype.ToUrl = function () {

    var sonuc = this.toString();

    sonuc = sonuc.split(' ').join('-');
    //sonuc = sonuc.replace(' ', '-');
    sonuc = sonuc.replaceAll("ç", "c");
    sonuc = sonuc.replaceAll("ş", "s");
    sonuc = sonuc.replaceAll("ı", "i");
    sonuc = sonuc.replaceAll("ö", "o");
    sonuc = sonuc.replaceAll("ü", "u");
    sonuc = sonuc.replaceAll("ğ", "g");
    sonuc = sonuc.replaceAll("Ç", "C");
    sonuc = sonuc.replaceAll("Ş", "S");
    sonuc = sonuc.replaceAll("İ", "I");
    sonuc = sonuc.replaceAll("Ö", "O");
    sonuc = sonuc.replaceAll("Ü", "U");
    sonuc = sonuc.replaceAll("Ğ", "G");

    sonuc = sonuc.replaceAll('?', '-');
    sonuc = sonuc.replaceAll('/', '-');
    sonuc = sonuc.replaceAll('\\', '-');
    sonuc = sonuc.replaceAll('(', '-');
    sonuc = sonuc.replaceAll(')', '-');
    sonuc = sonuc.replaceAll("&", "");
    sonuc = sonuc.replaceAll("'", "");
    sonuc = sonuc.replaceAll('.', '-');
    sonuc = sonuc.replaceAll("--", "-");
    sonuc = sonuc.replaceAll("---", "-");

    return sonuc;
}

//Extension method - 2013-04-19T11:22:00 Formatını 19.04.2013 olarak verir
//String.prototype.ToDate = function () {

//    var t1 = this.replace("T", " ");
//    var t2 = t1.split(' ');

//    var time = t2[1];
//    var hour = time.split(':')[0];
//    var min = time.split(':')[1];

//    var t3 = t2[0].split('-');

//    var year = t3[0];
//    var month = t3[1] - 1;
//    var day = t3[2];

//    var rndTar = new Date(year, month, day, hour, min);
//    var sonuc = rndTar.format('dd.MM.yyyy');

//    return sonuc;
//};
//Extension method - 2013-04-19T11:22:00 Formatını 22:00 olarak verir
String.prototype.ToTime = function () {

    var t1 = this.replace("T", " ");
    var t2 = t1.split(' ');

    var time = t2[1];
    var hour = time.split(':')[0];
    var min = time.split(':')[1];

    var t3 = t2[0].split('-');

    var year = t3[0];
    var month = t3[1];
    var day = t3[2];

    var rndTar = new Date(year, month, day, hour, min);

    return rndTar.getHours() + ":" + rndTar.getMinutes();
};
//Extension method - 2013-04-19T11:22:00 Formatını 19.04.2013 22:00 olarak verir
String.prototype.ToDateTime = function () {

    var t1 = this.replace("T", " ");
    var t2 = t1.split(' ');

    var time = t2[1];
    var hour = time.split(':')[0];
    var min = time.split(':')[1];

    var t3 = t2[0].split('-');

    var year = t3[0];
    var month = t3[1] - 1;
    var day = t3[2];

    var rndTar = new Date(year, month, day, hour, min);
    var sonuc = rndTar.format('dd.MM.yyyy hh.mm');

    return sonuc;
};

String.prototype.ToMoney = function () {

    var sonuc = this.toString();
    var cultInf = $("#inpCultureInfo").val();

    if (cultInf == "tr-TR") {
        sonuc = accounting.formatMoney(sonuc, "TL", 2, ".", ",", "%v%s");
    }

    if (cultInf == "en-US") {
        sonuc = accounting.formatMoney(sonuc, "$", 2, ",", ".", "%s%v")
    }

    if (cultInf == "de-DE") {
        sonuc = accounting.formatMoney(sonuc, "€", 2, ".", ",", "%s%v")
    }

    if (cultInf == "en-GB") {
        sonuc = accounting.formatMoney(sonuc, "£", 2, ",", ".", "%s%v")
    }


    return sonuc;
}

String.prototype.IsNumber = function () {
    return !isNaN(parseFloat(this)) && isFinite(this);
}

function getIEVersion()
    // Returns the version of Internet Explorer or a -1
    // (indicating the use of another browser).
{
    var rv = -1; // Return value assumes failure.
    if (navigator.appName == 'Microsoft Internet Explorer') {
        var ua = navigator.userAgent;
        var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
        if (re.exec(ua) != null)
            rv = parseFloat(RegExp.$1);
    }
    return rv;
}
function checkVersion() {
    var msg = "You're not using Internet Explorer.";
    var ver = getIEVersion();

    if (ver > -1) {
        if (ver >= 8.0)
            msg = "You're using a recent copy of Internet Explorer."
        else
            msg = "You should upgrade your copy of Internet Explorer.";
    }
    alert(msg);
}
var isMobile = {
    Android: function () {
        return navigator.userAgent.match(/Android/i);
    },
    BlackBerry: function () {
        return navigator.userAgent.match(/BlackBerry/i);
    },
    iPhone: function () {
        return navigator.userAgent.match(/iPhone/i);
    },
    iPad: function () {
        return navigator.userAgent.match(/iPad/i);
    },
    iPod: function () {
        return navigator.userAgent.match(/iPod/i);
    },
    iOS: function () {
        return navigator.userAgent.match(/iPhone|iPad|iPod/i);
    },
    Opera: function () {
        return navigator.userAgent.match(/Opera Mini/i);
    },
    Windows: function () {
        return navigator.userAgent.match(/IEMobile/i);
    },
    any: function () {
        return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
    }
};