(function (window, $) {
    window.maker = {
        ui: {
            btnSearch: "#btnSearch",
            SearchString: "#SearchString"
        },
        init: function () {
            $(maker.ui.btnSearch).off('click').on('click', function () {
                maker.search();
            })
        },
        search: function () {
            var keyword = backend.encodeText($('#SearchString').val()).trim();
            if (keyword !== '')
                keyword = "?searchString=" + backend.encodeText(keyword);

            var searchUrl = "/Backend/Maker/GetMakers" + keyword;
            $.ajax({
                url: searchUrl,
                success: function (result) {
                    $('#MakerList').html(result);
                    $('input.flat').iCheck({
                        checkboxClass: 'icheckbox_flat-green',
                        radioClass: 'iradio_flat-green'
                    });
                }
            });
        },
    }
})(window, jQuery);
$(document).ready(function () {
    maker.init();
});
function onSuccessAjax() {
    $('input.flat').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });
}