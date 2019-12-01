"use strict";


(function () {

    $(document).ready(
        () => {

            $('#DeleteDocument').click(function () {


                $.post("http://localhost:49467/Document/DeleteDocument", {


                    'contract': +$('#contract').val(),

                    'numberDocument': $('#numberDocument').val(),

                    'Day1': $('#Day1').val(),
                    'Month1': $('#Month1').val(),
                    'Year1': $('#Year1').val(),

                   

                }, function (response) {

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