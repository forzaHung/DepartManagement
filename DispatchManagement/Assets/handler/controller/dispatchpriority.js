(function (window, $) {

    window.dispatchpriority = {
        //TODO
        ui: {         
            search: "#btnSearch",
            searchString: "#searchString"          
        },
        init: function () {
           
            $(dispatchpriority.ui.search).on('click', function (e) {
                e.preventDefault();
                dispatchpriority.search();
            });
           
           
        },
     
        search: function () {
            var searchString = $(dispatchpriority.ui.searchString).val().trim();
            var searchUrl = "/DispatchPriority/GetDispatchPriority";
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
                    $('#DispatchPriorityList').html(result);
                   
                    NProgress.done();
                }
            });
        },
     
    };

})(window, jQuery);
$(document).ready(function () {
    dispatchpriority.init();
});
