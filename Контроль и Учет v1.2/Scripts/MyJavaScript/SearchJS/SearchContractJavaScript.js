"use strict";

(function () {

    $(document).ready(
        () => {

            $('#SearchContract').click(function () {


                $.post("http://localhost:49467/SearchInCatalog/SearchСontract", {

                    'contractNumber': $('#contractNumber').val(),

                }, function (response) {

                    let result = JSON.parse(response);
                    $("#myDiv").html("");
                    if (result.status == 500) {
                        $('#myDiv').append(`<h4>Договор не найден </h4>`)

                    }
                    else {

                        let contractResponse = JSON.parse(response); 

                        contractResponse.Contracts.forEach(c => {
                            $('#myDiv').append(`<h2>Договор №${c.contractNumber}</h2> 
                                                    <h4>Дата подписания - ${c.dateFrom}</h4>
                                                    <h4>Дата окончания действия - ${c.dateEnd}</h4>
                                                    <h4>Условия поставки по договору - ${c.incoterms} </h4>
                                                     `)
                        });

                    }// else


                });// function (response)

            }); // #CreateSpecification


        }); // $(document).ready(

})();// function ()
