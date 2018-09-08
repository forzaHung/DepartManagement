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
        },
        encodeText: function (text) {
            return escape(text.trim());
        },
        onSuccessAjax:function() {
            $('input.flat').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });
        },
        myStack : {"dir1":"down", "dir2":"right", "push":"top"},
        alert: function (options) {
            var opts = {
                type:"info",
                title: "Alert",
                text: "Check me out. I'm in a different stack.",
                addclass: "stack-custom2",
                stack: backend.myStack,
                delay: 2500,
            };
            switch (options.type) {
                case 'error':
                    opts.title = "Oh No";
                    opts.text = "Watch out for that water tower!";
                    opts.type = "error";
                    break;
                case 'info':
                    opts.title = "Breaking News";
                    opts.text = "Have you met Ted?";
                    opts.type = "info";
                    break;
                case 'success':
                    opts.title = "Good News Everyone";
                    opts.text = "I've invented a device that bites shiny metal asses.";
                    opts.type = "success";
                    break;
            }
            /* merge options into defaultOptions, recursively */
            $.extend(true, opts, options);
            //init PNotify
            new PNotify(opts);
        },
        confirm: function (options,callback) {
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
                alert('Oh ok. Chicken, I see.');
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
                href: '#p=' + options.pageIndex + '&c=d',
                hrefVariable: options.pageIndex,
                first: '<<',
                prev: '<',
                next: '>',
                last: '>>',
                onPageClick: function (event, p) {
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
