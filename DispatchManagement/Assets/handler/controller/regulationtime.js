(function (windown, $) {
    window.regulationtime = {
        init: function () {
            regulationtime.search();
            $("#searchRegulation").on('click', function () {
                regulationtime.search();
            })
            $('#PositionId').select2().on('select2:select', function () {
                $.ajax({
                    url: '/RegulationTime/CheckPositionDepartment',
                    data: {
                        IdDepartment: $("#DepartmentId").val(),
                        IdPosition: $("#PositionId").val()
                    },
                    type: "Post",
                    success: function (rs) {
                        if (!rs.stt) {
                            backend.alert({
                                type: "info",
                                title: "Thông báo",
                                text: rs.mess
                            });
                        }
                    },
                });
            });
            $("#tbl-regulation").on('click', ".deleteRegulationTime", function () {
                $(".deleteRegulation").attr('value', $(this).data("id"));
            })
            $(".deleteRegulation").on("click", function () {
                $.ajax({
                    type: "POST",
                    url: "/RegulationTime/Delete",
                    data: { Id: $(this).val() },
                    success: function (rp) {
                        location.reload();
                    }
                })
            });
        },
        search: function () {
            $("#tbl-regulation").bootstrapTable('refresh')
            var searchUrlInventory = "/RegulationTime/GetRegulationTimeList";
            $("#tbl-regulation").bootstrapTable({
                method: 'get',
                url: searchUrlInventory,
                queryParams: function (p) {
                    var param = $.extend(true, {
                        limit: p.limit,
                        offset: p.offset,
                        keyword: $("#SearchString").val()
                    }, p);
                    return param;
                },
                striped: true,
                sidePagination: 'server',
                pagination: true,
                paginationVAlign: 'both',
                limit: 25,
                pageSize: 25,
                pageList: [25, 50, 100, 200],
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
                        field: 'Department',
                        title: 'Phòng ban',
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'Position',
                        title: 'Chức vụ',
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'AllowedLate',
                        title: 'Thời gian đi trễ một tháng tối đa',
                        align: 'center',
                        valign: 'middle',
                        editable: {
                            type: 'text',
                            title: 'phút',
                            validate: function (value) {
                                value = $.trim(value);
                                var RE = /^[0-9]\d*(\.\d+)?$/;
                                if (!value) {
                                    return 'Thời gian đi trễ không được bỏ trống';
                                }
                                if (!RE.test(value)) {
                                    return 'Kiểu dữ liệu không hợp lệ.'
                                }
                                var data = $("#tbl-regulation").bootstrapTable('getData'),
                                    index = $(this).parents('tr').data('index');
                                if (data[index] == undefined)
                                    return 'Lỗi';
                                $.ajax({
                                    url: '/RegulationTime/ChangeAllowedLate',
                                    data: { Id: data[index].Id, allowedLate: value },
                                    type: 'Post',
                                    success: function (res) {
                                        backend.alert({
                                            type: "info",
                                            title: "Thông báo",
                                            text: res.alertContent
                                        });
                                    }
                                })
                                return '';
                            }
                        },
                        formatter: function (value, row, index) {
                            return value;
                        }
                    }, {
                        field: 'AllowedEarly',
                        title: 'Thời gian về sớm một tháng tối đa',
                        align: 'center',
                        valign: 'middle',
                        editable: {
                            type: 'text',
                            title: 'phút',
                            validate: function (value) {
                                value = $.trim(value);
                                var RE = /^[0-9]\d*(\.\d+)?$/;
                                if (!value) {
                                    return 'Thời gian về sớm không được bỏ trống';
                                }
                                if (!RE.test(value)) {
                                    return 'Kiểu dữ liệu không hợp lệ.'
                                }
                                var data = $("#tbl-regulation").bootstrapTable('getData'),
                                    index = $(this).parents('tr').data('index');
                                if (data[index] == undefined)
                                    return 'Lỗi';
                                $.ajax({
                                    url: '/RegulationTime/ChangeAllowedEarly',
                                    data: { Id: data[index].Id, allowedEarly: value },
                                    type: 'Post',
                                    success: function (res) {
                                        backend.alert({
                                            type: "info",
                                            title: "Thông báo",
                                            text: res.alertContent
                                        });
                                    }
                                })
                                return '';
                            }
                        },
                        formatter: function (value, row, index) {
                            return value;
                        }
                    }, {
                        field: 'TimesLate',
                        title: 'Số lần tối đa đến muộn một tháng',
                        align: 'center',
                        valign: 'middle',
                        editable: {
                            type: 'text',
                            title: 'phút',
                            validate: function (value) {
                                value = $.trim(value);
                                var RE = /^[0-9]\d*(\.\d+)?$/;
                                if (!value) {
                                    return 'Số lần đi muộn không được bỏ trống';
                                }
                                if (!RE.test(value)) {
                                    return 'Kiểu dữ liệu không hợp lệ.'
                                }
                                var data = $("#tbl-regulation").bootstrapTable('getData'),
                                    index = $(this).parents('tr').data('index');
                                if (data[index] == undefined)
                                    return 'Lỗi';
                                $.ajax({
                                    url: '/RegulationTime/ChangeTimesLate',
                                    data: { Id: data[index].Id, timesLate: value },
                                    type: 'Post',
                                    success: function (res) {
                                        backend.alert({
                                            type: "info",
                                            title: "Thông báo",
                                            text: res.alertContent
                                        });
                                    }
                                })
                                return '';
                            }
                        },
                        formatter: function (value, row, index) {
                            return value;
                        }
                    },
                    {
                        title: 'Chi tiết',
                        align: 'center',
                        valign: 'middle',
                        searchable: false,
                        formatter: function (value, row, index) {
                            return [
                                '<button class="btn-xs btn-danger delete deleteRegulationTime" data-id="' + row.Id + '" data-toggle="modal" data-target="#myModal">',
                                'Xóa quy định',
                                '</button>',
                            ].join(' ');
                        }
                    }
                ],
                onLoadSuccess: function (data) {
                    if (data.status) {
                        if (data.total > 0 && data.rows.length === 0) {
                            $("#tbl-regulation").bootstrapTable('refresh')
                        }
                    } else {
                        backend.alert({
                            type: "info",
                            title: "Thông báo",
                            text: "Không tìm thấy!"
                        });
                    }

                },
            });
        }
    };
})(window, jQuery);
$(document).ready(function () {
    regulationtime.init();
})