var TableManaged = function () {


    var initTableList = function () {

        var table = $('#tablelist');

        // begin first table
        table.dataTable({

            // Internationalisation. For more info refer to http://datatables.net/manual/i18n
            "language": {
                "aria": {
                    "sortAscending": ": activate to sort column ascending",
                    "sortDescending": ": activate to sort column descending"
                },
                "emptyTable": "Không có dữ liệu trong bảng",
                "info": "Showing _START_ to _END_ of _TOTAL_ entries",
                "infoEmpty": "No entries found",
                "infoFiltered": "(filtered1 from _MAX_ total entries)",
                "lengthMenu": "Show _MENU_ entries",
                "search": "Search:",
                "zeroRecords": "Không tìm thấy bản ghi"
            },

            // Uncomment below line("dom" parameter) to fix the dropdown overflow issue in the datatable cells. The default datatable layout
            // setup uses scrollable div(table-scrollable) with overflow:auto to enable vertical scroll(see: assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js). 
            // So when dropdowns used the scrollable div should be removed. 
            //"dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>",

            "bStateSave": false, // save datatable state(pagination, sort, etc) in cookie.
            //khai báo lại số cột

            "columns": [{
                "orderable": false
            }, {
                "orderable": false
            }, {
                "orderable": false
            }, {
                "orderable": false
            }, {
                "orderable": false
            }],
            "lengthMenu": [
                [5, 10, 15, -1],
                [5, 10, 15, "All"] // change per page values here
            ],
            // set the initial value
            "pageLength": 5,
            "pagingType": "bootstrap_full_number",
            "language": {
                "search": "My search: ",
                "lengthMenu": "  _MENU_ records",
                "paginate": {
                    "previous": "Prev",
                    "next": "Next",
                    "last": "Last",
                    "first": "First"
                }
            },
            "columnDefs": [{  // set default column settings
                'orderable': false,
                'targets': [0]
            }, {
                "searchable": false,
                "targets": [0]
            }],
            "order": [
                [1, "asc"]
            ] // set first column as a default sort by asc
        });

        var tableWrapper = jQuery('#sample_1_wrapper');

        table.find('.group-checkable').change(function () {
            var set = jQuery(this).attr("data-set");
            var checked = jQuery(this).is(":checked");
            jQuery(set).each(function () {
                if (checked) {
                    $(this).attr("checked", true);
                    $(this).parents('tr').addClass("active");
                } else {
                    $(this).attr("checked", false);
                    $(this).parents('tr').removeClass("active");
                }
            });
            jQuery.uniform.update(set);
        });

        table.on('change', 'tbody tr .checkboxes', function () {
            $(this).parents('tr').toggleClass("active");
        });

        tableWrapper.find('.dataTables_length select').addClass("form-control input-xsmall input-inline"); // modify table per page dropdown
    }

    return {

        //main function to initiate the module
        init: function () {
            if (!jQuery().dataTable) {
                return;
            }


            initTableList();
           
        }

    };

}();

var SearhOrder = {
    ui: {
        Add: "#btnSearch",
        changeStatus: ".changeStatus",
        ModalPrint: ".modal_print",
        PrintDocument: ".PrintDocument"
    },

    regisControl: function () {


        $(SearhOrder.ui.Add).off('click').on('click', function () {
            //window.location.href = "/Payments/Index";

            var fromDate = $('#FromDate').val();
            var toDate = $('#ToDate').val();
            var keyword = '';
            if (fromDate != null && fromDate.trim().length !== 0) {
                keyword = "?fromDate=" + fromDate;

                if (toDate != null && toDate.trim().length !== 0) {
                    keyword += "&toDate=" + toDate;
                }
            }
            var searchUrl = "/Backend/Order/Index" + keyword;
            alert(searchUrl);
            window.location.href = searchUrl;
        });

        $(SearhOrder.ui.changeStatus).off('click').on('click', function () {
            var Orderid = $(this).attr('data-oid');
            var getstatus = $(this).attr('data-value');

            $.ajax({
                url: "/Backend/Order/ChangeStatus",
                type: 'POST',
                data: {
                    Orderid: Orderid,
                    getstatus: getstatus
                },
                beforeSend: function () {
                    //commonBase.startLoading();
                },
                success: function (d) {
                    if (d.status) {
                        window.location.href = "/Backend/Order";
                    }

                }

            });
        });
        $(SearhOrder.ui.ModalPrint).off('click').on('click', function () {
            var Orderid = $(this).attr('data-oid');
            $.ajax({
                url: "/Backend/Order/GetOrderDetail",
                type: 'Post',
                data: {
                    Orderid: Orderid
                },
                beforeSend: function () {
                    //commonBase.startLoading();
                },
                success: function (d) {
                    var template = $('#tmp_Order').html();
                    var templatelist = $('#tmp_OrderList').html();
                    var templateprice = $('#tmp_TotalPrice').html();
                    var render = "";

                    if (d.status) {
                        render = "";

                        render += Mustache.render(template, {
                            id: d.data.Id,
                            CustomerId: d.data.CustomerId,
                            OrderDate: d.data.OrderDateConvert,
                            Status: d.data.Status,
                            CustomerName: d.data.CustomerName,
                            OrderDetails: d.data.orderDetails,
                            Address: d.data.Address,
                            Phone: d.data.Phone,
                            Note: d.data.Note,
                            AddressID: d.data.Id,
                            OrderStatusStr: d.data.OrderStatusStr
                        });

                        $('#Fill_OrderDetail').html(render);

                        //fill to list

                        var renderlist = "";
                        var UnitPrice = 0;
                        var SellPrice = 0;
                        $.each(d.data.OrderDetails, function (i, item) {
                            UnitPrice = item.UnitPrice.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"); //convert giá tiền
                            if (item.SellPrice != null) {
                                SellPrice = item.SellPrice.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"); //convert giá tiền
                            }
                            else {
                                SellPrice = item.SellPrice;
                            }
                            console.log(UnitPrice);
                            renderlist += Mustache.render(templatelist, {
                                ProductName: item.ProductName,
                                ProductCode: item.ProductCode,
                                MakerName: item.MakerName,
                                ProductImage: item.ProductImage,
                                UnitPrice: UnitPrice,
                                SellPrice: SellPrice,
                                Quantity: item.Quantity
                            });
                        })
                        $('#filltrOrderList').html(renderlist);
                        //get tổng giá
                        var renderprice = "";
                        var sum = 0;
                        var sumsell = 0;
                        $.each(d.data.OrderDetails, function (i, item) {
                            sum += item.UnitPrice * item.Quantity;
                            sumsell += item.SellPrice * item.Quantity;
                        })
                        sum = sum.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"); //convert giá tiền
                        sumsell = sumsell.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"); //convert giá tiền
                        renderprice = Mustache.render(templateprice, {
                            Sum: sum,
                            SumSell: sumsell
                        });
                        //console.log(sum);
                        $('#fillTotalPrice').html(renderprice);
                    }
                }
            });
        });
        $(SearhOrder.ui.PrintDocument).off('click').on('click', function () {
            var divName = 'printableArea';
            printDiv(divName);
        });
    }
}
$(document).ready(function () {

    SearhOrder.regisControl();

});
function printDiv(divName) {
    var printContents = document.getElementById(divName).innerHTML;
    var originalContents = document.body.innerHTML;

    document.body.innerHTML = printContents;

    window.print();

    document.body.innerHTML = originalContents;
}
