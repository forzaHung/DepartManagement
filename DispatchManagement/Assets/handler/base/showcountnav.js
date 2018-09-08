(function (window, $) {

    window.showcount = {
        //TODO
        ui: {
            //left
            dispatchin_department_Left: "#navdispatchIn_Department_Left",
            dispatchin_status_Left: "#navdispatchIn_Status_Left",
            dispatchin_type_Left: "#navdispatchIn_Type_Left",
            dispatchout_department_Left: "#navdispatchOut_Department_Left",
            dispatchout_status_Left: "#navdispatchOut_Status_Left",
            dispatchout_type_Left: "#navdispatchOut_Type_Left",
           

        },
        init: function () {
            //dispatchIn Left

            showcount.countSync.allAPI();
            
            
        },

        search_dispatchin_department: function () {

            var searchUrlDepartment = "/Count/ShowMenuDepartMent";
            $.ajax({
                url: searchUrlDepartment,
                type: 'POST',
                data: {
                    id: 0
                },
                beforeSend: function () {
                    NProgress.start();
                },
                success: function (d) {

                    if (d.status) {

                        $.each(d.data, function (i, item) {
                            var searchUrl = "/Count/CountDispatchIn";
                            $.ajax({
                                url: searchUrl,
                                data: {
                                    searchType: 0,
                                    searchStatus: 0,
                                    AddressToId: item.Id
                                },
                                type: 'POST',
                                beforeSend: function () {
                                    NProgress.start();
                                },
                                success: function (result) {
                                    $('.ShowCount_' + item.Id + '').html(result);

                                    NProgress.done();
                                }
                            });
                        });
                        NProgress.done();
                    }
                }
            });
        },
        search_dispatchin_status: function () {

            var searchUrlStatus = "/Count/ShowMenuStatus";
            $.ajax({
                url: searchUrlStatus,
                type: 'POST',
                data: {
                    id: 0
                },
                beforeSend: function () {
                    NProgress.start();
                },
                success: function (d) {

                    if (d.status) {

                        $.each(d.data, function (i, item) {
                            var searchUrl = "/Count/CountDispatchIn";
                            $.ajax({
                                url: searchUrl,
                                data: {
                                    searchType: 0,
                                    searchStatus: item.Id,
                                    AddressToId: 0
                                },
                                type: 'POST',
                                beforeSend: function () {
                                    NProgress.start();
                                },
                                success: function (result) {
                                    $('.ShowCountStatus_' + item.Id + '').html(result);

                                    NProgress.done();
                                }
                            });
                        });

                    }
                }
            });
        },
        search_dispatchin_type: function () {

            var searchUrlType = "/Count/ShowMenuType";
            $.ajax({
                url: searchUrlType,
                type: 'POST',
                data: {
                    id: 0
                },
                beforeSend: function () {
                    NProgress.start();
                },
                success: function (d) {

                    if (d.status) {

                        $.each(d.data, function (i, item) {
                            var searchUrl = "/Count/CountDispatchIn";
                            $.ajax({
                                url: searchUrl,
                                data: {
                                    searchType: item.Id,
                                    searchStatus: 0,
                                    AddressToId: 0
                                },
                                type: 'POST',
                                beforeSend: function () {
                                    NProgress.start();
                                },
                                success: function (result) {
                                    $('.ShowCountType_' + item.Id + '').html(result);

                                    NProgress.done();
                                }
                            });
                        });

                    }
                }
            });
        },

        //dispatchOut
        search_dispatchout_department: function () {

            var searchUrlDepartment = "/Count/ShowMenuDepartMent";
            $.ajax({
                url: searchUrlDepartment,
                type: 'POST',
                data: {
                    id: 0
                },
                beforeSend: function () {
                    NProgress.start();
                },
                success: function (d) {

                    if (d.status) {

                        $.each(d.data, function (i, item) {
                            var searchUrl = "/Count/CountDispatchOut";
                            $.ajax({
                                url: searchUrl,
                                data: {
                                    searchType: 0,
                                    searchStatus: 0,
                                    DepartmentId: item.Id
                                },
                                type: 'POST',
                                beforeSend: function () {
                                    NProgress.start();
                                },
                                success: function (result) {
                                    $('.ShowCountOut_' + item.Id + '').html(result);

                                    NProgress.done();
                                }
                            });
                        });

                    }
                }
            });
        },
        search_dispatchout_status: function () {

            var searchUrlDepartment = "/Count/ShowMenuStatus";
            $.ajax({
                url: searchUrlDepartment,
                type: 'POST',
                data: {
                    id: 0
                },
                beforeSend: function () {
                    NProgress.start();
                },
                success: function (d) {

                    if (d.status) {

                        $.each(d.data, function (i, item) {
                            var searchUrl = "/Count/CountDispatchOut";
                            $.ajax({
                                url: searchUrl,
                                data: {
                                    searchType: 0,
                                    searchStatus: item.Id,
                                    DepartmentId: 0
                                },
                                type: 'POST',
                                beforeSend: function () {
                                    NProgress.start();
                                },
                                success: function (result) {
                                    $('.ShowCountOutStatus_' + item.Id + '').html(result);

                                    NProgress.done();
                                }
                            });
                        });

                    }
                }
            });
        },
        search_dispatchout_type: function () {

            var searchUrlDepartment = "/Count/ShowMenuType";
            $.ajax({
                url: searchUrlDepartment,
                type: 'POST',
                data: {
                    id: 0
                },
                beforeSend: function () {
                    NProgress.start();
                },
                success: function (d) {

                    if (d.status) {

                        $.each(d.data, function (i, item) {
                            var searchUrl = "/Count/CountDispatchOut";
                            $.ajax({
                                url: searchUrl,
                                data: {
                                    searchType: item.Id,
                                    searchStatus: 0,
                                    DepartmentId: 0
                                },
                                type: 'POST',
                                beforeSend: function () {
                                    NProgress.start();
                                },
                                success: function (result) {
                                    $('.ShowCountOutType_' + item.Id + '').html(result);

                                    NProgress.done();
                                }
                            });
                        });

                    }
                }
            });
        },
        /*
        * Sync Ajax
        */
        countSync: {
            countDepartmentIn: function () {
                showcount.search_dispatchin_department();
            },
            countStatusIn: function () {
                showcount.search_dispatchin_status();
            },
            countTypeIn: function () {
                showcount.search_dispatchin_type();
            },
            countDepartmentOut: function () {
                showcount.search_dispatchout_department();
            },
            countStatusOut: function () {
                showcount.search_dispatchout_status();
            },
            countTypeOut: function () {
                showcount.search_dispatchout_type();
            },
            allAPI: function () {
                $.when($.each(showcount.countSync, function (e, i) {
                    if (e != "allAPI") {
                        setTimeout(function () {
                            i();
                        }, 1000)
                    }
                })).then(function () {
                    console.log("=== End Sync ====");
                });
            }
},
    };

})(window, jQuery);
$(document).ready(function () {
    showcount.init();

});
