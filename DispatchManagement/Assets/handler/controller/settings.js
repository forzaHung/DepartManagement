(function (window, $) {
    window.settings = {
        init: function () {
            $('#btnSearch').off('click').on('click', function () {
                var searchUrl = '/Backend/Settings/GetSettings';
                var type = 0;
                var name = $('#Name').val();
                $.ajax({
                    url: searchUrl,
                    data: { type: type, name: name },
                    success: function (result) {
                        $('#SettingsList').html(result);
                    }
                });
            })
        }
    }
})(window, jQuery);
$(document).ready(function () {
    settings.init();
})