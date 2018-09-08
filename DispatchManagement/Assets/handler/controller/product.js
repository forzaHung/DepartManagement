(function (window, $) {
    window.product = {
        //TODO
        ui: {
            imageRemove: "#imageRemove",
            productImage: "#file",
            imageUpload: "#imageUpload",
            btnSearch: "#btnSearch",
            ddlMaker: "#ddlMaker",
            searchCode: "input[name='searchCode']",
            searchName: "input[name='searchName']",
            btnselectImage: "#btn-select-images",
            imgdelete: ".imgdelete",
            selectAvatar: "#btn-select-avatar",
            checkedValue: new Array(),

        },
        init: function () {
            var config = product.ui;
            var id = $("#Id").val();
            if (id > 0) {
                var lstImg = $("#Images").val();
                if (lstImg != "") {
                    config.checkedValue = JSON.parse(lstImg);
                }
                product.showAlbum();
            }
            $(product.ui.imageRemove).off('click').on('click',
                function () {
                    $(config.imageUpload).removeAttr('src');
                    $("#PictureId").val(0);
                    $("#upload").addClass("hidden");
                });
            $(product.ui.productImage).on('change', function () {
                product.readUrl(this);
                $("#upload").removeClass("hidden");
            });
            $(product.ui.btnSearch).click(function (e) {
                e.preventDefault();
                product.search();
            });
            $(config.btnselectImage).click(function (e) {
                e.preventDefault();
                fileupload.allImage();
                fileupload.regisControl();
            });
            $(config.selectAvatar).click(function (e) {
                e.preventDefault();
                fileupload.allImage();
                fileupload.regisControl();
            });
            $("#btnSelect").click(function (e) {
                e.preventDefault();
                var selectType = $("#selectType").val();
                if (selectType === "album") {
                    product.selectAlbum();
                } else if (selectType === "avatar") {
                    var tmpArr = new Array();
                    $('.chkpic').each(function () {
                        if (this.checked) {
                            tmpArr.push({ id: $(this).attr("data-id"), value: this.value });
                        }
                    });

                    var lastImg = tmpArr.length - 1;
                    tmpArr = JSON.stringify(tmpArr[lastImg]);
                    var json = JSON.parse(tmpArr);
                    $(config.imageUpload).attr('src', json.value);
                    $("#PictureId").val(json.id);
                    $("#upload").removeClass("hidden");
                    $("#ModalUpload").modal("hide");
                }
            });
            $("#btn-select-images").click(function (e) {
                e.preventDefault();
                $("#selectType").val("album");
            })
            $("#btn-select-avatar").click(function (e) {
                e.preventDefault();
                $("#selectType").val("avatar");
            })
            $('.setHot').off('click').on('click', function () {
                var id = $(this).attr('data-id');
                $.ajax({
                    url: '/Backend/Product/SetHot',
                    data: { id: id },
                    dataType: "json",
                    type: "GET",
                    success: function (result) {
                        if (result.status) {
                            window.location.href = "/Backend/Product";
                        }
                        else {
                            var html = '';
                            html += '<table class="table table-hover table-bordered" id="tblHot">';
                            html += '<thead>';
                            html += '<tr><th>Product Name</th><th></th></tr>'
                            html += '</thead>';
                            html += '<tbody>';
                            for (i = 0; i < result.ListHot.length; i++) {
                                var item = result.ListHot[i];
                                html += '<tr>';
                                html += '<td>' + item.ProductName + '</td>';
                                html += '<td><input type="checkbox" checked="checked" class="chkHot" data-id="' + item.Id + '"/></td>';
                                html += '</tr>';
                            }
                            html += '</tbody>';
                            html += '</table>';
                            $('#listHot').html(html);
                            $('#idSetHot').val(id);
                            $('#modalHotList').modal('show');
                            $('.chkHot').off('change').on('change', function () {
                                var idList = $('#idRemoveHot').val();
                                if ($(this).is(':checked')) {
                                    var idRemove = $(this).attr('data-id');
                                    idList = idList.replace(idRemove, '').replace(',,', '');
                                }
                                else {
                                    var idRemove = $(this).attr('data-id');
                                    idList += ',' + idRemove;
                                }
                                var lastChar = idList.substr(idList.length - 1);
                                if (lastChar == ',') {
                                    idList = idList.substr(0, idList.length - 1);
                                }
                                var tempRemoveList = idList.split(',');
                                if (tempRemoveList.length > 5) {
                                    $('#spWarning').text('Số lượng sản phẩm hot ít nhất phải là 3');
                                    $('#btnSaveHot').hide();
                                }
                                else {
                                    $('#spWarning').text('Hệ thống đã được set đủ 6 sản phẩm hot, đề nghị loại bỏ bớt để thêm sản phẩm');
                                    $('#btnSaveHot').show();
                                }
                                $('#idRemoveHot').val(idList);
                            })
                        }
                    }
                });
            })
            $('#btnCancelHot').off('click').on('click', function () {
                $('#modalHotList').modal('hide');
            })
            $('#btnSaveHot').off('click').on('click', function () {
                var id = $('#idSetHot').val();
                var idRemoves = $('#idRemoveHot').val();
                $.ajax({
                    type: "POST",
                    url: "/Backend/Product/ChangeHot",
                    data: { id: id, idRemoves: idRemoves },
                    dataType: "json",
                    success: function (result) {
                        if (result.status) {
                            $('#modalHotList').modal('hide');
                            window.location.href = "/Backend/Product";
                        }
                    }
                });
            })            
            product.attribute.saveAttribute();
        },
        search: function () {
            var proCode = $(product.ui.searchCode).val().trim();
            var proName = $(product.ui.searchName).val().trim();
            var makerId = $(product.ui.ddlMaker).val();
            var searchString = {
                searchCode: proCode,
                searchName: proName,
                makerId: makerId
            }
            searchString = JSON.stringify(searchString);
            var searchUrl = "/Backend/Product/GetProducts";
            $.ajax({
                url: searchUrl,
                data: {
                    searchString: searchString
                },
                success: function (result) {
                    //ChangeUrl("Index", searchUrl);
                    $('#ProductList').html(result);
                }
            });
        },
        readUrl: function (input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $(product.ui.imageUpload).attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        },
        removeImage: function () {
            var config = product.ui;
            $(config.imgdelete).click(function (e) {
                e.preventDefault();

                var item = $(this).attr("data-value");
                alert(item)

                config.checkedValue = config.checkedValue.filter(function (e) { return e !== item })
                console.log(config.checkedValue);
                var tmpPic = $("#tmp-Selectedpic").html();
                var render = "";
                $.each(config.checkedValue, function (i, item) {
                    render += Mustache.render(tmpPic, {
                        picturePath: item,
                    });
                });
                $("#ulImg").html(render);
                $("#Images").val(JSON.stringify(config.checkedValue));
                product.removeImage();
            });
        },
        selectAlbum: function () {
            var config = product.ui;
            var inputElements = document.getElementsByClassName('chkpic');
            var tmpArr = new Array();
            if (config.checkedValue.length <= 0) {
                $('.chkpic').each(function () {
                    if (this.checked) {
                        config.checkedValue.push(this.value);
                    }
                });
            } else {
                $('.chkpic').each(function () {
                    if (this.checked) {
                        tmpArr.push(this.value);
                    }
                });
                //compare 2 array
                var retArr = jQuery.diffArr(config.checkedValue, tmpArr);
                if (retArr.length > 0) {
                    for (var i = 0; i < retArr.length; i++) {
                        if (retArr[i] != undefined) {
                            config.checkedValue.push(retArr[i]);
                        }
                    }
                }

            }
            if (config.checkedValue.length <= 0) {
                backend.alert({
                    type: "warning",
                    text: "Please select an images"
                });
                return;
            }
            else {
                product.showAlbum();
            }
            $("#ModalUpload").modal("hide");
            $("#Images").val(JSON.stringify(config.checkedValue));
            product.removeImage();
        },
        showAlbum: function () {
            var config = product.ui;
            var tmpPic = $("#tmp-Selectedpic").html();
            var render = "";
            $.each(config.checkedValue, function (i, item) {
                render += Mustache.render(tmpPic, {
                    picturePath: item,
                });
            });
            $("#ulImg").html(render);
        },
        attribute: {
            saveAttribute: function () {
                $(".btnsave").click(function (e) {
                    e.preventDefault();
                    var $that = $(this);
                    //if ($("#frmAttr").valid()) {
                    var checked = $that.parent().parent().find(".chkAttr");
                    if (checked.is(':checked')) {
                        var cateId = $("#cateId").val();
                        var productId = $("#productId").val();
                        var categoryAttributeID = checked.val();
                        var textPromt = $that.parent().parent().find(".textpromt").val();
                        var displayOrder = $that.parent().parent().find(".display_order").val();
                        var attrValue = $that.parent().parent().find(".attr_values").val();
                        var controltype = $that.parent().parent().find(".ddlTypeControl").val();
                        if (product.attribute.regisform(textPromt, displayOrder, attrValue) == false) {
                            return false;
                        }
                        var fd = new FormData();
                        fd.append("productId", productId);
                        fd.append("cateId", cateId);
                        fd.append("categoryAttributeID", categoryAttributeID);
                        fd.append("textPromt", textPromt);
                        fd.append("displayOrder", displayOrder);
                        fd.append("attrValue", attrValue);
                        fd.append("controltype", controltype);
                        $.ajax({
                            type: "POST",
                            url: "/Backend/Product/SaveAttribute",
                            data: fd,
                            //dataType: "json",
                            cache: false,
                            contentType: false,
                            processData: false,
                            beforeSend: function () {
                            },
                            success: function (response) {
                                var mess = "";
                                if (response.status) {
                                    backend.alert({
                                        title: "Thông báo",
                                        type: "info",
                                        text: response.msg
                                    });
                                    setTimeout(function () {
                                        window.location.reload();
                                    }, 1000);
                                }
                                else {
                                    backend.alert({
                                        title: "Chú ý",
                                        type: "warning",
                                        text: response.msg,
                                    });
                                }
                            },
                            error: function () {
                            }
                        });
                    } else {
                        backend.alert({
                            title: "Chú ý",
                            type: "warning",
                            text: "Vui lòng chọn thuộc tính."
                        });
                        return;
                    }
                    //}
                })
            },
            regisform: function (textpromt, order, values) {
                //select integers only
                var intRegex = /[0-9 -()+]+$/;
                if ($.trim(textpromt) === '') {
                    backend.alert({
                        title: "Chú ý",
                        type: "error",
                        text: "Vui lòng nhập tên hiển thị."
                    });
                    return false
                }
                if ($.trim(order) === '') {
                    backend.alert({
                        title: "Chú ý",
                        type: "error",
                        text: "Vui lòng nhập thứ tự hiển thị."
                    });
                    return false

                } else {
                    if (intRegex.test($.trim(order)) == false) {
                        backend.alert({
                            title: "Chú ý",
                            type: "error",
                            text: "thứ tự hiển thị phải là số tự nhiên"
                        });
                        return false
                    }
                }
                if ($.trim(values) === '') {
                    backend.alert({
                        title: "Chú ý",
                        type: "error",
                        text: "Vui lòng nhập giá trị thuộc tính"
                    });
                    return false
                }
                return true;
                //$("#frmAttr").validate({
                //    rules: {
                //        textpromt: {
                //            required: true,
                //            maxlength: 250
                //        },
                //        display_order: {
                //            required: true,
                //            number: true
                //        },
                //        attr_values: {
                //            required: true,
                //            maxlength: 250
                //        },

                //    }, errorElement: 'span'
                //});
                //product.attribute.saveAttribute();
            },
            detailAttr: function (productId, cateId) {
                $.ajax({
                    type: "GET",
                    url: "/Backend/Product/GetAttribute",
                    data: { productId: productId, cateId: cateId },
                    dataType: "json",
                    cache: false,
                    contentType: false,
                    processData: false,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        var mess = "";
                        if (response.status) {
                            
                        }
                        else {
                            
                        }
                    },
                    error: function () {
                    }
                });
            },
        },
    };

})(window, jQuery);
$(document).ready(function () {
    product.init();
});
function checkHot(id) {

}


