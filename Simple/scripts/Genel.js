$(document).ready(function () {
    $(".required").click(function () {
        $(this).removeClass("required");
    });
});

function GetLogo() {

    var req = AjaxPost("/services/general.asmx/GetLogo");

    req.success(function (data) {
        if (data.d) {
            $("#imgLogo").attr("src", data.d);
        }
        else {
            $("#imgLogo").hide();
        }
    });
}

function GetNavigation() {
    var req = AjaxPost("/services/general.asmx/GetNavigation");

    req.success(function (JSON) {
        if (JSON.d) {

            var data = $.parseJSON(JSON.d);
            var htmlStr = "";

            htmlStr += '<ul class="left">';
            htmlStr += '<li class="divider"></li>';

            $.each(data, function (i, e) {

                if (e.DefaultSayfa) {
                    htmlStr += '<li><a href="' + e.Url + '">' + e.Adi + '</a></li>';
                }
                else {
                    htmlStr += '<li><a href="/' + e.Id + e.Url + '">' + e.Adi + '</a><ul><li><a href="#!">deneme</a></li></ul></li>';
                }
                htmlStr += '<li class="divider"></li>';
            });

            htmlStr += '</ul>';

            $("#sctItems").html(htmlStr);

            var mv = ($("#main-nav").width() - $("#sctItems ul").width()) / 2; //menu ortalamak için gerekli margin degeri

            $("#sctItems ul").css("margin-left", mv + "px");

            //htmlStr = htmlStr.replace('class="left"', 'class="side-nav"');
            //$("#dvSideNav").html(htmlStr);
        }
        else {
            $("#sctItems").hide();
        }
    });
}

function GetNavigationHTML() {
    var req = AjaxPost("/services/general.asmx/GetNavigationHTML");

    req.success(function (JSON) {
        $("#sctItems").html(JSON.d);
    });
}

function GetFooterNavigation() {
    var req = AjaxPost("/services/general.asmx/GetNavigation");

    req.success(function (JSON) {
        if (JSON.d) {

            var data = $.parseJSON(JSON.d);
            var htmlStr = "";

            htmlStr += '<ul class="inline-list right">';

            $.each(data, function (i, e) {

                if (e.DefaultSayfa) {
                    htmlStr += '<li><a href="' + e.Url + '">' + e.Adi + '</a></li>';
                }
                else {
                    htmlStr += '<li><a href="' + e.Url + '">' + e.Adi + '</a></li>';
                }
            });

            htmlStr += '</ul>';

            $("#dvFooterNav").html(htmlStr);
        }
        else {
            $("#dvFooterNav").hide();
        }
    });
}

function GetBackgroundPicture() {

    var req = AjaxPost("/services/general.asmx/GetBackgroundPicture");

    req.success(function (data) {
        if (data.d) {
            $("#dvBackgroundImage").append("<script type='text/javascript'>$.backstretch('" + data.d + "');</script>");
        }
        else {
            $("#dvBackgroundImage").hide();
        }
    });
}

function GetPhotoGalleries(path) {

    var req = AjaxPost("/services/general.asmx/GetPhotoGaleries");

    req.success(function (JSON) {
        if (JSON.d) {

            var data = $.parseJSON(JSON.d);
            var htmlStr = "";
            var actCls = "";

            htmlStr += '<dl class="sub-nav">';
            htmlStr += '<dt>Gallery : </dt>';

            $.each(data, function (i, e) {

                var url = path + "/" + e.Id + "/" + e.Adi.ToUrl();

                if (location.href.indexOf(url) != -1) {
                    actCls = " class='active' ";
                    url = "#!";
                }

                htmlStr += '<dd' + actCls + '><a href="' + url + '">' + e.Adi + '</a></dd>';
                actCls = "";
            });

            htmlStr += '</dl>';

            $("#dvPhotoGalleries").html(htmlStr);

            if (location.href.indexOf('?id=') != -1) {
                GetPhotoOfGalleries();
            }
        }
        else {
            $("#dvPhotoGalleries").hide();
        }
    });
}

function GetSubPages(path, id) {

    var dataS = '{"id":"' + id + '"}';

    var req = AjaxPost("/services/general.asmx/GetSubPages", dataS);

    req.success(function (JSON) {
        if (JSON.d != 'null') {

            var data = $.parseJSON(JSON.d);
            var htmlStr = "";
            var actCls = "";

            htmlStr += '<dl class="sub-nav">';
            htmlStr += '<dt>&rarr;</dt>';

            $.each(data, function (i, e) {

                var url = "/" + e.Id + "/" + e.Adi.ToUrl();

                if (location.href.indexOf(url) != -1) {
                    actCls = " class='active' ";
                    url = "#!";
                }

                htmlStr += '<dd' + actCls + '><a href="' + url + '">' + e.Adi + '</a></dd>';
                actCls = "";
            });

            htmlStr += '</dl>';

            $("#dvSubPages").html(htmlStr);
        }
        else {

            $("#dvSubPages").parent().hide();

        }
    });
}

function GetPhotoOfGalleries(galleryId) {

    var data = '{"galleryId":"' + galleryId + '"}';

    var req = AjaxPost("/services/general.asmx/GetPhotosOfGalleries", data);

    req.success(function (JSON) {
        if (JSON.d) {

            var data = $.parseJSON(JSON.d);
            var htmlStr = "";
            var actCls = "";

            //htmlStr += '<div class="large-12 columns">';
            //htmlStr += '<ul class="small-block-grid-5">';
            $.each(data, function (i, e) {

                //htmlStr += '<li>';
                htmlStr += '<div class="small-6 medium-3 large-2 columns">';
                htmlStr += '<div class="imgThumb" style="background-image:url(' + e.Orta.replace("~", "") + ')"><a class="th radius fancybox" data-fancybox-group="gallery" href="' + e.Buyuk.replace("~", "") + '" title="' + e.Aciklama + '">&nbsp;';
                htmlStr += '</a></div>';
                //htmlStr += '</li>';
                htmlStr += '</div>';
            });
            //htmlStr += '</ul>';
            //htmlStr += '</div>';

            $("#dvGalleryPhotos").html(htmlStr);

            $("#dvGalleryPhotos .large-2:last-child").css("float", "left");
        }
        else {
            $("#dvGalleryPhotos").hide();
        }
    });
}

function GetProjects(path) {

    var req = AjaxPost("/services/general.asmx/GetProjects");

    req.success(function (JSON) {

        if (JSON.d) {

            var data = $.parseJSON(JSON.d);
            var htmlStr = "";
            var actCls = "";

            htmlStr += '<dl class="sub-nav">';
            htmlStr += '<dt>Project : </dt>';

            $.each(data, function (i, e) {

                var url = path + "/" + e.Id + "/" + e.Adi.ToUrl();

                if (location.href.indexOf(url) != -1) {
                    actCls = " class='active' ";
                    url = "#!";
                }

                htmlStr += '<dd' + actCls + '><a href="' + url + '">' + e.Adi + '</a></dd>';
                actCls = "";
            });

            htmlStr += '</dl>';

            $("#dvProjects").html(htmlStr);

            if (location.href.indexOf('?id=') != -1) {
                GetPhotoOfProjects();
            }

        }

        else {
            $("#dvProjects").hide();
        }


    });

}

function GetPhotoOfProjects(projectId) {

    var data = '{"projectId":"' + projectId + '"}';

    var req = AjaxPost("/services/general.asmx/GetPhotosOfProjects", data);

    req.success(function (JSON) {

        if (JSON.d) {

            var data = $.parseJSON(JSON.d);
            var htmlStr = "";
            var actCls = "";

            $.each(data, function (i, e) {


                htmlStr += '<div class="small-6 medium-3 large-2 columns">';
                htmlStr += '<div class="imgThumb" style="background-image:url(' + e.Orta.replace("~", "") + ')"><a class="th radius fancybox" data-fancybox-group="gallery" href="' + e.Buyuk.replace("~", "") + '">&nbsp;';
                htmlStr += '</a></div>';
                htmlStr += '</div>';

            });

            $("#dvProjectPhotos").html(htmlStr);

            $("#dvProjectPhotos .large-2:last-child").css("float", "left");

        }

        else {

            $("#dvProjectPhotos").hide();
        }

    });

}

function GetSections(admin, sayfaId) {

    if (!sayfaId) {
        sayfaId = 0;
    }

    var data = '{"sayfaId":' + sayfaId + '}';

    var req = AjaxPost("/services/general.asmx/GetSections", data);

    req.success(function (JSON) {
        if (JSON.d) {

            var data = $.parseJSON(JSON.d);

            $.each(data, function (i, e) {

                var htmlStr = "";

                var height = "";
                var cerceveStr = e.CerceveKullanimi == false ? " reset-panel " : "";

                if (e.Height) {
                    height = "style='height:" + e.Height + "px'";
                }

                htmlStr += '<div class="large-12 columns" blokId="' + e.Id + '" blokName="' + e.Adi + '" pozId="' + e.PozisyonId + '">';
                htmlStr += '<div id="dvBlok_' + e.Id + '" class="panel' + cerceveStr + '" ' + height + ' poz="' + e.PozisyonId + '">';
                if (e.BaslikKullanimi) {
                    htmlStr += '<h4>' + e.Adi + '<br/><small>' + e.Aciklama + '</small></h4>';
                    htmlStr += '<hr/>';
                }
                if (e.Icerik) {
                    htmlStr += '<p>' + e.Icerik + '</p>';
                }
                htmlStr += '<div class="clear"></div>';
                htmlStr += '</div>';
                htmlStr += '</div>';

                //if (enmPozisyonlar.Sol == e.PozisyonId) {
                //    if ($("#dvSol").length > 0) {
                //        $("#dvSol").append(htmlStr);
                //    }
                //}

                if (enmPozisyonlar.OrtaUst == e.PozisyonId) {
                    if ($("#dvOrtaUst").length > 0) {
                        $("#dvOrtaUst").append(htmlStr);
                    }
                }

                if (enmPozisyonlar.OrtaUstSol == e.PozisyonId) {
                    if ($("#dvOrtaUstSol").length > 0) {
                        $("#dvOrtaUstSol").append(htmlStr);
                    }
                }

                if (enmPozisyonlar.OrtaUstSag == e.PozisyonId) {
                    if ($("#dvOrtaUstSag").length > 0) {
                        $("#dvOrtaUstSag").append(htmlStr);
                    }
                }

                if (enmPozisyonlar.OrtaSol == e.PozisyonId) {
                    if ($("#dvOrtaSol").length > 0) {
                        $("#dvOrtaSol").append(htmlStr);
                    }
                }

                if (enmPozisyonlar.Orta == e.PozisyonId) {
                    if ($("#dvOrta").length > 0) {
                        $("#dvOrta").append(htmlStr);
                    }
                }

                if (enmPozisyonlar.OrtaSag == e.PozisyonId) {
                    if ($("#dvOrtaSag").length > 0) {
                        $("#dvOrtaSag").append(htmlStr);
                    }
                }

                if (enmPozisyonlar.Sag == e.PozisyonId) {
                    if ($("#dvSag").length > 0) {
                        $("#dvSag").append(htmlStr);
                    }
                }

                if (enmPozisyonlar.OrtaAltSol == e.PozisyonId) {
                    if ($("#dvOrtaAltSol").length > 0) {
                        $("#dvOrtaAltSol").append(htmlStr);
                    }
                }

                if (enmPozisyonlar.OrtaAltSag == e.PozisyonId) {
                    if ($("#dvOrtaAltSag").length > 0) {
                        $("#dvOrtaAltSag").append(htmlStr);
                    }
                }

                if (enmPozisyonlar.OrtaAlt == e.PozisyonId) {
                    if ($("#dvOrtaAlt").length > 0) {
                        $("#dvOrtaAlt").append(htmlStr);
                    }
                }



            });


            //htmlStr += '</div>';

            //$("#dvSections").html(htmlStr);

            //$('#dvSections').each(function () {

            //    var highestBox = 0;
            //    $('.panel', this).each(function () {

            //        if ($(this).height() > highestBox)
            //            highestBox = $(this).height();
            //    });

            //    $('.panel', this).height(highestBox);

            //});

            if ($("#dvSections div").length % 3 != 0) {
                //$("#dvSections div:last-child").parent().css("float", "left");
            }

            if (admin == 'True') {

                //$("#dvOrtaSol,#dvOrta,#dvOrtaSag").droppable({
                //    activeClass: "ui-state-hover",
                //    hoverClass: "ui-state-active",
                //    drop: function (event, ui) {
                //        $(this).append(ui.draggable);
                //    }
                //});

                $("#dvSol,#dvSag,#dvOrtaSol, #dvOrta,#dvOrtaSag, #dvOrtaAlt,#dvOrtaUst,#dvOrtaUstSol,#dvOrtaUstSag,#dvOrtaAltSol,#dvOrtaAltSag").sortable({
                    connectWith: ".connectedSortable",
                    placeholder: "ui-state-highlight"
                }).disableSelection();

                $("#dvSections .panel").resizable({
                    handles: 's, w',
                    start: function (event, ui) {
                        if ($("#spnHeight").length == 0) {
                            $(ui.element).append("<span class='spnHeight'></span>");
                        }
                    },
                    resize: function (event, ui) {
                        $(ui.element).find(".spnHeight").html(ui.size.height);
                    },
                    stop: function (event, ui) {
                        $(ui.element).attr("height", ui.size.height);
                    }
                });

                //$("#dvSections .panel").draggable();

                $("#dvSag .panel").resizable({
                    handles: 's, w',
                    start: function (event, ui) {
                        if ($("#spnHeight").length == 0) {
                            $(ui.element).append("<span class='spnHeight'></span>");
                        }
                    },
                    resize: function (event, ui) {
                        $(ui.element).find(".spnHeight").html(ui.size.height);
                    },
                    stop: function (event, ui) {
                        $(ui.element).attr("height", ui.size.height);
                    }
                });

                $("#dvSol .panel").resizable({
                    handles: 's, w',
                    start: function (event, ui) {
                        if ($("#spnHeight").length == 0) {
                            $(ui.element).append("<span class='spnHeight'></span>");
                        }
                    },
                    resize: function (event, ui) {
                        $(ui.element).find(".spnHeight").html(ui.size.height);
                    },
                    stop: function (event, ui) {
                        $(ui.element).attr("height", ui.size.height);
                    }
                });

                $('#dvSections').each(function () {

                    $('.panel', this).each(function () {

                        $(this).css("border-bottom", "2px solid #a71a1d");
                    });
                });

                $("#dvSol .panel").css("border-bottom", "2px solid #a71a1d");
                $("#dvSag .panel").css("border-bottom", "2px solid #a71a1d");

            } else {
                if ($('#dvUst').children().length == 0) {
                    $('#dvUst').hide();
                }
                if ($('#dvUstSol').children().length == 0) {
                    $('#dvUstSol').hide();
                }
                if ($('#dvUstSag').children().length == 0) {
                    $('#dvUstSag').hide();
                }
                if ($('#dvUstAlt').children().length == 0) {
                    $('#dvUstAlt').hide();
                }
                if ($('#dvUstOrta').children().length == 0) {
                    $('#dvUstOrta').hide();
                }
                if ($('#dvOrtaUst').children().length == 0) {
                    $('#dvOrtaUst').hide();
                }
                if ($('#dvOrtaUstSol').children().length == 0) {
                    $('#dvOrtaUstSol').hide();
                }
                if ($('#dvOrtaUstSag').children().length == 0) {
                    $('#dvOrtaUstSag').hide();
                }
                if ($('#dvOrtaAltSol').children().length == 0) {
                    $('#dvOrtaAltSol').hide();
                }
                if ($('#dvOrtaAltSag').children().length == 0) {
                    $('#dvOrtaAltSag').hide();
                }
                if ($('#dvOrtaAlt').children().length == 0) {
                    $('#dvOrtaAlt').hide();
                }
            }

            //CKEDITOR.on('instanceCreated', function (event) {
            //    var editor = event.editor,
            //        element = editor.element;

            //    // Customize editors for headers and tag list.
            //    // These editors don't need features like smileys, templates, iframes etc.
            //    if (element.is('h1', 'h2', 'h3') || element.getAttribute('id') == 'taglist') {
            //        // Customize the editor configurations on "configLoaded" event,
            //        // which is fired after the configuration file loading and
            //        // execution. This makes it possible to change the
            //        // configurations before the editor initialization takes place.
            //        editor.on('configLoaded', function () {

            //            // Remove unnecessary plugins to make the editor simpler.
            //            editor.config.removePlugins = 'colorbutton,find,flash,font,' +
            //                'forms,iframe,image,newpage,removeformat,' +
            //                'smiley,specialchar,stylescombo,templates';

            //            // Rearrange the layout of the toolbar.
            //            editor.config.toolbarGroups = [
            //                { name: 'editing', groups: ['basicstyles', 'links'] },
            //                { name: 'undo' },
            //                { name: 'clipboard', groups: ['selection', 'clipboard'] },
            //                { name: 'about' }
            //            ];
            //        });
            //    }
            //});

            $('#container').each(function () {

                //$('.fancybox', this).each(function () {
                //    $(this).append("<span class='zoom large-3'><img src='/css/img/zoom.png' /></span>");
                //});

                $('.fancybox-editor', this).each(function () {
                    $(this).wrap("<div class='fancybox-editor-container' title='Click to zoom'> </div>");
                    $(this).parent().append("<span class='zoom large-3'><img src='/css/img/zoom.png' /></span>");
                    //$(this).attr("title", "Click to zoom");
                });
            });


        }
        else {
            $("#dvSections").hide();
        }

        $(document).foundation('orbit', {
            animation: 'fade',
            timer_speed: 100,
            animation_speed: 500
        });
    });
}

function BloklariOrtala() {
    var ieVers = getIEVersion();

    if (ieVers >= 9.0 || ieVers == -1) {
        $(document).foundation();
    }
    else { //ie8 and olders

        if (ieVers < 8) {
            //blokların arasındaki mesafe hesaplanır ve ortadaki blok tam ortalanır
            var fark = $('#dvOrtaSag').offset().left - $('#dvOrta').offset().left;

            var sec2W = $("#dvOrta").width();

            var mL = ((fark / 2) - (sec2W / 2));

            $("#dvOrta").css("margin-left", mL + " px");
        }

    }
}

function SablonDegistir(id) {
    location.href = "Default.aspx?mf=" + id;
}