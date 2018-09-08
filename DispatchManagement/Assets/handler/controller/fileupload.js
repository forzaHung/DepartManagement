(function (window, $) {

    window.fileupload = {
        //TODO
        ui: {
            tbodyPic: "#tblPic",
            tmpMustachePic: "#tmp-pic",
            pnListAll:"#pnListAll",
            pageSize: 5,
            pageCount: 0,
            pageIndex: 1,
        },
        init: function () {
            fileupload.regisControl();
            fileupload.allImage();
        },
        regisControl: function () {
            $(fileupload.ui.pnListAll).click(function (e) {
                e.preventDefault();
                fileupload.allImage();
            })
        },
        allImage: function (pageIndex) {
            var configs = fileupload.ui;
            if (pageIndex == undefined)
                pageIndex = configs.pageIndex;
            $.ajax({
                type: "GET",
                url: "/backend/FileUpload/showImages",
                data: {
                    pageIndex: pageIndex, pageSize: configs.pageSize,
                },
                dataType: "json",
                success: function (response) {
                    var template = $(configs.tmpMustachePic).html();
                    var render = "";
                    if (response.status) {
                        $.each(response.data, function (i, item) {
                            render += Mustache.render(template, {
                                id: item.Id,
                                picturePath: item.ThumbPath,
                                MimeType: item.MimeType,
                                PicType: item.PicType,
                                Album: item.Album,
                            });
                        });
                        if (render != undefined) {
                            $(configs.tbodyPic).html(render);
                        }
                        else
                            $(configs.tbodyPic).html('');

                        backend.wrapPaging({
                            totalRecord: response.total,
                            pageIndex: pageIndex,
                            pageSize: configs.pageSize
                        }, function (pageNumber) {
                            fileupload.allImage(pageNumber);
                        });
                    }
                }
            });
        },
        selectedImage: function () {

        },
    };

})(window, jQuery);
$(document).ready(function () {
    //fileupload.init();
});
