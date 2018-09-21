(function (window, $) {
    window.projectstatus = {
        //TODO
        ui: {
            btnSearch: "#btnSearch",
            item_Published: "#item_Published",
            searchName: "input[name='searchString']"

        },
        init: function () {

            //$(news.ui.imageRemove).off('click').on('click',
            //    function () {
            //        $.ajax({
            //            url: "/FileUpload/Remove",
            //            type: "POST",
            //            data: formData,
            //            cache: false,
            //            contentType: false,
            //            processData: false
            //        });
            //        $(news.ui.imageUpload).removeAttr('src');
            //        $('#upload').addClass('hide');
            //        $('#image').val('');
            //    });
            //$(news.ui.file).on('change', function () {
            //    var $that = $(this);
            //    var newFile = $that[0].files[0];
            //    if (newFile === undefined || newFile === null) {
            //        return;
            //    }
            //    var formData = new FormData();
            //    formData.append("newFile", newFile);
            //    $.ajax({
            //        url: "/FileUpload/Upload",
            //        type: "POST",
            //        data: formData,
            //        cache: false,
            //        contentType: false,
            //        processData: false
            //    })
            //    .done(function (data) {
            //        // Handle returned fileUrl here.
            //        console.log('file done');
            //        if (data.Status) {
            //            $('#image').val(data.FileName);
            //            $('#upload').removeClass('hide');
            //            $('#editimages').addClass('hide');
            //            $(news.ui.imageUpload).attr('src', data.FolderPath + data.FileName);
            //        } else {
            //            alert("Error: " + data.Message);
            //        }
            //    })
            //    .fail(function (err) {
            //        console.log(err.responseText);
            //        $('#image').val(err.responseText);
            //        $('#upload').addClass('hide');
            //    })
            //    .always(function () {
            //        console.log('file always');
            //        $that.val('');
            //    });
            //});
            $(projectstatus.ui.btnSearch).click(function (e) {

                e.preventDefault();
                projectstatus.search();
            });
            projectstatus.regisControl();
        },
        search: function () {
            var proName = $(projectstatus.ui.searchName).val().trim();

            var searchString = {
                searchName: proName,

            }

                        
            searchString = JSON.stringify(searchString);
            var searchUrl = "/projectstatus/GetProjectStatus";
            $.ajax({
                url: searchUrl,
                data: {
                    searchString: searchString
                },
                success: function (result) {
                    //ChangeUrl("Index", searchUrl);

                    $('#ProjectStatusList').html(result);

                    projectstatus.init();
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
                projectstatus.updateActive(moduleId, action, chckValue);
            });
            $('.btnedit').on('click', function (e) {
                var moduleId = $(this).attr('module');
                //alert(moduleId);
                projectstatus.edit(moduleId);
            });
            //$('.ckbox').click(function () {
            //    if ($(this).is(':checked')) {


            //        var chckValue = true;
            //        var moduleId = $(this).attr('module');

            //        var action = $(this).attr('action');
            //        news.updatePublish(moduleId, action, chckValue);
            //    } else {

            //        var chckValue = false;
            //        var moduleId = $(this).attr('module');

            //        var action = $(this).attr('action');
            //        news.updatePublish(moduleId, action, chckValue);
            //    }
            //});
        },
        updateActive: function (moduleID, action, access) {

            $.ajax({
                type: "POST",
                url: "/ProjectStatus/Active",
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
            projectstatus.init();
        },
    };

})(window, jQuery);
$(document).ready(function () {
    projectstatus.init();

});




