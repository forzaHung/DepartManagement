(function (window, $) {
    window.project_roles = {
        ui: {
            //userId: "#userId"
        },
        init: function () {
            project_roles.regisControl();

        },
        regisControl: function () {
            $('.roles').on('ifChanged', function (e) {
                e.preventDefault();
                var chckValue = $(this).iCheck('update')[0].checked;
                var ProjectId = $(this).attr('projectid');
                var userId = $(this).attr('UserId');
                var action = $(this).attr('action');
                project_roles.updatePermission(userId, ProjectId, action, chckValue);
            });
        },
        updatePermission: function (userId, ProjectId, action, access) {
            $.ajax({
                type: "POST",
                url: "/project_roles/EditRoles",
                data: { userId: userId, ProjectId: ProjectId, action: action, access: access },
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
    project_roles.init();
})