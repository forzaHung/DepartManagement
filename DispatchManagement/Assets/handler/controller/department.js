(function (window, $) {

    window.department = {
        //TODO
        ui: {         
            search: "#btnSearch",
            searchString: "#searchString"          
        },
        init: function () {
           
            $(department.ui.search).on('click', function (e) {
                e.preventDefault();
                department.search();
            });
           
           
        },
     
        search: function () {
            var searchString = $(department.ui.searchString).val().trim();
            var searchUrl = "/Department/GetDepartment";
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
                    $('#DepartmentList').html(result);
                   
                    NProgress.done();
                }
            });
        },
     
    };

})(window, jQuery);
$(document).ready(function () {
    department.init();
});
