$(function () {
    var
        $table = $('#tree-table'),
        rows = $table.find('tr');

    rows.each(function (index, row) {
        //debugger;
        console.log(row);
        console.log(index);
        var
            $row = $(row),
            level = $row.data('level'),
            id = $row.data('id'),
            $columnName = $row.find('td[data-column="name"]'),
            //$columnTitle = $row.find('td[data-column="title"]'),
            children = $table.find('tr[data-parent="' + id + '"]');

        if (children.length) {
            var expander = $columnName.prepend('<span class="treegrid-expander glyphicon glyphicon-chevron-right"></span> ');
            var text = $columnName.html();

            children.hide();

            expander.on('click','span',function (e) {
                var $target = $(e.target);
                if ($target.hasClass('glyphicon-chevron-right')) {
                    $target
                        .removeClass('glyphicon-chevron-right')
                        .addClass('glyphicon-chevron-down');

                    children.show();
                } else {
                    $target
                        .removeClass('glyphicon-chevron-down')
                        .addClass('glyphicon-chevron-right');

                    reverseHide($table, $row);
                }
            });
        }
        var space = "";
        for (var i = 0; i < level; i++) {
            /*space += "&nbsp;&nbsp;&nbsp;&nbsp;"*/;
            space += "-----";
        }

        $columnName.prepend('' + space + '' );
    });

    // Reverse hide all elements
    reverseHide = function (table, element) {
        var
            $element = $(element),
            id = $element.data('id'),
            children = table.find('tr[data-parent="' + id + '"]');

        if (children.length) {
            children.each(function (i, e) {
                reverseHide(table, e);
            });

            $element
                .find('.glyphicon-chevron-down')
                .removeClass('glyphicon-chevron-down')
                .addClass('glyphicon-chevron-right');

            children.hide();
        }
    };
});
