"use strict";

(function () {

    $(document).ready(
        () => {

            $('#CreateSpecification').click(function () {

                
                
                $.post("http://localhost:49467/Specification/Create",
                    {

                        'firm': +$('#firm').val(),
                        'provider': +$('#provider').val(),
                        'contract': +$('#contract').val(),

                        'Day1': $('#Day1').val(),
                        'Month1': $('#Month1').val(),
                        'Year1': $('#Year1').val(),

                        'Day2': $('#Day2').val(),
                        'Month2': $('#Month2').val(),
                        'Year2': $('#Year2').val(),

                    }, function (response) {

                        var result = JSON.parse(response);

                        if (result.status === '500') {
                            alert('Ошибка сервера: Извините,что-то пошло не так.');

                        }
                        else {

                            //document.location = 'http://localhost:49467/Specification/NewSpecification';

                           
                            var output = '<div id="myDiv">';

                            for (let i = 0; i < 1; i++) {

                                

                                output += '<table id="myTable2" align="left">'

                                        output += '<tr class="row1"> <td class="myDT1">Приложение №</td><td class="myDT1">________________</td> </tr>';
                                        output += '<tr class="row1"> <td class="myDT1">к договору №</td><td class="myDT1">' + result.numberContract + '</td> </tr>';
                                        output += '<tr class="row1"> <td class="myDT1">от №</td><td class="myDT1">' + result.dateFromContract + '</td> </tr>';

                                        output += '<tr class="row1"> <td class="myDT1"> </td><td class="myDT1"></td> </tr>';
                                        output += '<tr class="row1"> <td class="myDT1"> </td><td class="myDT1"></td> </tr>';

                                        output += '<tr class="row1"> <td class="myDT1"></td><td class="myDT1">' + result.dateOfCreationSpecification + '</td> </tr>';


                                        output += '</table>';


                                        output += '<p> </p>'
                                        output += '<p> </p>'

                                    output += '<p> <th> СПЕЦИФИКАЦИЯ </p>';

                                    output += '<p> на поставку продукции </p>';

                                    output += '<p> </p>'
                                    output += '<p> </p>'

                                    output += '<table id="myTable3" align="center">'

                                    output += '<tr class="row2"> <th>№ п/п</th> <th>Наименование</th> <th>Ед. изм.</th> <th>Кол-во</th> <th>Цена</th> <th>Сумма</th></tr>';

                                    output += '<tr class="row2"> <th>1</th> <th>'+result.ArrayTitlesSpecification[0]+'</th> <th>Ед. изм.</th> <th>Кол-во</th> <th>Цена</th> <th>Сумма</th></tr>';
                                    output += '<tr class="row2"> <th>2</th> <th>' + result.ArrayTitlesSpecification[1] + '</th> <th>Ед. изм.</th> <th>Кол-во</th> <th>Цена</th> <th>Сумма</th></tr>';
                                    output += '<tr class="row2"> <th>3</th> <th>' + result.ArrayTitlesSpecification[2] + '</th> <th>Ед. изм.</th> <th>Кол-во</th> <th>Цена</th> <th>Сумма</th></tr>';
                                    output += '<tr class="row2"> <th>4</th> <th>' + result.ArrayTitlesSpecification[3] + '</th> <th>Ед. изм.</th> <th>Кол-во</th> <th>Цена</th> <th>Сумма</th></tr>';
                                    output += '<tr class="row2"> <th>5</th> <th>' + result.ArrayTitlesSpecification[4] + '</th> <th>Ед. изм.</th> <th>Кол-во</th> <th>Цена</th> <th>Сумма</th></tr>';
                                    output += '<tr class="row2"> <th>6</th> <th>' + result.ArrayTitlesSpecification[5] + '</th> <th>Ед. изм.</th> <th>Кол-во</th> <th>Цена</th> <th>Сумма</th></tr>';

                                    output += '<tr class="row3"> <th></th> <th>ИТОГО:</th> <th></th> <th></th> <th></th> <th>0,00</th></tr>';
                                   
                                    output += '</table>';


                                    output += '<p> </p>'
                                    output += '<p> </p>'

                                    output += '<p> 1. Условия Поставки -  </p>'
                                    output += '<p> 2. Срок поставки -     </p>'
                                    output += '<p> 2. Срок оплаты -       </p>'

                                    output += '<p> </p>'
                                    output += '<p> </p>'


                                    output += '<table id="myTable4" align="center">'

                                    output += '<tr class="row4"> <th class="myTD2"> ПОСТАВЩИК </th> <th class="myTD2">ПОКУПАТЕЛЬ</th> </tr>';
                                    output += '<tr class="row4"> <td class="myTD2"> </td> <td class="myTD2"></td> </tr>';
                                    output += '<tr class="row4"> <td class="myTD2">' + result.titleProvider + '</td> <td class="myTD2">' + result.titleFirm +'</td> </tr>';
                                    output += '<tr class="row4"> <td class="myTD2"> </td> <td class="myTD2"></td> </tr>';
                                    output += '<tr class="row4"> <td class="myTD2">Директор: ___________ ' + result.providerDirectorName[0] + '. ' + result.providerDirectorSecondName[0] + '. ' + result.providerDirectorSurname + '</td> <td class="myTD2">Директор: ___________  ' + result.firmDirectorName[0] + '. ' + result.firmDirectorSecondName[0] + '. ' + result.firmDirectorSurname+'</td> </tr>';

                                    output += '</table>';

                                }// for

                            output += '</<div>';

                             output += '<p> </p>'
                             output += '<p> </p>'



                             $('#divMyTable').html(output);



                        }// else

                       
                    });// function (response)
                            
            }); // #CreateSpecification


        }); // $(document).ready(

})();// function ()





/*

 $(document).ready(function () {
                                $("#block").append(GetTable());

                                alert('Новая страница!');
                            });

function GetTable() {

                                return `
                                                                    <table id="myTable1">
                                                                        <tr class="row1">
                                                                        <th>Приложение №</th>   <th><ins> </ins></th>
                                                                        </tr>
                                                                        <tr class="row2">
                                                                        <th>к договору №</th>   <th><ins> </ins></th>
                                                                        </tr>

                                                                        <tr class="row3">
                                                                        <th>от</th>   <th><ins> </ins></th>
                                                                        </tr>

                                                                        <tr class="row4">
                                                                        <th> </th>   <th> </th>
                                                                        </tr>

                                                                        <tr class="row5">
                                                                        <th> </th>   <th>  11.07.2018г. </th>
                                                                        </tr>

                                                                    </table>

                                              <table id="myTable2">

                                                <tr>
                                               <th>№ п/п</th>     <th>Наименование</th>   <th>Ед. изм.</th>   <th>Кол-во</th>  <th>Цена</th>  <th>Сумма</th>

                                                </tr>

                                                <tr class="row6">

                                               <td> 1 </td>  <td> Масло </td>   <td> л </td>   <td> 200 </td>   <td> 100 </td>     <td> 20000 </td>
                                                </tr>

                                                <tr class="row7">

                                                 <td> </td>   <td> Итого: </td>     <td>  </td>   <td> </td>    <td>  </td>       <td> 20000 </td>
                                                </tr>

                                            </table>  `;
                            }//GetTable*/