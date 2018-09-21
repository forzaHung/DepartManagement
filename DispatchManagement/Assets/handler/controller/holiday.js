(function (windown, $) {
    window.holiday = {
        init: function () {
            //holiday.search();
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            holiday.search();
            $("#btnSearch").on('click', function() {
                holiday.search();
            });
            $("#tbl-Holiday").on('click', ".deleteHolidayDate", function() {
                $.ajax({
                    type: "POST",
                    url: "/Holiday/Delete",
                    data: { Id: $(this).data("id") },
                    success: function(rp) {
                        if (rp.stt) {
                            holiday.search();
                        } else {
                            toastr["error"]("Có lỗi trong quá trình xóa ngày nghỉ.");
                        }
                    }
                })
            });
        },
        search: function () {
            $("#tbl-Holiday").bootstrapTable('refresh')
            var searchUrlInventory = "/Holiday/GetHolidayList";
            $("#tbl-Holiday").bootstrapTable({
                method: 'get',
                url: searchUrlInventory,
                queryParams: function (p) {
                    var param = $.extend(true, {
                        limit: p.limit,
                        offset: p.offset,
                        fromDate: $("#fromDate").val(),
                        toDate: $("#toDate").val()
                    }, p);
                    return param;
                },
                striped: true,
                sidePagination: 'server',
                pagination: true,
                paginationVAlign: 'both',
                limit: 31,
                pageSize: 31,
                pageList: [31, 61, 122, 244],
                search: false,
                showColumns: false,
                showRefresh: false,
                minimumCountColumns: 2,
                columns: [
                    {
                        field: 'STT',
                        checkbox: true,
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'HolidayDate',
                        title: 'Ngày nghỉ',
                        align: 'center',
                        valign: 'middle',
                        formatter: function(value) {
                            if (value != null && value.toString().indexOf('-') == -1) {
                                return holiday.smalldateFormatJsonDMY(value);
                            }
                            return "";
                        }
                    },
                    {
                        field: 'Description',
                        title: 'Nội dung',
                        align: 'center',
                        valign: 'middle',
                        editable: {
                            type: 'text',
                            title: 'phút',
                            validate: function(value) {
                                value = $.trim(value);
                                if (!value) {
                                    return 'Nội dung không được bỏ trống';
                                }
                                var data = $("#tbl-Holiday").bootstrapTable('getData'),
                                    index = $(this).parents('tr').data('index');
                                if (data[index] == undefined)
                                    return 'Lỗi';
                                $.ajax({
                                    url: '/Holiday/ChangeDescription',
                                    data: { Id: data[index].Id, description: value },
                                    type: 'Post',
                                    success: function(res) {
                                        holiday.search();
                                    }
                                })
                                return '';
                            }
                        },
                    },
                    {
                        title: 'Chi tiết',
                        align: 'center',
                        valign: 'middle',
                        searchable: false,
                        formatter: function (value, row, index) {
                            return [
                                '<button class="btn-xs btn-danger delete deleteHolidayDate" data-id="' + row.Id + '" data-toggle="modal" data-target="#myModal">',
                                'Xóa quy định',
                                '</button>',
                            ].join(' ');
                        }
                    }
                ],
                onLoadSuccess: function (data) {
                    if (data.status) {
                       
                    } else {
                        $("#tbl-Holiday").bootstrapTable('removeAll');
                        toastr["error"](data.mess);
                    }

                },
            });
        },
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
    };
})(window, jQuery);
$(document).ready(function () {
    holiday.init();
})