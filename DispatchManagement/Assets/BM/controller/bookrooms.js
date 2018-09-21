(function (windown, $) {
    window.bookrooms = {
        init: function () {
            bookrooms.search();
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
            //bookrooms.search();
            //bookrooms.searchTime();
            $("#DepartmentId").change(function () {
                var Id = $(this).val();
                $.ajax({
                    type: "post",
                    url: "/BookRooms/GetlistEmployee",
                    data: { Id: Id },
                    success: function (rp) {
                        if (rp.stt) {
                            $('#EmployeeId').children('option:not(:first)').remove();
                            console.log(rp.list.length)
                            for (var i = 0; i < rp.list.length; i++) {
                                var name = rp.list[i].FirstName + ' ' + rp.list[i].LastName;
                                $('#EmployeeId').append($('<option>', { value: rp.list[i].Id, text: name}));
                            }
                        }        
                    }
                });
            });
            $("#btnSearch").on('click', function () {
                bookrooms.search();
            });
            $("#btnSearchTime").on('click', function () {
                bookrooms.searchTime();
            });
            $("#tbl-Bookrooms").on('click', ".deleteBookroomsDate", function () {
                $.ajax({
                    type: "POST",
                    url: "/BookRooms/Delete",
                    data: { Id: $(this).data("id") },
                    success: function (rp) {
                        if (rp.stt) {
                            bookrooms.search();
                        } else {
                            toastr["error"]("Có lỗi trong quá trình xóa.");
                        }
                    }
                })
            });
        },
        search: function () {
            $("#tbl-Bookrooms").bootstrapTable('refresh');
            var searchUrlInventory = "/BookRooms/GetBookroomsList";
            $("#tbl-Bookrooms").bootstrapTable({
                method: 'get',
                url: searchUrlInventory,
                queryParams: function (p) {
                    console.log(p);
                    var param = $.extend(true, {
                        limit: p.limit,
                        offset: p.offset,
                        keyword: $("#searchString").val(),
                        searchDate: $("#searchDate").val()
                    }, p);
                    return param;
                },
                striped: true,
                sidePagination: 'server',
                pagination: true,
                paginationVAlign: 'both',
                limit: 10,
                pageSize: 10,
                pageList: [10, 20, 40, 80],
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
                        field: 'NameEmployee',
                        title: 'Nhân Viên',
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'NameDepartment',
                        title: 'Phòng Ban',
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'NameRoom',
                        title: 'Phòng Họp',
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'CreateDate',
                        title: 'Ngày Họp',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            if (value != null && value.toString().indexOf('-') == -1) {
                                return bookrooms.smalldateFormatJsonDMY(value);
                            }
                            return "";
                        }
                    },
                    {
                        field: 'TimeStart',
                        title: 'Thời Gian Bắt Đầu',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            if (value != null && value.toString().indexOf('-') == -1) {
                                return bookrooms.smalltimeFormatJsonDMY(value);
                            }
                            return "";
                        }
                    },
                    {
                        field: 'TimeEnd',
                        title: 'Thời Gian Kết Thúc',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            if (value != null && value.toString().indexOf('-') == -1) {
                                return bookrooms.smalltimeFormatJsonDMY(value);
                            }
                            return "";
                        }
                    },
                    {
                        title: 'Chi tiết',
                        align: 'center',
                        valign: 'middle',
                        searchable: false,
                        formatter: function (value, row, index) {
                            return [
                                '<a href="/BM/BookRooms/Edit/' + row.Id + '">Sửa</a>',
                                '<button class="btn-xs btn-danger delete deleteBookroomsDate" data-id="' + row.Id + '" data-toggle="modal" data-target="#myModal">',
                                'Xóa',
                                '</button>',
                            ].join(' ');
                        }
                    }
                ],
                onLoadSuccess: function (data) {
                    if (data.status) {

                    } else {
                        $("#tbl-Bookrooms").bootstrapTable('removeAll');
                        toastr["error"](data.mess);
                    }

                },
            });
        },

        searchTime: function () {
            $("#tbl-Bookrooms").bootstrapTable('refresh');
            var searchUrlInventory = "/BookRooms/GetBookRooms";
            $("#tbl-Bookrooms").bootstrapTable({
                method: 'get',
                url: searchUrlInventory,
                queryParams: function (p) {
                    console.log(p);
                    var param = $.extend(true, {
                        searchDate: $("#searchDate").val()
                    }, p);
                    return param;
                },
                striped: true,
                sidePagination: 'server',
                pagination: true,
                paginationVAlign: 'both',
                limit: 10,
                pageSize: 10,
                pageList: [10, 20, 40, 80],
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
                        field: 'NameEmployee',
                        title: 'Nhân Viên',
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'NameDepartment',
                        title: 'Phòng Ban',
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'NameRoom',
                        title: 'Phòng Họp',
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'CreateDate',
                        title: 'Ngày Họp',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            if (value != null && value.toString().indexOf('-') == -1) {
                                return bookrooms.smalldateFormatJsonDMY(value);
                            }
                            return "";
                        }
                    },
                    {
                        field: 'TimeStart',
                        title: 'Thời Gian Bắt Đầu',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            if (value != null && value.toString().indexOf('-') == -1) {
                                return bookrooms.smalltimeFormatJsonDMY(value);
                            }
                            return "";
                        }
                    },
                    {
                        field: 'TimeEnd',
                        title: 'Thời Gian Kết Thúc',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            if (value != null && value.toString().indexOf('-') == -1) {
                                return bookrooms.smalltimeFormatJsonDMY(value);
                            }
                            return "";
                        }
                    },
                    {
                        title: 'Chi tiết',
                        align: 'center',
                        valign: 'middle',
                        searchable: false,
                        formatter: function (value, row, index) {
                            return [
                                '<a href="/BM/BookRooms/Edit/' + row.Id + '">Sửa</a>',
                                '<button class="btn-xs btn-danger delete deleteBookroomsDate" data-id="' + row.Id + '" data-toggle="modal" data-target="#myModal">',
                                'Xóa',
                                '</button>',
                            ].join(' ');
                        }
                    }
                ],
                onLoadSuccess: function (data) {
                    if (data.status) {

                    } else {
                        $("#tbl-Bookrooms").bootstrapTable('removeAll');
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
        smalltimeFormatJsonDMY: function (datetime) {
            if (datetime == '' || datetime == undefined || datetime == null) {
                return '';
            } else {
                var newdate = new Date(parseInt(datetime.substr(6)));
                var hours = newdate.getHours();
                var minutes = newdate.getMinutes();
                return hours + ":" + minutes;
            }
        },
    };
})(window, jQuery);
$(document).ready(function () {
    bookrooms.init();
})