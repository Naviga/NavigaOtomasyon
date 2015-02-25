
CKEDITOR.plugins.add('mygrid3', {
    requires: 'widget',
    icons: 'mygrid3',
    init: function (editor) {
        editor.widgets.add('mygrid3', {

            button: '3 Kolon ekle',

            template:
                '<div class="row">'+
                    '<div class="large-4 column">' +
                        '<h2 class="title1">Title 1</h2>' +
                        '<p class="content1">Content...</p>' +
                    '</div>'+
                    '<div class="large-4 column">' +
                        '<h2 class="title2">Title 2</h2>' +
                        '<p class="content2">Content...</p>' +
                    '</div>'+
                    '<div class="large-4 column">' +
                        '<h2 class="title3">Title 3</h2>' +
                        '<p class="content3">Content...</p>' +
                    '</div>',

            editables: {
                title: {
                    selector: '.title1',
                    allowedContent: 'br strong em'
                },
                content: {
                    selector: '.content1',
                    allowedContent: 'p br ul ol li strong em'
                }
            },

            //allowedContent:
            //    'div(!row); div(!simplebox-content); h2(!simplebox-title)',

            requiredContent: 'div(row)',

            upcast: function (element) {
                return element.name == 'div' && element.hasClass('simplebox');
            }
        });
    }
});