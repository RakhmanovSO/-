"use strict";


(function () {

    $(document).ready(
        () => {

            $('#DeleteFirm').click(function () {

                $.post("http://localhost:49467/Providers/DeleteProvider", {

                    'FirmTitle': $('#FirmTitle').val(),

                    'Surname': $('#Surname').val()



                }, function (response) {

                    let result = JSON.parse(response);


                    if (result.status === '500') {
                        alert('Ошибка сервера: Извините,что-то пошло не так.');

                    }
                    else {
                        alert('Фирма успешно удалена!');
                    }




                });
            });
        }
    );

})();