"use strict";

let ajaxUrl1 = "http://localhost:49467/Specification/GetItemsProvider/";
let ajaxUrl2 = "http://localhost:49467/Specification/GetItemsContract/";


function GetAjaxReqvect(target1, target2, url) {

    var id2 = $(target1).val();

    $.ajax({
        type: 'GET',
        url: `${url}${id2}`,
        success: function (data) {

            $(target2).replaceWith(data);

        }
    });


}

$(document).ready(() => {


    $('body').on('change', '#Firm', function () {
        GetAjaxReqvect("#Firm", "#Provider", ajaxUrl1);
    });


    $('body').on('change', '#Provider', function () {
        GetAjaxReqvect("#Provider", "#Contract", ajaxUrl2);
    });

});

