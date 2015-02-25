var uploaderButton = $("#aFotoSec");
var interval;
var isFileTypeOk = true;
if (uploaderButton[0]) {

    new AjaxUpload(uploaderButton, {
        action: "/services/general.asmx/uploadPhoto",
        name: "userPhoto",
        onSubmit: function (file, extension) {
            //Sadece txt uzantılı dosyalar yüklenebilecek
            if (extension == "jpg" || extension == "png") {
                isFileTypeOk = true;
                //Butonun metnini değiştiriyoruz
                uploaderButton.text("Yükleniiyor...");
                $("#imgPhotoLoader").show();
            }
            else
                isFileTypeOk = false;
        },
        beforeSend: function () {
            alert('ok');
        },
        onComplete: function (file, response) {
            window.clearInterval(interval);

            this.enable();

            uploaderButton.text("Fotoğraf Seç");

            if (response == "hata") {
                alert("Resim yüklenemedi ! Lütfen tekrar deneyin.");
            }
            else {
                YuklenenResmiGoster("/yukleme/resim/SolUst/" + file);
            }
        }
    });
}