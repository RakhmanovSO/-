"use strict";

    (function () {

        $(document).ready(
            () => {

                $('#SearchFirm').click(function () {


                    $.post("http://localhost:49467/SearchInCatalog/SearchFirm", {

                        'titleFirm': $('#titleFirm').val(),

                    }, function (response) {

                        let result = JSON.parse(response);
                        $("#myDiv").html("");

                        if (result.status == 500) {
                            $('#myDiv').append(`<h4>Фирма не найдена </h4>`)

                        }
                        else {

                            let firmsResponse = JSON.parse(response);

                            firmsResponse.Firms.forEach(f =>
                            {
                                $('#myDiv').append(`<h2>Фирма: ${f.FirmTitle}</h2> 
                                                    <h4>Директор: ${f.DirectorSurname} ${f.DirectorName} ${f.DirectorSecondName}</h4>
                                                    <h4>Юридический адрес: ${f.addresLegalCountry} г.${f.addresLegalCity} ул.${f.addresLegalStreet} д.№${f.addresLegalHouseNumber} офис №${f.addresLegalOfficeNumber}</h4>
                                                     <h4>Физический адрес: ${f.addresActualCountry} г.${f.addresActualCity} ул.${f.addresActualStreet} д.№${f.addresActualHouseNumber} офис №${f.addresActualOfficeNumber}</h4>
                                                       `)
                            });
                        
                        }// else


                    });// function (response)

            }); // #CreateSpecification


        }); // $(document).ready(

})();// function ()
