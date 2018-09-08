(function (window, $) {

    window.dispatchin = {
        //TODO
        ui: {         
            search: "#btnSearch",           
            searchCode: "#searchCode",
            searchName: "#searchName",
            ddlType: "#ddlType option:selected",
            ddlDepartment: "#ddlDepartment option:selected",
            ddlStatus: "#ddlStatus option:selected",
            dateFrom: "#dateFrom",
            getAddressToId:"#AddressToId",
            getType: "#Type",
            getStatus: "#Status",
            getPriority: "#Priority"
        },
        init: function () {
           
            $(dispatchin.ui.search).on('click', function (e) {
                e.preventDefault();
                dispatchin.search();
            });
           
           
        },
     
        search: function () {
            var Code = $(dispatchin.ui.searchCode).val().trim();
            var Name = $(dispatchin.ui.searchName).val().trim();
            //if ($(dispatchin.ui.getType).val().trim() == "" || $(dispatchin.ui.getType).val()==0)
            //{
                var Type = $(dispatchin.ui.ddlType).val();
            //}
            //else
            //{
            //    var Type = $(dispatchin.ui.getType).val();
            //}
            var Department = $(dispatchin.ui.ddlDepartment).val();
            //if ($(dispatchin.ui.getStatus).val().trim() == "" || $(dispatchin.ui.getStatus).val()==0)
            //{
                var Status = $(dispatchin.ui.ddlStatus).val();
            //}
            //else
            //{
            //    var Status = $(dispatchin.ui.getStatus).val();
            //}
            var Priority = $(dispatchin.ui.getPriority).val();
            var DateFrom = $(dispatchin.ui.dateFrom).val();
            var AddressToId = $(dispatchin.ui.getAddressToId).val();
            
            var searchUrl = "/DispatchIn/GetDispatchIn";
            
            $.ajax({
                url: searchUrl,
                data: {
                    Code: Code,
                    Name: Name,
                    Type: Type,
                    Department: Department,
                    Status: Status,
                    Priority: Priority,
                   
                    AddressToId: AddressToId,
                    DateFrom: DateFrom
                },
                beforeSend:function(){
                    NProgress.start();
                },
                success: function (result) {
                    $('#DispatchInList').html(result);
                   
                    NProgress.done();
                }
            });
        },
     
    };

})(window, jQuery);
$(document).ready(function () {
    dispatchin.init();
   
});
function deleteDispatch(Id)
{    
    if (confirm('Bạn có muốn xóa công văn này không?') == true) {
        var AddressToId = getParameterByName('AddressToId');
        var Type = getParameterByName('Type');
        var Status = getParameterByName('Status');
        

        var DeleteUrl = "/DispatchIn/Delete";

        $.ajax({
            url: DeleteUrl,
            data: {
                Id: Id,
                AddressToId:AddressToId,
                Type:Type,
                Status: Status
            },
            beforeSend: function () {
                NProgress.start();
            },
            success: function (result) {
                $('#DispatchInList').html(result);

                NProgress.done();
            }
        });
        return true;
    } else {
        return false;
    }
   
}

function getParameterByName(name, url) {
    if (!url) {
        url = window.location.href;
    }
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}