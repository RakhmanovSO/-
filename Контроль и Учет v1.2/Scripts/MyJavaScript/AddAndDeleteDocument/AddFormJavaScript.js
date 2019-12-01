"use strict";

function GetFormTemlate() {

    return `
      <p>  </p>
      <form>
            <div class="form-row">
                <div class="col">
                    <div class="form-group">
                        <label class="sr-only" for="Наименование">Наименование товара/услуги</label>
                        <input id="name" class="form-control" type="text" placeholder="Наименование товара/услуги" />
                    </div>
                </div>

                <div class="col">
                    <div class="form-group">
                        <label class="sr-only" for="Ед. измерения">Ед. измерения</label>
                        <input id="unitMeasurements" class="form-control" type="text" placeholder="Ед. измерения" />
                    </div>
                </div>

                <div class="col">
                    <div class="form-group">
                        <label class="sr-only" for="Кол-во">Кол-во</label>
                        <input id="amount" class="form-control" type="text" placeholder="Кол-во" />
                    </div>
                </div>

                <div class="col">
                    <div class="form-group">
                        <label class="sr-only" for="Цена без НДС">Цена без НДС</label>
                        <input id="price" class="form-control" type="text" placeholder="Цена без НДС" />
                    </div>

                </div>

                <div class="col">
                    <div class="form-group">
                        <label class="sr-only" for="Сумма без НДС">Сумма без НДС</label>
                        <input id="summ" class="form-control" type="text" placeholder="Сумма без НДС" />
                    </div>

                </div>

            </div>
        </form>
    `;

}//GetFormTemlate

$(document).ready(() => {

    $('#AddForm').click(function () {
        // $(this)
        $('.container2').first().append(GetFormTemlate());
    });

});
