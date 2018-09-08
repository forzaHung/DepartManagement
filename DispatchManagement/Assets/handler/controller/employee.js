(function (window, $) {

    window.employee = {
        //TODO
        ui: {
            file: "#file",
            imageUpload: "#imageUpload",
            published: ".published",
            search: "#btnSearch",
            searchString: "#searchString",
            viewProfile: ".viewProfile"
        },
        init: function () {
            $(".activeUser").on('ifChanged', function () {
                //var access = $(this).iCheck('update')[0].checked;
                var access = $(this).prop("checked");
                var this1 = $(this);
                $.ajax({
                    type: "POST",
                    url: "/Employee/EditActive",
                    data: { userId: $(this).attr('module'), access: access },
                    dataType: "json",
                    success: function (response) {
                        if (!response.status) {
                            this1.prop('checked', !access);
                            if (access) {
                                $(".hover").removeClass("checked");
                            } else {
                                $(".hover").addClass("checked");
                            }
                            backend.alert({
                                type: "error",
                                text: "Lỗi hệ thống, vui lòng liên hệ với kỹ thuật"
                            });
                        } else {
                            backend.alert({
                                type: "info",
                                text: "Chuyển trạng thái thành công"
                            });
                        }
                    },
                    error: function () {
                    }
                })
            })
            $(employee.ui.file).on('change', function () {
                employee.readUrl(this);
            });
            $(employee.ui.published).on('ifChanged', function () {
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
                        NProgress.start();
                    },
                    success: function (d) {
                        if (d.status) {
                            window.location.reload();
                        } else {
                            alert("Cannot change status for this category. Please, contact to Administrator");
                        }
                        NProgress.done();
                    }

                });
            });
            $(employee.ui.search).on('click', function (e) {
                e.preventDefault();
                employee.search();
            });
            $('.reset_pass').on('click', function (e) {
                var id = $(this).attr("data-id");
                employee.resetPassword(id);
            });
            $(employee.ui.viewProfile).on('click', function (e) {
                var id = $(this).attr("data-id");
                window.location.href = "/Profile?id=" + id;
            })
        },
        readUrl: function (input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $(employee.ui.imageUpload).attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        },
        search: function () {
            var searchString = $(employee.ui.searchString).val().trim();
            var searchUrl = "/Employee/GetEmployee";
            $.ajax({
                url: searchUrl,
                data: {
                    searchString: searchString
                },
                beforeSend:function(){
                    NProgress.start();
                },
                success: function (result) {
                    $('#employeeList').html(result);
                    $('.reset_pass').on('click', function (e) {
                        var id = $(this).attr("data-id");
                        employee.resetPassword(id);
                    });
                    NProgress.done();
                }
            });
        },
        resetPassword: function (userId) {
            var url = "/User/ResetPass";
            $.ajax({
                url: url,
                type:"POST",
                data: {
                    userId: userId
                },
                beforeSend: function () {
                    NProgress.start();
                },
                success: function (result) {
                    if (result.status) {
                        backend.alert({
                            type: "info",
                            text: "Reset mật khẩu thành công. Mật khẩu mới sẽ là: 12345678. Bạn nên thay đổi mật khẩu sau khi đăng nhập."
                        });
                    } else {
                        backend.alert({
                            type: "error",
                            text: "Có lỗi xảy ra trong quá trình xử lý. Vui lòng Liên hệ với Kỹ thuật. "
                        });
                    }
                    NProgress.done();
                }
            });
        }
    };

})(window, jQuery);
$(document).ready(function () {
    employee.init();
});
