(function (window, $) {

    window.dispatchstatus = {
        //TODO
        ui: {         
            search: "#btnSearch",
            searchString: "#searchString"          
        },
        init: function () {
           
            $(dispatchstatus.ui.search).on('click', function (e) {
                e.preventDefault();
                dispatchstatus.search();
            });
           
           
        },
     
        search: function () {
            var searchString = $(dispatchstatus.ui.searchString).val().trim();
            var searchUrl = "/DispatchStatus/GetDispatchStatus";
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
                    $('#DispatchStatusList').html(result);
                   
                    NProgress.done();
                }
            });
        },
     
    };

})(window, jQuery);
$(document).ready(function () {
    dispatchstatus.init();
});
