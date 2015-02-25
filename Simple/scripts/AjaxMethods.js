

function AjaxPost(methodUrl, data, loaderId) {

    if (!data) {

        data = "{}";

    }
    var req = $.ajax({

        url: methodUrl,
        type: "POST",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            if (loaderId) {
                $("#" + loaderId).show();
            }

        }
    });


    req.complete(function () {
        if (loaderId) {
            $("#" + loaderId).hide();
        }
    });

    req.error(function (jqXHR, exception) {
        //var errStr = "<p>" + settings.url + "</p>";
        //$(jqXHR.url).dialog();
        $("<p>" + jqXHR.responseText + "</p>").dialog();
    });
    return req;
}
