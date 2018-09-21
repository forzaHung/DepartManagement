$(document).ready(function () {
 
    //label text input
    check_fc();
    $('.form-control.ocb').click(function () {
        $(this).children('input').focus();
        check_fc();
    });
    $(window).click(function () {
        check_fc();
    });
    function check_fc() {
        $('.field_input').focus(function () {
            $(this).parent().addClass('focus');
        });
        $('.field_input').blur(function () {
            if (!$(this).val()) {
                $(this).parent().removeClass('focus');
            }
        });
        $.each($('.field_input'), function (i, e) {
            if ($(this).val() != '') {
                $(this).parent().addClass('focus');
            }

        });
        /*$.each($('.select_ocb'),function(i,e){
			if($(this).find('select').val()==""||$(this).find('select').val()==" "){
			$(this).parent().addClass('focus');
			}
		});*/
    }
    //pack();
    //set moving for assitan				
    //pikachuMoving(".field_input");

  

});

function textboxFocus(e) {
    $(e).focus(function () {
        var $p = $(this).closest('.form-control.ocb');
        $p.css('border-color', '#0066b3');
    });
    $(e).blur(function () {
        var $p = $(this).closest('.form-control.ocb');
        $p.css('border-color', '');
    });
}

function LoadCustomer(Customer_TypeId)
{
    if (Customer_TypeId == 0)
    {
        $('#CustomerList').hide(200);
      
    }
    else
    {
        $('#CustomerList').show(200);

        $.ajax({
            url: "/Project/GetCustomer",
            type: 'GET',
            data: {
                Customer_TypeId: Customer_TypeId
            },
            beforeSend: function () {
                //commonBase.startLoading();
            },
            success: function (d) {
              
                var template = $('#tmp_customer').html();
           
                var render = "";
             
                if (d.status) {
                    var result = $.parseJSON(d.data);

                    var data1 = '{"Customer":' + d.data + '}';
                    var data = JSON.parse(data1);

                    render += Mustache.to_html(template, data);
                    $('#CustomerId').html(render);

                  
                }
            }

        });

    }
  
}


(function (window, $) {

    window.project = {
        //TODO
        ui: {
        
            search: "#btnSearch",
            searchString: "#searchString"
           
        },
        init: function () {
           
            $(project.ui.search).on('click', function (e) {
               
                e.preventDefault();
                project.search();
            });
           
        
        },
     
        search: function () {
            var searchString = $(project.ui.searchString).val().trim();
            var searchUrl = "/Project/GetProject";
            $.ajax({
                url: searchUrl,
                data: {
                    searchString: searchString
                },
                beforeSend: function () {
                    NProgress.start();
                },
                success: function (result) {
                    $('#projectList').html(result);
                  
                    NProgress.done();
                }
            });
        }
        
    };

})(window, jQuery);
$(document).ready(function () {
    project.init();
});
