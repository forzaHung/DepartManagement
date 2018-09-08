//Add regex check for JQuery Validation Plugin
$.validator.addMethod(
        "regex",
        function (value, element, regexp) {
            var re = new RegExp(regexp);
            return this.optional(element) || re.test(value);
        },
        "Caractère non valide"
);
$.validator.addMethod("myPhoneNumberValidator", function (value, element) {
    // no phone number is a good phone number (the field is optional)
    if (value.length == 0) { return true; }
    // if the phone number field is not empty, it has to follow the fixed format
    return /^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$/.test(value);
}, 'Định dạng số điện thoại không hợp lệ');
//compare date
$.validator.addMethod("greaterThan",
function (value, element, params) {
    if (!/Invalid|NaN/.test(new Date(formatDate(value)))) {
        return new Date(formatDate(value)) >= new Date(formatDate($(params).val()));
    } else {
        return false;
    }
}, 'Ngày đi không được lớn hơn ngày về!');
$.validator.addMethod("money", function (value, element) {
    return this.optional(element) || /^\$?[0-9][0-9\,]*(\.\d{1,2})?$|^\$?[\.]([\d][\d]?)$/.test(value);
}, "Định dạng tiền không hợp lệ");
$.validator.addMethod("starEmail", function (value, element) {
    var emailRegExp = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    value = value.toLowerCase();
    return this.optional(element) || emailRegExp.test(value);
}, $.validator.messages.email);

function formatDate(strDate) {
    var newDate = strDate.split('/');
    var d = newDate[0];
    var m = newDate[1];
    var y = newDate[2];
    return y + "-" + m + "-" + d;
}
