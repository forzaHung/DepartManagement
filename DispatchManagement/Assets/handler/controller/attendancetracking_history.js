(function (windown, $) {
    window.attendancetracking_history = {
        init: function () {
            //attendancetracking_history.search();
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
            $("#btnSearch").on('click', function () {
                attendancetracking_history.search();
            })
            $('#PositionId').select2().on('select2:select', function () {
                $.ajax({
                    url: '/AttendanceTracking_History/CheckPositionDepartment',
                    data: {
                        IdDepartment: $("#DepartmentId").val(),
                        IdPosition: $("#PositionId").val()
                    },
                    type: "Post",
                    success: function (rs) {
                        if (!rs.stt) {
                            toastr["error"](rs.mess);
                        }
                    },
                });
            });
            $("#btnExport").on('click', function () {
                $.ajax({
                    url: '/AttendanceTracking_History/ExportAttendance',
                    data: {
                        fromDate: $("#fromDate").val(),
                        toDate: $("#toDate").val(),
                        idEmployee: $("#employee").val(),
                    },
                    type: 'post',
                    success: function (rt) {
                        if (rt.status == true) {
                            attendancetracking_history.saveFileAs(rt.FileLink, rt.FileName);
                        }
                    }
                });
            });
            $("#tbl-attendancetrackinghistory").on('click', ".deleteAttendanceTrackingHistory", function () {
                $(".deleteAttendanceTrackingHistory").attr('value', $(this).data("id"));
            })
            $(".deleteAttendanceTrackingHistory").on("click", function () {
                $.ajax({
                    type: "POST",
                    url: "/AttendanceTracking_History/Delete",
                    data: { Id: $(this).val() },
                    success: function (rp) {
                        location.reload();
                    }
                })
            });
            $('#employee').select2({
                placeholder: 'Nhập tên nhân viên để tìm kiếm',
                minimumInputLength: 1,
                maximumSelectionLength: 2,
                ajax: {
                    url: '/AttendanceTracking_History/EmployeeSearch',
                    dataType: 'json',
                    type: "POST",
                    delay: 350,
                    data: function (params) {
                        return {
                            keyword: params.term,
                        }
                    },
                    processResults: function (data, params) {
                        return {
                            results: $.map(data.data, function (items) {
                                console.log(items);
                                return {
                                    text: items.FirstName + items.LastName,
                                    id: items.Id,
                                    data: items,
                                }
                            })
                        }
                    }
                },
                templateResult: function (data) {
                    if (data.loading)
                        return data.text;
                    var $result = $("<span></span>");
                    $result.text(data.text);
                    $result.append(" <span class='badge'>" + data.id + "</span>");
                    //$result.append(" <span class='label label-primary'>" + data.quantity + "</label>");
                    return $result;
                }
            }).on('select2:select', function (evt) {
                var data = evt.params.data;
                $('#employeeId').val(data.id);
            });
        },
        saveFileAs: function (uri, filename) {
            var link1 = document.createElement('a');
            if (typeof link1.download === 'string') {
                link1.href = uri;
                link1.download = filename;

                //Firefox requires the link to be in the body
                document.body.appendChild(link1);

                //simulate click
                link1.click();

                //remove the link when done
                document.body.removeChild(link1);
            } else {
                window.open(uri);
            }
        },
        search: function () {
            $("#tbl-AttendanceTrackingHistory").bootstrapTable('refresh')
            var searchUrlInventory = "/AttendanceTracking_History/GetAttendanceTrackingHistoryList";
            $("#tbl-AttendanceTrackingHistory").bootstrapTable({
                method: 'get',
                url: searchUrlInventory,
                queryParams: function (p) {
                    var param = $.extend(true, {
                        limit: p.limit,
                        offset: p.offset,
                        fromDate: $("#fromDate").val(),
                        toDate: $("#toDate").val(),
                        employee: $("#employeeId").val()
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
                        field: 'IdEmployee',
                        title: 'Mã nhân viên',
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'NameEmployee',
                        title: 'Tên nhân viên',
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'DateCheck',
                        title: 'Ngày chấm công',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            if (value != null && value.toString().indexOf('-') == -1) {
                                return attendancetracking_history.smalldateFormatJsonDMY(value);
                            }
                            return "";
                        }
                    },
                    {
                        field: 'DayOfWeek',
                        title: 'Thứ',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            if (value == "Sunday") return "Chủ Nhật";
                            if (value == "Monday") return "Thứ Hai";
                            if (value == "Tuesday") return "Thứ Ba";
                            if (value == "Wednesday") return "Thứ Tư";
                            if (value == "Thursday") return "Thứ Năm";
                            if (value == "Friday") return "Thứ Sáu";
                            if (value == "Saturday") return "Thứ Bảy";
                        }
                    },
                    {
                        field: 'CheckIn',
                        title: 'Giờ vào',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value == null) {
                                return "";
                            } else {
                                return value;
                            }
                        }
                    },
                    {
                        field: 'CheckOut',
                        title: 'Giờ ra',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value == null) {
                                return "";
                            } else {
                                return value;
                            }
                        }
                    },
                    {
                        field: 'TimeLate',
                        title: 'Đến trễ',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value < 1) {
                                return "";
                            } else {
                                return value;
                            }
                        }
                    },
                    {
                        field: 'TimeEarly',
                        title: 'Về sớm',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value < 1) {
                                return "";
                            } else {
                                return value;
                            }
                        }
                    },
                    {
                        field: 'Work',
                        title: 'Công',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value <= 0) {
                                return "";
                            } else {
                                return value;
                            }
                        }
                    },
                    {
                        field: 'OverTime',
                        title: 'Tăng ca',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value == null) {
                                return "";
                            } else {
                                return value;
                            }
                        }
                    },
                    {
                        field: 'Absent',
                        title: 'Vắng mặt',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value == null) {
                                return "";
                            } else {
                                return value;
                            }
                        }
                    },
                    //{
                    //    title: 'Chi tiết',
                    //    align: 'center',
                    //    valign: 'middle',
                    //    searchable: false,
                    //    formatter: function (value, row, index) {
                    //        return [
                    //            '<button class="btn-xs btn-danger delete deleteAttendanceTrackingHistory" data-id="' + row.Id + '" data-toggle="modal" data-target="#myModal">',
                    //            'Xóa quy định',
                    //            '</button>',
                    //        ].join(' ');
                    //    }
                    //}
                ],
                onLoadSuccess: function (data) {
                    if (data.status) {
                        $("#totalTimeEarly").text("Về sớm: " + data.totalTimeEarly);
                        $("#totalAbsent").text("Số ngày vắng: " + data.totalAbsent);
                        $("#totalTimeLate").text("Đến muộn: " + data.totalTimeLate);
                        $("#totalTimesLate").text("Số lần đến muộn: " + data.totalTimesLate);
                        $("#totalTimesEarly").text("Số lần về sớm: " + data.totalTimesEarly);
                        $("#totalMinutesOverTime").text("Tổng thời gian tăng ca: " + data.totalMinutesOverTime);
                        $("#forgetTracking").text("Số lần không chấm công khi về: " + data.forgetTracking);
                        $("#work").text("Tổng số công: " + data.work);
                    } else {
                        $("#totalTimeEarly").text("");
                        $("#totalAbsent").text("");
                        $("#totalTimeLate").text("");
                        $("#totalTimesLate").text("");
                        $("#totalTimesEarly").text("");
                        $("#totalMinutesOverTime").text("");
                        $("#forgetTracking").text("");
                        $("#work").text("");
                        $("#tbl-AttendanceTrackingHistory").bootstrapTable('removeAll');
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
    attendancetracking_history.init();
})