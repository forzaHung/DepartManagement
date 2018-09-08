(function (window, $) {

    window.files = {
        //TODO
        ui: {         
            search: "#btnSearch",           
            searchCode: "#searchCode",
            searchName: "#searchName",          
            searchdateUpload: "#dateUpload"
        },
        init: function () {
           
            $(files.ui.search).on('click', function (e) {
                e.preventDefault();
                files.search();
            });
           
           
        },
     
        search: function () {            
            var Code = $(files.ui.searchCode).val().trim();
            var Name = $(files.ui.searchName).val().trim();         
            var dateUpload = $(files.ui.searchdateUpload).val();
            
            
            var searchUrl = "/files/GetFile";
            
            $.ajax({
                url: searchUrl,
                data: {
                    Code: Code,
                    Name: Name,                    
                    Date: dateUpload
                },
                beforeSend:function(){
                    NProgress.start();
                },
                success: function (result) {
                    $('#FileList').html(result);
                   
                    NProgress.done();
                }
            });
        },
     
    };

})(window, jQuery);
$(document).ready(function () {
    files.init();
   
});


