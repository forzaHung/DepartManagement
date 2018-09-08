(function (factory) {
    if (typeof define === "function" && define.amd) {
        define(["jquery", "../jquery.validate"], factory);
    } else {
        factory(jQuery);
    }
}(function ($) {

    /*
     * Translated default messages for the jQuery validation plugin.
     * Locale: FR (French; français)
     */
    $.extend($.validator.messages, {
        required: "Dữ liệu bắt bộc phải nhập",
        email: "Địa chỉ email không hợp lệ",
        number: "Dữ liệu phải là kiểu số",
        maxlength: $.validator.format("Dữ liệu không được vượt quá {0} kí tự"),
    });

}));