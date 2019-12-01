"use strict";

(function () {

    $(document).ready(
        () => {

            $('#CreateDocument').click(function () {

                let getInputValuesElement = document.querySelector('#CreateDocument');
                let container = document.querySelector('#container2');

                //let dateValues = [];

                var SCD = {};

                SCD.contractID = +$('#contract').val();
                SCD.numberDocument = $('#numberDocument').val();

                SCD.Day = $('#Day1').val();
                SCD.Month = $('#Month1').val();

                SCD.Year = $('#Year1').val();

                SCD.NDS = $('#nds').val();

                var array = [];

                let inputs = container.querySelectorAll('input');

                inputs.forEach(input => {

                    array.push((input.value));
                });

                SCD.ArrayDocTitle = array;


                

                // AJAX ...

                //SCD = JSON.stringify({ 'SCD': SCD });

                /*
                $.ajax({
                    url: 'http://localhost:49467/Document/CreateDocument',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: SCD, 
                    success: function (response) {

                        let result = JSON.parse(response);

                        if (result.status === '500') {
                            alert('Ошибка сервера: Извините,что-то пошло не так.');

                        }//if
                        else {
                            alert('Документ успешно добавлен!');
                        }//else
                    }
                });
                */

                
                $.post("http://localhost:49467/Document/CreateDocument",
                    SCD,
                    function (response) {

                        let result = JSON.parse(response);

                        if (result.status === '500') {
                            alert('Ошибка сервера: Извините,что-то пошло не так.');

                        }
                        else {
                            alert('Документ успешно добавлен!');
                        }

                    });

                



            });
        }
    );

})();








    /*
                        'name': $('#name').val(),
                        'unitMeasurements': $('#unitMeasurements').val(),
                        'amount': $('#amount').val(),
                        'price': $('#price').val(),
                        'summ': $('#summ').val()
                         */


/*
var postData = { values: stringArray };

    $.ajax({
        type: "POST",
        url: "/Home/SaveList",
        data: postData,
        success: function(data){
            alert(data.Result);
        },
        dataType: "json",
        traditional: true
    });
*/