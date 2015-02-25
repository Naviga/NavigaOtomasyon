/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.extraPlugins = 'allowsave,templates,format,richcombo,floatpanel,listblock,button';
    config.filebrowserImageBrowseUrl = '/admin/DosyaYonetimi.aspx?iframe';
    config.filebrowserUploadUrl = '/uploader/upload.php';
    config.filebrowserBrowseUrl = '/admin/Pages.aspx?iframe&ss=1&i=0';
    //config.filebrowserImageUploadUrl= '/uploader/upload.php?type=Images';
    //config.toolbar = [['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'], '/'];
};
