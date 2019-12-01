"use strict";

(function () {

    $(document).ready(
        () => {

            $('#SearchDocuments').click(function () {


                $.post("http://localhost:49467/SearchInCatalog/SearchDocument", {

                    'documentsNumber': $('#documentsNumber').val(),

                }, function (response) {

                    let result = JSON.parse(response);
                    $("#myDiv").html("");
                    if (result.status == 500) {
                        $('#myDiv').append(`<h4>Документ не найден </h4>`)
                    }
                    else {

                        let documentResponse = JSON.parse(response);

                        documentResponse.Documents.forEach(d => {
                            $('#myDiv').append(`<h4>Документ №${d.documentNumber} от ${d.date} </h4> 
                                                 `)

                            documentResponse.DocumentTitle.forEach(s => {
                                $('#myDiv').append(`<p> ${s.t}  Ед. изм. - ${s.ed}  Кол-во - ${s.amount} Цена за ед. - ${s.price} </p> 
                                                 `)
                            });

                        });

                    }// else


                });// function (response)

            }); // #CreateSpecification


        }); // $(document).ready(

})();// function ()
