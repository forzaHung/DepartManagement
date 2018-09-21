(function (windown, $) {
    window.employee_payroll = {
        init: function () {
            employee_payroll.search();
            employee_payroll.searchConfig();
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
                employee_payroll.search();
            });
            $("#btnCreate").on('click', function () {

                if ($("#employee").val() == null) {
                    var r = confirm("Bạn có chắc chắn muốn tạo bảng lương cho toàn bộ nhân viên?");
                    if (r == true) {
                        $.ajax({
                            url: '/Employee_Payroll/CreatePayrollMulti',
                            data: {
                                fromDate: $("#fromDate").val(),
                                toDate: $("#toDate").val()
                            },
                            type: 'post',
                            success: function (rs) {
                                if (rs.stt) {
                                    toastr["success"]("Thêm mới cho tất cả nhân viên thành công bảng lương thành công!");
                                    employee_payroll.search();
                                } else {
                                    toastr["error"](rs.mess);

                                    employee_payroll.search();
                                    $("#displayReason").append(rs.displayReason);
                                }
                            }
                        });
                    }
                }
                else {
                    $.ajax({
                        url: '/Employee_Payroll/CreatePayroll',
                        data: {
                            fromDate: $("#fromDate").val(),
                            toDate: $("#toDate").val(),
                            idEmployee: $("#employee").val()
                        }
                        ,
                        type: 'post',
                        success: function (rs) {
                            if (rs.stt) {
                                toastr["success"]("Thêm mới bảng lương thành công!");
                                employee_payroll.search();
                            } else {
                                toastr["error"](rs.mess);
                            }
                        }
                    });
                }
            });
            $("#btnExport").on('click', function () {
                $.ajax({
                    url: '/Employee_Payroll/ExportAttendance',
                    data: {
                        fromDate: $("#fromDate").val(),
                        toDate: $("#toDate").val(),
                        idEmployee: $("#employee").val(),
                    },
                    type: 'post',
                    success: function (rt) {
                        if (rt.status == true) {
                            employee_payroll.saveFileAs(rt.FileLink, rt.FileName);
                        }
                    }
                });
            });
            $("#tbl-EmployeePayroll").on('click', ".deletePayroll", function () {
                $.ajax({
                    type: "POST",
                    url: "/Employee_Payroll/Delete",
                    data: { Id: $(this).data("id") },
                    success: function (rp) {
                        employee_payroll.search();
                    }
                })
            });
            $("#tbl-EmployeePayroll").on('click', ".UpdatePayroll", function () {
                $.ajax({
                    url: '/Employee_Payroll/UpdatePayroll',
                    data: {
                        Id: $(this).attr("data-id"),
                        fromDate: backend.smalldateFormatJsonYMD($(this).attr("data-FromDate")),
                        toDate: backend.smalldateFormatJsonYMD($(this).attr("data-ToDate")),
                        idEmployee: $(this).attr("data-IdEmployee"),

                    },
                    type: 'post',
                    success: function (rs) {
                        if (rs.stt) {
                            toastr["success"]("Cập nhật bảng lương thành công!");
                            employee_payroll.search();
                        } else {
                            toastr["error"](rs.mess);
                        }
                    }
                });
            });
            $('#employee').select2({
                placeholder: 'Nhập tên nhân viên để tìm kiếm',
                minimumInputLength: 1,
                maximumSelectionLength: 2,
                ajax: {
                    url: '/Employee_Payroll/EmployeeSearch',
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
            $('#employeeSearch').select2({
                placeholder: 'Nhập tên nhân viên để tìm kiếm',
                minimumInputLength: 1,
                maximumSelectionLength: 2,
                ajax: {
                    url: '/Employee_Payroll/EmployeeSearch',
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
                $('#employeeIdSearch').val(data.id);
            });
            $('#employeeConfig').select2({
                placeholder: 'Nhập tên nhân viên để tìm kiếm',
                minimumInputLength: 1,
                maximumSelectionLength: 2,
                ajax: {
                    url: '/Employee_Payroll/EmployeeSearch',
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
                $('#employeeIdConfig').val(data.id);
            });
            $("#btnSearchConfig").on('click', function () {
                employee_payroll.searchConfig();
            });
            $("#btnCreateConfigPayroll").on('click', function () {
                $.ajax({
                    url: '/Employee_Payroll/CreateConfigPayroll',
                    type: 'post',
                    data: { IdEmployee: $('#employeeIdConfig').val() },
                    success: function (rp) {
                        if (rp.stt) {
                            employee_payroll.searchConfig();
                            toastr["success"](rp.mess);
                        } else {
                            toastr["error"](rp.mess);
                        }
                    }
                });
            });
            $("#fromMonth").datepicker({
                format: "mm/yyyy",
                viewMode: "months",
                minViewMode: "months",
                autoclose: true,

            });
            $("#toMonth").datepicker({
                format: "mm/yyyy",
                viewMode: "months",
                minViewMode: "months",
                autoclose: true,
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
            $("#tbl-EmployeePayroll").bootstrapTable('refresh')
            var searchUrlInventory = "/Employee_Payroll/GetEmployeePayrollList";
            $("#displayReason").html("");
            $("#tbl-EmployeePayroll").bootstrapTable({
                method: 'get',
                url: searchUrlInventory,
                queryParams: function (p) {
                    var param = $.extend(true, {
                        limit: p.limit,
                        offset: p.offset,
                        fromMonth: "01/" + $("#fromMonth").val(),
                        toMonth: "01/" + $("#toMonth").val(),
                        employee: $("#employeeIdSearch").val()
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
                        field: 'NamePayroll',
                        title: 'Tên bảng lương',
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
                        field: 'WageAgreement',
                        title: 'Lương thỏa thuận',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            return value.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,').slice(0, -3);
                        }
                    },
                    {
                        field: 'StandardWorkDays',
                        title: 'Số ngày công tiêu chuẩn',
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'NumberDaysOfficialSalary',
                        title: 'Số ngày lương chính thức',
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'DaysProbationarySalary',
                        title: 'Số ngày lương thử việc',
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'TotalMonthlySalary',
                        title: 'Tổng lương tháng được hưởng',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            return value.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,').slice(0, -3);
                        }
                    },
                    {
                        field: 'PremiumSalary',
                        title: 'Lương đóng bảo hiểm',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            return value.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,').slice(0, -3);
                        }
                    },
                    {
                        field: 'CompulsoryInsurance',
                        title: 'Bảo hiểm bắt buộc',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            return value.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,').slice(0, -3);
                        }
                    },
                    {
                        field: 'UnionFees',
                        title: 'Kinh phí công đoàn',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            return value.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,').slice(0, -3);
                        }
                    },
                    {
                        field: 'FromDate',
                        title: 'Từ ngày',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            if (value != null && value.toString().indexOf('-') == -1) {
                                return employee_payroll.smalldateFormatJsonDMY(value);
                            }
                            return "";
                        }
                    },
                    {
                        field: 'ToDate',
                        title: 'Đến ngày',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            if (value != null && value.toString().indexOf('-') == -1) {
                                return employee_payroll.smalldateFormatJsonDMY(value);
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
                                '<button class="btn-xs btn-danger delete deletePayroll" style="width:130px"  data-id="' + row.Id + '" data-toggle="modal" data-target="#myModal">',
                                'Xóa bảng lương',
                                '</button>',
                                '<button class="btn-xs btn-default UpdatePayroll" style="width:130px" data-id="' + row.Id + '" data-IdEmployee="' + row.IdEmployee + '" data-FromDate="' + row.FromDate + '" data-ToDate="' + row.ToDate + '" data-toggle="modal" data-target="#myModal">',
                                'Cập nhật bảng lương',
                                '</button>',
                            ].join(' ');
                        }
                    }
                ],
                onLoadSuccess: function (data) {
                    if (data.status) {

                    } else {
                        $("#tbl-EmployeePayroll").bootstrapTable('removeAll');
                        toastr["error"](data.mess);
                    }

                },
            });
        },
        searchConfig: function () {
            $("#tbl-EmployeePayrollConfig").bootstrapTable('refresh')
            var searchUrlInventory = "/Employee_Payroll/GetEmployeePayrollConfigList";
            $("#tbl-EmployeePayrollConfig").bootstrapTable({
                method: 'get',
                url: searchUrlInventory,
                queryParams: function (p) {
                    var param = $.extend(true, {
                        limit: p.limit,
                        offset: p.offset,
                        employee: $("#employeeIdConfig").val()
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
                        field: 'NameEmployee',
                        title: 'Tên nhân viên',
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'WageAgreement',
                        title: 'Lương thỏa thuận',
                        align: 'center',
                        valign: 'middle',
                        //formatter: function (oldvalue) {
                        //    console.log(oldvalue);
                        //    value = oldvalue.toString().split(",").join("");
                        //    return parseInt(value).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,').slice(0, -3);
                        //},
                        editable: {
                            type: 'text',
                            title: 'việt nam đồng',
                            display: function (value) {
                                $(this).text(parseInt(value).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,').slice(0, -3));
                            },
                            validate: function (oldvalue) {
                                var result = true;
                                messResult = "";
                                value = oldvalue.split(",").join("");
                                console.log(value);
                                value = $.trim(value);
                                if (!value) {
                                    return 'Tiền lương không được bỏ trống';
                                }
                                
                                var RE = /^[0-9]\d*(\.\d+)?$/;
                                if (!RE.test(value)) {
                                    return 'Kiểu dữ liệu không hợp lệ.'
                                }
                                var data = $("#tbl-EmployeePayrollConfig").bootstrapTable('getData'),
                                    index = $(this).parents('tr').data('index');
                                if (data[index] == undefined)
                                    return 'Lỗi';
                                $.ajax({
                                    url: '/Employee_Payroll/ChangeWageAgreement',
                                    data: { Id: data[index].Id, wageAgreement: value },
                                    type: 'Post',
                                    success: function (res) {
                                        if (res.stt) {
                                            toastr["success"](res.mess);

                                        } else {
                                            toastr["error"](res.mess);
                                            result = false;
                                            messResult = res.mess;
                                        }
                                    },
                                    async: false
                                });
                                if (!result) return "Lỗi: " + messResult;
                                else return '';
                            }
                        },
                    },
                    {
                        field: 'ProbationaryFromDate',
                        title: 'Thử việc từ ngày',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            console.log(value);
                            console.log(employee_payroll.smalldateFormatJsonDMY(value));
                            if (value != null && value.toString().indexOf('-') == -1) {
                                return employee_payroll.smalldateFormatJsonDMY(value);
                            }
                            return "";
                        },
                        editable: {
                            title: 'Thời gian',
                            format: 'dd/mm/yyyy',
                            viewformat: 'dd/mm/yyyy',
                            type:'date',
                            validate: function (value) {                               
                                var result = true;
                                var messResult = "";
                                value = $.trim(value);
                                if (!value) {
                                    return 'Thời gian không được bỏ trống';
                                }
                                var data = $("#tbl-EmployeePayrollConfig").bootstrapTable('getData'),
                                    index = $(this).parents('tr').data('index');
                                if (data[index] == undefined)
                                    return 'Lỗi';
                                $.ajax({
                                    url: '/Employee_Payroll/ChangeProbationaryFromDate',
                                    data: { Id: data[index].Id, probationaryFromDate: employee_payroll.convertDate(value), IdEmployee: data[index].IdEmployee },
                                    type: 'Post',
                                    success: function (res) {
                                        if (res.stt) {
                                            toastr["success"](res.mess);
                                        } else {
                                            toastr["error"](res.mess);
                                            result = false;
                                            messResult = res.mess;
                                        }
                                    },
                                    async: false
                                });
                                if (!result) return messResult;
                                return "";
                                
                            }
                        },
                    },
                    {
                        field: 'ProbationaryToDate',
                        title: 'Thử việc đến ngày',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value) {
                            if (value != null && value.toString().indexOf('-') == -1) {
                                return employee_payroll.smalldateFormatJsonDMY(value);
                            }
                            return "";
                        },
                        editable: {
                            type: 'date',
                            title: 'Thời gian',
                            format: 'dd/mm/yyyy',
                            viewformat: 'dd/mm/yyyy',
                            validate: function (value) {
                                var result = true;
                                var messResult = "";
                                value = $.trim(value);
                                if (!value) {
                                    return 'Thời gian không được bỏ trống';
                                }
                                var data = $("#tbl-EmployeePayrollConfig").bootstrapTable('getData'),
                                    index = $(this).parents('tr').data('index');
                                if (data[index] == undefined)
                                    return 'Lỗi';
                                $.ajax({
                                    url: '/Employee_Payroll/ChangeProbationaryToDate',
                                    data: { Id: data[index].Id, probationaryToDate: employee_payroll.convertDate(value), IdEmployee: data[index].IdEmployee },
                                    type: 'Post',
                                    success:  function(res){
                                        if (res.stt) {
                                            toastr["success"](res.mess);
                                        } else {
                                            result = false;
                                            messResult = res.mess;
                                            toastr["error"](res.mess);
                                          
                                        }
                                    },
                                    async: false
                                });
                                if (!result) return "Lỗi: " + messResult;
                                else return "";
                            }
                        },
                    },
                ],
                onLoadSuccess: function (data) {
                    if (data.status) {

                    } else {
                        $("#tbl-EmployeePayrollConfig").bootstrapTable('removeAll');
                        toastr["error"](data.mess);
                    }

                },
            });
        },
        convertDate: function (d) {
            var parts = d.split(" ");
            var months = { Jan: "01", Feb: "02", Mar: "03", Apr: "04", May: "05", Jun: "06", Jul: "07", Aug: "08", Sep: "09", Oct: "10", Nov: "11", Dec: "12" };
            return parts[3] + "-" + months[parts[1]] + "-" + parts[2];
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
    employee_payroll.init();
})