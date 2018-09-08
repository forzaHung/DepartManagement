(function (window, $) {
    window.roles = {
        ui: {
            userId: "#userId"
        },
        init: function () {
            roles.regisControl();

        },
        regisControl: function () {
            $('.roles').on('ifChanged', function (e) {
                e.preventDefault();
                var chckValue = $(this).iCheck('update')[0].checked;
                var moduleId = $(this).attr('module');
                var userId = $(roles.ui.userId).val();
                var action = $(this).attr('action');
                roles.updatePermission(userId, moduleId, action, chckValue);
            });
        },
        updatePermission: function (userId, moduleID, action, access) {
            $.ajax({
                type: "POST",
                url: "/roles/EditRoles",
                data: { userId: userId, moduleId: moduleID, action: action, access: access },
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
                            text: "Cấp quyền thành công"
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
    }
})(window, jQuery)
$(function () {
    roles.init();
})