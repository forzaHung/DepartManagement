(function (window, $) {
    window.profile = {
        ui: {
            file: "#file",
            imageUpload:"#imageUpload"
        },
        init: function () {
            $(profile.ui.file).on('change', function () {
                profile.readUrl(this);
            });
        },
        readUrl: function (input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $(profile.ui.imageUpload).attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    }
})(window, jQuery);
$(document).ready(function () { 
    profile.init();
})