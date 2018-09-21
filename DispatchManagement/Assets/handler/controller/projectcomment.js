(function (window, $) {
    window.projectcomment = {
        init: function () {
            projectcomment.regisControl();
        },
        regisControl: function () {
            $(".panel-shadow .post-footer").delegate(".viewOpt", "click", function () {
                var stt = $(this).next().css("display");
                if (stt === "none") {
                    $(this).next().slideDown();
                    $(this).html("Ẩn trả lời");
                    var id = $(this).attr('data-commentID');
                    var projectID = $('#projectID').val();
                    clear(id, projectID);
                }
                else {
                    $(this).next().slideUp();
                    $(this).html("Hiện trả lời");
                }
            });
            $(".cha").on("click", function () {
                var comment = $('#txtComment').val();
                var projectID = $('#projectID').val();
                var id = 0;
                projectcomment.addCommentCha(id, comment, projectID);
            });
            $(".viewOpt").on("click", function () {
                var id = $(this).attr('data-commentID');
                projectcomment.loadcomment(id);
            });
            //$(".con").on("click", function () {
            //    var id = $(this).attr('data-comment');
            //    var projectID = $(this).attr('data-project');
            //    var comment = $('#txtCommentCon_' + id + '').val();
            //    alert(111);
            //    projectcomment.addCommentCha(id, comment, projectID);
            //});
        },
        loadcomment: function (id) {
            $.ajax({
                type: "POST",
                url: "/ProjectComment/loadComment",
                data: { id: id },
                dataType: "json",
                beforeSend: function () {
                    NProgress.start();
                },
                success: function (result) {
                    //ChangeUrl("Index", searchUrl);
                    if (result.status) {
                        var objdt = result.data;
                        $.each(objdt, function (i, item) {
                            $('#groupCommentCon_' + id + '').append(
                                 "<li class=\"comment\">" +
                                         "<a class=" + 'pull-left' + " href=" + '#' + ">" +
                                             "<img src=\"/Assets/images/img.jpg\" class=\"avatar\" alt=\"avatar\">" +
                                         "</a>" +
                                         "<div class=\"comment-body\">" +
                                             "<div class=\"comment-heading\">"
                                                 + "<h4 class=\"user\">" + (item.FullName) + "</h4>" +
                                                 "<h5 class=\"time\">" + ToJavaScriptDate(item.CreateDate) + "</h5>" +
                                             "</div>" +
                                             "<p>" + item.Description + "</p>" +
                                         "</div>" +
                                     "</li>");
                        });
                    }
                    NProgress.done();
                },
                error: function () {
                    NProgress.done();
                }
            });
        },
        addCommentCha: function (id, comment, projectID) {
            $.ajax({
                type: "POST",
                url: "/ProjectComment/CreateComment",
                data: { id: id, comment: comment, projectID: projectID },
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
                        if (response.data == 0) {
                            window.location.reload("/ProjectComment/Index/" + projectID+"");
                            backend.alert({
                                type: "info",
                                text: "Thêm bình luận thành công"

                            });
                        } else {
                            var id = response.data;
                            clear(id, projectID);
                            projectcomment.loadcomment(id);
                        }
                    }
                    // roles.getPermission(groupID);
                    NProgress.done();
                },
                error: function () {
                    NProgress.done();
                }
            })
        }
    };
})(window, jQuery);
$(document).ready(function () {
    projectcomment.init();

});
function ToJavaScriptDate(value) {
    if (value != "null" || value != "") {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        var dt = new Date(parseFloat(results[1]));
        return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear() + " " + dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
    }
}
function clear(id, projectID) {
    $('#groupCommentCon_' + id + '').html("");
    $('#groupCommentCon_' + id + '').append("<li>" +
                                    "<div class=\"input-group\">" +
                                        "<input class=\"form-control\" id=\"txtCommentCon_" + id + "\" placeholder=\"Thêm bình luận...\" type=\"text\">" +
                                        "<span class=\"input-group-addon\">" +
                                            "<a href=\"#\" ><i class=\"fa fa-edit con\" onclick=\"addcommentcon(this)\" data-comment=\"" + id + "\" data-project=\"" + projectID + "\"></i></a>" +
                                        "</span>" +
                                    "</div>" +
                                "</li>")
    
}

function addcommentcon(item)
{
    var id = $(item).attr('data-comment');
    var projectID = $(item).attr('data-project');
    var comment = $('#txtCommentCon_' + id + '').val();
    
    projectcomment.addCommentCha(id, comment, projectID);
}