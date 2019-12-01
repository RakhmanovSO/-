"use strict";


(function () {

    $(document).ready(
        () => {

            $('#DeleteFirm').click(function () {

                $.post("http://localhost:49467/Firm/Delete", {

                    'FirmTitle': $('#FirmTitle').val(),

                    'Surname': $('#Surname').val()



                }, function (response) {

                    let result = JSON.parse(response);


                    if (result.status === 500) {
                        alert('Ошибка сервера: Извините,что-то пошло не так. Проверьте введенные данные');

                    }
                    else if (result.status === 200) {
                        alert('Фирма успешно удалена!');
                    }



                });
            });
        }
    );

})();