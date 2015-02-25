
CKEDITOR.plugins.add('mygrid4', {
    requires: 'widget',
    icons: 'mygrid4',

    init: function (editor) {
        editor.widgets.add('mygrid4', {

            button: '4 Kolon ekle',

            template:
                '<div class="simplebox">' +
                    '<h2 class="simplebox-title">Title</h2>' +
                    '<div class="simplebox-content"><p>Content...</p></div>' +
                '</div>',

            editables: {
                title: {
                    selector: '.simplebox-title',
                    allowedContent: 'br strong em'
                },
                content: {
                    selector: '.simplebox-content',
                    allowedContent: 'p br ul ol li strong em'
                }
            },

            allowedContent:
                'div(!simplebox); div(!simplebox-content); h2(!simplebox-title)',

            requiredContent: 'div(simplebox)',

            upcast: function (element) {
                return element.name == 'div' && element.hasClass('simplebox');
            }
        });
    }
});