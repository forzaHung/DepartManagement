(function (window, $) {
    window.changepassword = {
        init: function () {
            setTimeout(function () {
                $('#OldPassword').val('');
            }, 'fast');
        }
    }
})(window, jQuery);
$(document).ready(function () {
    changepassword.init();
})