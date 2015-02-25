/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

/**
 * @fileOverview The Save plugin.
 */

(function () {
    var saveCmd = {
        readOnly: 1,

        exec: function (editor) {
            if (editor.fire('save')) {

                var op = $(editor.element).attr("op");

                if (op == 'page-content') {
                    SavePageContent(editor.getData());
                }
                else if (op == 'page-title') {
                    SavePageTitle(editor.getData());
                }
                else if (op == 'page-pictures-title') {
                    SavePagePicturesTitle(editor.getData());
                }
                else if (op == 'string-resources') {
                    var code = $(editor.element).attr("code");

                    SaveStringResource(code, editor.getData());
                }

            }
        }
    };

    // Register a plugin named "allowsave".
    CKEDITOR.plugins.add('allowsave', {
        lang: 'af,ar,bg,bn,bs,ca,cs,cy,da,de,el,en,en-au,en-ca,en-gb,eo,es,et,eu,fa,fi,fo,fr,fr-ca,gl,gu,he,hi,hr,hu,id,is,it,ja,ka,km,ko,ku,lt,lv,mk,mn,ms,nb,nl,no,pl,pt,pt-br,ro,ru,si,sk,sl,sq,sr,sr-latn,sv,th,tr,ug,uk,vi,zh,zh-cn', // %REMOVE_LINE_CORE%
        icons: 'save', // %REMOVE_LINE_CORE%
        hidpi: true, // %REMOVE_LINE_CORE%
        init: function (editor) {
            var command = editor.addCommand('save', saveCmd);
            command.modes = { wysiwyg: 1, source: 1 };

            editor.ui.addButton && editor.ui.addButton('Save', {
                label: editor.lang.allowsave.toolbar,
                command: 'save',
                toolbar: 'document,10'
            });
        }
    });
})();

/**
 * Fired when the user clicks the Save button on the editor toolbar.
 * This event allows to overwrite the default Save button behavior.
 *
 * @since 4.2
 * @event save
 * @member CKEDITOR.editor
 * @param {CKEDITOR.editor} editor This editor instance.
 */
