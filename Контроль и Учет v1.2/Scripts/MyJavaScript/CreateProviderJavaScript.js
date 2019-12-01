"use strict";


(function () {

    $(document).ready(
        () => {

            $('#CreateFirm').click(function () {


                $.post("http://localhost:49467/Providers/CreateProvider", {

                    'FirmTitle': $('#FirmTitle').val(),


                    'AddressType1': +$('#AddressType1').val(),
                    'Country1': $('#Country1').val(),
                    'City1': $('#City1').val(),
                    'Street1': $('#Street1').val(),
                    'HouseNumber1': $('#HouseNumber1').val(),
                    'OfficeNumber1': $('#OfficeNumber1').val(),


                    'AddressType2': +$('#AddressType2').val(),
                    'Country2': $('#Country2').val(),
                    'City2': $('#City2').val(),
                    'Street2': $('#Street2').val(),
                    'HouseNumber2': $('#HouseNumber2').val(),
                    'OfficeNumber2': $('#OfficeNumber2').val(),


                    'AddressType3': +$('#AddressType3').val(),
                    'Country3': $('#Country3').val(),
                    'City3': $('#City3').val(),
                    'Street3': $('#Street3').val(),
                    'HouseNumber3': $('#HouseNumber3').val(),
                    'OfficeNumber3': $('#OfficeNumber3').val(),


                    'Surname': $('#Surname').val(),
                    'Name': $('#Name').val(),
                    'SecondName': $('#SecondName').val(),

                    'firm': +$('#firm').val()

                }, function (response) {

                    let result = JSON.parse(response);

                    if (result.status === 500) {
                        alert('Ошибка сервера: Извините,что-то пошло не так. Проверьте введенные данные');

                    }
                    else if (result.status === 200) {
                        alert('Фирма успешно добавлена!');
                    }

                });
            });
        }
    );

})();