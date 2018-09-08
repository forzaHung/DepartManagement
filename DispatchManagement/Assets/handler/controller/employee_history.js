(function (window, $) {

    window.employee_history = {
        //TODO
        ui: {         
            search: "#btnSearch",
            searchString: "#searchString"          
        },
        init: function () {
           
            $(employee_history.ui.search).on('click', function (e) {
                e.preventDefault();
                employee_history.search();
            });
           
           
        },
     
        search: function () {
            var searchString = $(employee_history.ui.searchString).val().trim();
            var searchUrl = "/Employee_History/GetEmployee_History";
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
                    //window.location.href = "/Employee_History?searchString=" + searchString;
                    $('#Employee_HistoryList').html(result);
                   
                    NProgress.done();
                }
            });
        },
     
    };

})(window, jQuery);
$(document).ready(function () {
    employee_history.init();
});
