(function (window, $) {

    window.category = {
        //TODO
        ui: {
            file: "#file",
            imageUpload: "#imageUpload",
            published: ".published",
            ishot: ".ishot",
            exportMenu: "#exportMenu"
        },
        init: function () {
            $(category.ui.file).on('change', function () {
                category.readUrl(this);
            });
            $(category.ui.published).on('ifChanged', function () {
                var id = $(this).parent().parent().attr("data-id");
                var chckValue = $(this).iCheck('update')[0].checked;
                $.ajax({
                    url: "/Category/ChangeStatus",
                    type: 'POST',
                    data: {
                        id: id,
                        published: chckValue
                    },
                    beforeSend: function () {
                    },
                    success: function (d) {
                        console.log(d);
                        if (d.status) {
                            window.location.reload();
                        } else {
                            alert("Cannot change status for this category. Please, contact to Administrator");
                        }
                    }

                });
            });
            $(category.ui.ishot).on('ifChanged', function () {
                var id = $(this).parent().parent().attr("data-id");
                var chckValue = $(this).iCheck('update')[0].checked;
                $.ajax({
                    url: "/Category/SetHot",
                    type: 'POST',
                    data: {
                        id: id,
                        status: chckValue
                    },
                    beforeSend: function () {
                    },
                    success: function (d) {
                        if (d.status) {
                            window.location.reload();
                        } else {
                            alert(d.msg);
                        }
                    }

                });
            });
            $(category.ui.exportMenu).on('click', function (e) {
                $.ajax({
                    url: "/Backend/Category/ExportMenu",
                    type: 'GET',
                    //data: {
                    //    id: id,
                    //    published: chckValue
                    //},
                    beforeSend: function () {
                        NProgress.start();
                        NProgress.inc();
                    },
                    success: function (d) {
                        if (d.status) {
                            backend.alert({
                                type: "info",
                                title: "Notification",
                                text: "Export menu successful"
                            });
                        } else {
                            backend.alert({
                                type: "error",
                                title: "Error",
                                text: "Export menu Unsuccess. Contact to Administrator."
                            });
                        }
                        NProgress.done();
                    },
                    error: function () {
                        NProgress.done();
                    }

                });
            });
        },
        readUrl: function (input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $(category.ui.imageUpload).attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    };

})(window, jQuery);
$(document).ready(function () {
    category.init();
});
