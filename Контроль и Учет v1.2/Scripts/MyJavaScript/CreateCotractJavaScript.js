"use strict";


(function () {

    $(document).ready(
        () => {

            $('#CreateСontract').click(function () {

              
              

                $.post("http://localhost:49467/Contract/CreateCotract", {

                    'СontractNumber': $('#СontractNumber').val(),

                    'Day1': $('#Day1').val(),
                    'Month1': $('#Month1').val(),
                    'Year1': $('#Year1').val(),
                  
                    'Day2': $('#Day2').val(),
                    'Month2': $('#Month2').val(),
                    'Year2': $('#Year2').val(),

                    'Incoterms': $('#Incoterms').val(),


                    'firm': +$('#firm').val(),

                }, function (response) {

                    let result = JSON.parse(response);

                    if (result.status === '500') {
                        alert('Ошибка сервера: Извините,что-то пошло не так. Проверьте пожалуйста введенные данные');

                    }
                    else {
                        alert('Договор успешно добавлен!');
                    }

                });
            });
        }
    );

})();