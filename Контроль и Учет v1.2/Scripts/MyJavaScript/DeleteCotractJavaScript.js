"use strict";


(function () {

    $(document).ready(
        () => {

            $('#DeleteСontract').click(function () {

                $.post("http://localhost:49467/Contract/DeleteCotract", {

                    'СontractNumber': $('#СontractNumber').val(),

                    'ProviderTitle': $('#ProviderTitle').val()



                }, function (response) {

                    let result = JSON.parse(response);


                    if (result.status === '500') {
                        alert('Ошибка сервера: Извините,что-то пошло не так.');

                    }
                    else {
                        alert('Договор успешно удален!');
                    }




                });
            });
        }
    );

})();