(function (window, $) {

    window.dispatchtype = {
        //TODO
        ui: {         
            search: "#btnSearch",
            searchString: "#searchString"          
        },
        init: function () {
           
            $(dispatchtype.ui.search).on('click', function (e) {
                e.preventDefault();
                dispatchtype.search();
            });
           
           
        },
     
        search: function () {
            var searchString = $(dispatchtype.ui.searchString).val().trim();
            var searchUrl = "/DispatchType/GetDispatchType";
            console.log(searchUrl);
            $.ajax({
                url: searchUrl,
                data: {
                    searchString: searchString
                },
                beforeSend:function(){
                    NProgress.start();
                },
                success: function (result) {
                    $('#DispatchTypeList').html(result);
                   
                    NProgress.done();
                }
            });
        },
     
    };

})(window, jQuery);
$(document).ready(function () {
    dispatchtype.init();
});
