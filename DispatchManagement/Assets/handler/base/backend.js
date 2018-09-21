(function (window, $) {

    window.backend = {
        cb: function (start, end, label) {
            console.log(start.toISOString(), end.toISOString(), label);
            $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
            //alert("Callback has fired: [" + start.format('MMMM D, YYYY') + " to " + end.format('MMMM D, YYYY') + ", label = " + label + "]");
        },
        optionSet1: {
            startDate: moment().subtract(29, 'days'),
            endDate: moment(),
            minDate: '01/01/2012',
            maxDate: '12/31/2015',
            dateLimit: {
                days: 60
            },
            showDropdowns: true,
            showWeekNumbers: true,
            timePicker: false,
            timePickerIncrement: 1,
            timePicker12Hour: true,
            ranges: {
                'Today': [moment(), moment()],
                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
            },
            opens: 'left',
            buttonClasses: ['btn btn-default'],
            applyClass: 'btn-small btn-primary',
            cancelClass: 'btn-small',
            format: 'MM/DD/YYYY',
            separator: ' to ',
            locale: {
                applyLabel: 'Submit',
                cancelLabel: 'Clear',
                fromLabel: 'From',
                toLabel: 'To',
                customRangeLabel: 'Custom',
                daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                firstDay: 1
            }
        },
        init: function () {
            $('#reportrange span').html(moment().subtract(29, 'days').format('MMMM D, YYYY') + ' - ' + moment().format('MMMM D, YYYY'));
            $('#reportrange').daterangepicker(backend.optionSet1, backend.cb);
            $('#reportrange').on('show.daterangepicker', function () {
                console.log("show event fired");
            });
            $('#reportrange').on('hide.daterangepicker', function () {
                console.log("hide event fired");
            });
            $('#reportrange').on('apply.daterangepicker', function (ev, picker) {
                console.log("apply event fired, start/end dates are " + picker.startDate.format('MMMM D, YYYY') + " to " + picker.endDate.format('MMMM D, YYYY'));
            });
            $('#reportrange').on('cancel.daterangepicker', function (ev, picker) {
                console.log("cancel event fired");
            });
            $('#options1').click(function () {
                $('#reportrange').data('daterangepicker').setOptions(backend.optionSet1, backend.cb);
            });
            $('#options2').click(function () {
                $('#reportrange').data('daterangepicker').setOptions(optionSet2, backend.cb);
            });
            $('#destroy').click(function () {
                $('#reportrange').data('daterangepicker').remove();
            });

            backend.select2Default();
            backend.select2FixPosition();
            backend.regisFlatCheckbox();
        },
        encodeText: function (text) {
            return escape(text.trim());
        },
        onSuccessAjax: function () {
            $('input.flat').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });
        },
        myStack: { "dir1": "down", "dir2": "right", "push": "top" },
        alert: function (options) {
            var opts = {
                type: "info",
                title: "Alert",
                text: "Check me out. I'm in a different stack.",
                addclass: "stack-custom2",
                stack: backend.myStack,
                delay: 2500,
            };
            switch (options.type) {
                case 'error':
                    opts.title = "Lỗi";
                    // opts.text = "Watch out for that water tower!";
                    opts.type = "error";
                    break;
                case 'info':
                    opts.title = "Thông báo";
                    //opts.text = "Have you met Ted?";
                    opts.type = "info";
                    break;
                case 'success':
                    opts.title = "Thông báo";
                    //opts.text = "I've invented a device that bites shiny metal asses.";
                    opts.type = "success";
                    break;
            }
            /* merge options into defaultOptions, recursively */
            $.extend(true, opts, options);
            //init PNotify
            new PNotify(opts);
        },
        confirm: function (options, callback) {
            var opts = {
                title: "Confirmation",
                text: "Check me out. I'm in a different stack.",
                icon: 'glyphicon glyphicon-question-sign',
                hide: false,
                addclass: "stack-modal",
                confirm: {
                    confirm: true
                },
                buttons: {
                    closer: false,
                    sticker: false
                },
                history: {
                    history: false
                }
            };
            $.extend(true, opts, options);
            //init PNotify
            new PNotify(opts)
                .get().on('pnotify.confirm', function () {
                    callback();
                }).on('pnotify.cancel', function () {

                });
        },
        blockNotify: function (options) {
            var opts = {
                title: "PNotify",
                text: "Welcome. Try hovering over me. You can click things behind me, because I'm non-blocking.",
                addclass: "stack-custom2",
                type: "dark",
                nonblock: {
                    nonblock: false
                },
            };
            /* merge options into defaultOptions, recursively */
            $.extend(true, opts, options);
            //init PNotify
            new PNotify(opts);

        },
        wrapPaging: function (options, callback) {
            var totalRecord = 0;
            if (typeof options.totalRecord != "undefined") {
                totalRecord = options.totalRecord;

            } else {
                console.log("totalRecord is not defined");
                return false;
            }
            var defaultOptions = {
                pageId: '#paginationUL',
                pageIndex: 1,
                pageSize: 20,
                changePageSize: true
            };

            /* merge options into defaultOptions, recursively */
            $.extend(true, defaultOptions, options);
            var totalsize = Math.ceil(totalRecord / defaultOptions.pageSize);
            //Unbind pagination if it existed or click change pagesize
            if ($(defaultOptions.pageId + ' a').length === 0 || defaultOptions.changePageSize === true) {
                $(defaultOptions.pageId).empty();
                $(defaultOptions.pageId).removeData("twbs-pagination");
                $(defaultOptions.pageId).unbind("page");
            }
            //Bind Pagination Event
            $(defaultOptions.pageId).twbsPagination({
                totalPages: totalsize,
                visiblePages: 7,
                href: '#p=' + options.pageIndex + '&c=p',
                hrefVariable: options.pageIndex,
                first: '<<',
                prev: '<',
                next: '>',
                last: '>>',
                onPageClick: function (event, p) {
                    options.pageIndex = p;
                    callback(p);
                }
            });
        },
        imgError: function (image) {
            var url = window.location.href;
            image.onerror = "";
            image.src = "/Assets/Backend/images/user.png";

            return true;
        },
        //------------- loading -----------
        startLoading: function () {
            $('.dv-loading').removeClass('hide');
        },
        stopLoading: function () {
            setTimeout($('.dv-loading')
                .addClass('hide'), 100);
        },
        miniLoading: function () {
            var loading = '<img src="/Assets/img/mini-loading.gif">';
            return loading;
        },
        mediumLoading: function () {
            var loading = '<img src="/Assets/img/loading.gif">';
            return loading;
        },
        //--------------------------
        smalldateFormatJsonDMY: function (datetime) {
            if (datetime == '' || datetime == undefined || datetime == null) {
                return '';
            } else {
                var newdate = new Date(parseInt(datetime.substr(6)));
                var month = newdate.getMonth() + 1;
                var day = newdate.getDate();
                var year = newdate.getFullYear();
                if (month < 10)
                    month = "0" + month;
                if (day < 10)
                    day = "0" + day;
                return day + "/" + month + "/" + year;
            }
        },
        smalldateFormatJsonYMD: function (datetime) {
            if (datetime == '' || datetime == undefined || datetime == null) {
                return '';
            } else {
                var newdate = new Date(parseInt(datetime.substr(6)));
                var month = newdate.getMonth() + 1;
                var day = newdate.getDate();
                var year = newdate.getFullYear();
                if (month < 10)
                    month = "0" + month;
                if (day < 10)
                    day = "0" + day;
                return year + "-" + month + "-" + day;
            }
        },
        stringToDate: function (_date, _format, _delimiter) {
            var formatLowerCase = _format.toLowerCase();
            var formatItems = formatLowerCase.split(_delimiter);
            var dateItems = _date.split(_delimiter);
            var monthIndex = formatItems.indexOf("mm");
            var dayIndex = formatItems.indexOf("dd");
            var yearIndex = formatItems.indexOf("yyyy");
            var month = parseInt(dateItems[monthIndex]);
            month -= 1;
            var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);
            return formatedDate;
        },
        select2Default: function () {
            var elements = $('[select2Search]');
            for (var i = 0; i < elements.length; i++) {
                var elem = $(elements[i]);
                if (!elem.data('select2')) {
                    elem.select2($.extend(true, {

                    }, elem.data()));
                }
            }
            this.select2FixPosition();
        },
        select2FixPosition: function () {
            //select2 fix position
            $(".select2-hidden-accessible").map(function (index, elm) {
                $(elm).data("select2").on("results:message", function () {
                    this.dropdown._positionDropdown();
                });
            });

            $("select").on("select2:close", function (e) {
                var _this = $(this);
                var form = _this.closest("form");
                if (form.length > 0 && form.data("validator"))
                    _this.valid();
            });
        },
        regisFlatCheckbox: function () {
            $('input.flat').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });
        },
        getDistrictByCity: function (id, $boxOption) {
            var html = "";
            $.ajax({
                url: '/LocationPickup/GetLocationById',
                type: "POST",
                data: { id: id },
                dataType: "json",
                success: function (response) {
                    $.each(response.data, function (i, val) {
                        html += "<option value=\"" + val.DistrictID + "\">" + val.Name + "</option>";
                    });
                    $boxOption.html(html);
                }
            });

        },
        checkDecimal: function (element) {
            var v = parseFloat(element.value);
            console.log(v);
            if (isNaN(v)) {
                element.value = '';
                console.log(element.value);
            } else {
                element.value = v.toFixed(2);
                console.log(element.value);
            }
        },
        ckDecimal: function (element) {
            var v = parseFloat(element.value);
            //console.log(v);
            //if (isNaN(v)) {
            //    element.value = '';
            //    console.log(element.value);
            //} else {
            //    element.value = v.toFixed(2);
            //    console.log(element.value);
            //}
        },
        jsDateToString: function (_date) {
            var dd = _date.getDate();
            var mm = _date.getMonth() + 1; //January is 0!

            var yyyy = _date.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            var res = dd + '/' + mm + '/' + yyyy;
            return res;
        },
        //--------Noty Alert---------
        noty: {
            //--------------Message alert--------------------
            alert: function (options) {
                var titleHeader = "";
                if (typeof options.titleHeader != undefined) {
                    titleHeader = options.titleHeader;
                }
                var defaultOptions = {
                    layout: 'center',
                    theme: 'defaultTheme',
                    type: 'alert',
                    text: '',
                    dismissQueue: true,
                    template: '<div class="noty_message"><div class="noty-header">' + titleHeader + '</div><div class="noty_text"></div><div class="noty_close"></div></div>',
                    animation: {
                        open: { height: 'toggle' },
                        close: { height: 'toggle' },
                        easing: 'swing',
                        speed: 500
                    },
                    timeout: 1000, //1s
                    force: false,
                    modal: true,
                    maxVisible: 1, // max item display
                    closeWith: ['button'], /// ['click', 'button', 'hover']
                    callback: {
                        onShow: function () {
                        },
                        afterShow: function () {
                            var that = this;
                            $.each(that.options.buttons, function (i, v) {
                                if (v.focus != undefined && v.focus == true) {
                                    $(that.$buttons).find("button")[i].focus();
                                    return false;
                                }
                            });
                        },
                        onClose: function () {
                        },
                        afterClose: function () {
                        },
                        onCloseClick: function () {
                        }
                    },
                    buttons: false
                };
                /* merge options into defaultOptions, recursively */
                $.extend(true, defaultOptions, options);
                if (defaultOptions.type == 'success') {
                    defaultOptions.callback.onClose.call();
                    //this.log(defaultOptions.text);
                    return noty(defaultOptions);
                } else {
                    if (defaultOptions.type == 'showsuccess') {
                        defaultOptions.type = 'success';
                    }

                    return noty(defaultOptions);
                }
                //return noty(defaultOptions);
            },
            msgAlert: function (options, callback) {
                var settings = $.extend({}, { type: 'alert' }, options);
                this.alert(settings);
            },
            msgSuccess: function (options) {
                var settings = $.extend({}, { type: 'success' }, options);
                this.alert(settings);
            },
            msgError: function (options) {
                var settings = {
                    type: 'error',
                    buttons: [{
                        //addClass: 'btn btn-primary',
                        addClass: 'btn btn-grey btn-crm btn-ok',
                        text: 'OK',
                        onClick: function ($noty) {
                            // this = button element
                            // $noty = $noty element
                            $noty.close();
                        },
                        focus: false
                    }],
                    modal: true,
                    titleHeader: 'Error'
                };
                $.extend(true, settings, options);
                this.alert(settings);
            },
            msgWarning: function (options) {
                var settings = {
                    type: 'warning',
                    buttons: [{
                        //addClass: 'btn btn-primary',
                        addClass: 'btn btn-grey btn-crm btn-ok',
                        text: 'OK',
                        onClick: function ($noty) {
                            // this = button element
                            // $noty = $noty element
                            $noty.close();
                        },
                        focus: false
                    }],
                    modal: true,
                    titleHeader: 'Warning'
                };
                $.extend(true, settings, options);
                this.alert(settings);
            },
            msgInfo: function (options) {
                var settings = $.extend({}, { type: 'information' }, options);
                this.alert(settings);
            },
            msgShowError: function (options) {
                var settings = $.extend({}, { type: 'error' }, options);
                this.alert(settings);
            },
            msgShowWarning: function (options) {
                var settings = $.extend({}, { type: 'warning' }, options);
                this.alert(settings);
            },
            msgShowSuccess: function (options) {
                var settings = {
                    type: 'showsuccess',
                    buttons: [{
                        addClass: 'btn btn-grey btn-crm btn-ok',
                        text: 'OK',
                        onClick: function ($noty) {
                            $noty.close();
                        },
                        focus: false
                    }],
                    modal: true,
                    titleHeader: 'Success'
                };
                $.extend(true, settings, options);
                this.alert(settings);
            },
            msgConfirm: function (options, callback, dismiss) {
                dismiss = dismiss || function () { };
                var settings = {
                    type: 'confirm',
                    buttons: [{
                        //addClass: 'btn btn-primary',
                        addClass: 'btn btn-grey btn-crm btn-ok',
                        text: 'Yes',
                        onClick: function ($noty) {
                            // this = button element
                            // $noty = $noty element
                            if (callback != undefined)
                                callback();
                            $noty.close();
                        },
                        focus: false

                    }, {
                        //addClass: 'btn btn-danger',
                        addClass: 'btn btn-grey btn-crm btn-cancel',
                        text: 'No',
                        onClick: function ($noty) {
                            if (dismiss != null) {
                                dismiss();
                            }
                            $noty.close();
                        },
                        focus: true
                    }],
                    modal: true,
                    titleHeader: 'Confirmation'
                };
                $.extend(true, settings, options);
                this.alert(settings);
            },
            msgWarningWithAbort: function (options) {
                var settings = {
                    type: 'confirm',
                    buttons: [{
                        addClass: 'btn btn-grey btn-crm btn-cancel',
                        text: 'Abort',
                        onClick: function ($noty) {
                            $noty.close();
                        }
                    }],
                    modal: true
                };
                $.extend(true, settings, options);
                this.alert(settings);
            },
            readAlert: function (key) {
                var message = '';
                $.ajax({
                    type: "GET",
                    url: "/Alert/GetKey",
                    data: { key: key },
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        if (response.status == true) {
                            message = response.Message;
                        }
                    }
                });
                return message;
            },
        },
    };
    jQuery.extend({
        compare: function (arrayA, arrayB) {
            //  if (arrayA.length != arrayB.length) { return false; }
            // sort modifies original array
            // (which are passed by reference to our method!)
            // so clone the arrays before sorting
            var a = jQuery.extend(true, [], arrayA);
            var b = jQuery.extend(true, [], arrayB);
            var c = new Array();
            a.sort();
            b.sort();
            for (var i = 0, l = a.length; i < l; i++) {
                if (a[i] !== b[i]) {
                    c.push(b[i]);
                }
            }
            return c;
        },
        diffArr: function (arrayOld, arrayNew) {
            //return array2.filter(function (i) { return array1.indexOf(i.value) < 0; });
            var a = jQuery.extend(true, [], arrayOld);
            var b = jQuery.extend(true, [], arrayNew);
            var difference = [];
            console.log(a);
            console.log(b);
            b.filter(function (i) {
                //console.log("i ===>" + i.value);
                //console.log("===>" + a.valueOf(i));
                if (a.indexOf(i) < 0) {
                    difference.push(i);
                    return true;
                } else {
                    return false;
                }
            });
            //jQuery.grep(a, function (el) {
            //    //for (var i = 0; i < a.length; i++) {
            //    //    if (el.value.indexOf(a[i].value) == -1) {
            //    //        difference.push(el);
            //    //    }
            //    //}
            //     if (jQuery.inArray(el, b) == -1) difference.push(el);
            //});
            console.log(difference);
            return difference;
        }
    });

})(window, jQuery);
$(document).ready(function () {
    backend.init();
});
var SaveFileAs = function (uri, filename) {
    console.log(uri);
    var link = document.createElement('a');
    if (typeof link.download === 'string') {
        link.href = uri;
        link.download = filename;

        //Firefox requires the link to be in the body
        document.body.appendChild(link);

        //simulate click
        link.click();

        //remove the link when done
        document.body.removeChild(link);
    } else {
        window.open(uri);
    }
}