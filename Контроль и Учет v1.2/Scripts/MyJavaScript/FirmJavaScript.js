"use strict";


(function () {


$(document).ready(() => {

    let requestSetings = {
        ajaxUrl: 'http://localhost:49467/',
        getFirmsMethod: 'SearchInCatalog/AjaxList',
        offset: 0,
        limit: 2
    };

    $('#more').click(function () {

        $.get(`${requestSetings.ajaxUrl}${requestSetings.getFirmsMethod}`,
            { offset: requestSetings.offset, limit: requestSetings.limit },
              function (response) {


                let firmsResponse = JSON.parse(response);

                firmsResponse.Firms.forEach(f => {

                    $('#firmsContainer').append(`

    <h2>Фирма:${f.FirmTitle}</h2> <h4>Директор: ${f.DirectorSurname} ${f.DirectorName} ${f.DirectorSecondName}</h4>
     <a href="http://localhost:49467/SearchInCatalog/Details/${f.Id}">Подробная информация о фирме</a> 
    | <a href="http://localhost:49467/SearchInCatalog/Edit/${f.Id}">Редактировать информацию о фирме</a>

              `);

                   
                });

                requestSetings.offset += 2;

            });

    });

});

})();

