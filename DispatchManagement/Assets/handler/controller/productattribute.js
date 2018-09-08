(function (window, $) {
    window.productattribute = {
        ui: {
            btnSearch: "#btnSearch",
            ddlCate: "#ddlCategory"
        },
        init: function () {
            $(productattribute.ui.btnSearch).click(function (e) {
                e.preventDefault();
                productattribute.search();
            })
        },
        search: function () {
            var cateID = $(productattribute.ui.ddlCate).val();            
            var searchUrl = "/Backend/ProductAttribute/GetProductAttributes";
            $.ajax({
                url: searchUrl,
                data: { cateID: cateID },
                success: function (result) {
                    console.log(result);
                    $('#ProductAttributeList').html(result);
                }
            });
        }
    }
})(window, jQuery); 
$(document).ready(function () {
    productattribute.init();
})