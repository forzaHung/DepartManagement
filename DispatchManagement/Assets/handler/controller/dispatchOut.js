(function (window, $) {

    window.dispatchout = {
        //TODO
        ui: {         
            search: "#btnSearch",           
            searchCode: "#searchCode",
            searchName: "#searchName",
            ddlType: "#ddlType option:selected",
            ddlDepartment: "#ddlDepartment option:selected",
            ddlStatus: "#ddlStatus option:selected",
            dateWrite: "#dateWrite",
            getDepartmentId: "#DepartmentId",
            getType: "#Type",
            getStatus: "#Status",
            getPriority: "#Priority"
        },
        init: function () {
           
            $(dispatchout.ui.search).on('click', function (e) {
                e.preventDefault();
                dispatchout.search();
            });
           
           
        },
     
        search: function () {
            
            
            var Code = $(dispatchout.ui.searchCode).val().trim();
            var Name = $(dispatchout.ui.searchName).val().trim();
            if ($(dispatchout.ui.getType).val().trim() == "" || $(dispatchout.ui.getType).val()==0)
            {
                var Type = $(dispatchout.ui.ddlType).val();
            }
            else
            {
                var Type = $(dispatchout.ui.getType).val();
            }
            var Department = $(dispatchout.ui.ddlDepartment).val();
            if ($(dispatchout.ui.getStatus).val().trim() == "" || $(dispatchout.ui.getStatus).val()==0)
            {
                var Status = $(dispatchout.ui.ddlStatus).val();
            }
            else
            {
                var Status = $(dispatchout.ui.getStatus).val();
            }
            var Priority = $(dispatchout.ui.getPriority).val();
            var DateWrite = $(dispatchout.ui.dateWrite).val();
            var DepartmentId = $(dispatchout.ui.getDepartmentId).val();
            
            var searchUrl = "/DispatchOut/GetDispatchOut";
            
            $.ajax({
                url: searchUrl,
                data: {
                    Code: Code,
                    Name: Name,
                    Type: Type,
                    DepartmentId: DepartmentId,
                    Status: Status,
                    Priority:Priority,
                    Department: Department,
                    DateWrite:DateWrite
                },
                beforeSend:function(){
                    NProgress.start();
                },
                success: function (result) {
                    $('#DispatchOutList').html(result);
                   
                    NProgress.done();
                }
            });
        },
     
    };

})(window, jQuery);
$(document).ready(function () {
    dispatchout.init();
   
});
function deleteDispatch(Id)
{    
    if (confirm('Bạn có muốn xóa công văn này không?') == true) {
        var AddressToId = getParameterByName('AddressToId');
        var Type = getParameterByName('Type');
        var Status = getParameterByName('Status');
        

        var DeleteUrl = "/DispatchOut/Delete";

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
                $('#DispatchOutList').html(result);

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