(function (window, $) {
    window.customertype = {
        //TODO
        ui: {
            btnSearch: "#btnSearch",
            searchName: "input[name='searchString']"

        },
        init: function () {
           
            $(customertype.ui.btnSearch).click(function (e) {

                e.preventDefault();
                customertype.search();
            });
            customertype.regisControl();
        },
        search: function () {
            var proName = $(customertype.ui.searchName).val().trim();

            var searchString = {
                searchName: proName,

            }
            searchString = JSON.stringify(searchString);
            
            var searchUrl = "/CustomerType/GetCustomerType";
            $.ajax({
                 
                url: searchUrl,
                data: {
                    searchString: searchString
                },
                success: function (result) {
                    //ChangeUrl("Index", searchUrl);
                    $('#customerTypeList').html(result);
                    customertype.init();
                    backend.alert({
                        type: "info",
                        text: "Kết quả tìm kiếm"
                    });
                    $('input.flat').iCheck({
                        checkboxClass: 'icheckbox_flat-green',
                        radioClass: 'iradio_flat-green'
                    });

                }
            });
        },
        regisControl: function () {
            $('.roles').on('ifChanged', function (e) {
                //e.preventDefault();
                var chckValue = $(this).iCheck('update')[0].checked;
                var moduleId = $(this).attr('module');

                var action = $(this).attr('action');
                customertype.updatePublish(moduleId, action, chckValue);
            });   
        },
        updatePublish: function (moduleID, action, access) {

            $.ajax({
                type: "POST",
                url: "/CustomerType/Publish",
                data: { moduleId: moduleID, action: action, access: access },
                dataType: "json",
                beforeSend: function () {
                    NProgress.start();
                },
                success: function (response) {
                    if (!response.status) {
                        backend.alert({
                            type: "error",
                            text: "Lỗi hệ thống, vui lòng liên hệ với kỹ thuật"
                        });

                    } else {
                        backend.alert({
                            type: "info",
                            text: "Thay đổi trạng thái thành công"
                        });
                    }
                    // roles.getPermission(groupID);
                    NProgress.done();
                },
                error: function () {
                    NProgress.done();
                }
            })
        },
        onSuccessAjax: function () {
            $('input.flat').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });
            customertype.init();
        },
    };

})(window, jQuery);
$(document).ready(function () {
    customertype.init();

});




