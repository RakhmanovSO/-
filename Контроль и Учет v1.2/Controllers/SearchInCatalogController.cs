
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System.Data.Entity; //
using Контроль_и_Учет_v1._2.DB_ModelContext; //
using Newtonsoft.Json; //



namespace Контроль_и_Учет_v1._2.Controllers
{
    public class SearchInCatalogController : Controller
    {
        DB_DocumentsEntities dbModel = new DB_DocumentsEntities();

        // GET: SearchInCatalog
        public ActionResult Index()
        {
            return View();
        }

        // GET: 
        public ActionResult SearchFirm ()
        {
                return View();
         
        }//  SearchFirm


        [HttpGet]
        public string AjaxList()
        {
            using (DB_DocumentsEntities dbModel = new DB_DocumentsEntities())
            {

                int offset = int.Parse(Request["offset"] != null ? Request["offset"] : "0");
                int limit = int.Parse(Request["limit"] != null ? Request["limit"] : "2");

                var firms = dbModel.Firms
                .OrderBy(f => f.IdFirm)
                .Skip(offset)
                .Take(limit)
                .Select((f) => new
                {
                    FirmTitle = f.TitleFirm,
                    DirectorSurname = f.Directors.Surname,
                    DirectorName = f.Directors.Name,
                    DirectorSecondName = f.Directors.SecondName,
                    Id = f.IdFirm

                })
                .ToList();

                //offset++;

                return JsonConvert.SerializeObject(new
                { Firms = firms, });

            }

        } // AjaxList()


        public class FirmResult {
 
           public string[] arrayMoreInformation { get; set; }

           public FirmResult() { }

        }


        [HttpPost]
        public string SearchFirm(string s1) {
            try {

                string titleFirm = Request["titleFirm"];



               // var firmTitleResult = dbModel.Firms.FirstOrDefault(f => f.TitleFirm == titleFirm);

             
                var firmResult = dbModel.Firms
                .Where(f => f.TitleFirm == titleFirm).Select((f) => new
                {
                    FirmTitle = f.TitleFirm,
                    DirectorSurname = f.Directors.Surname,
                    DirectorName = f.Directors.Name,
                    DirectorSecondName = f.Directors.SecondName,
                    addresLegalCountry = f.Addresses.Country,
                    addresLegalCity = f.Addresses.City,
                    addresLegalStreet = f.Addresses.Street,
                    addresLegalHouseNumber = f.Addresses.HouseNumber,
                    addresLegalOfficeNumber = f. Addresses.OfficeNumber,
                    addresActualCountry = f.Addresses1.Country,
                    addresActualCity = f.Addresses1.City,
                    addresActualStreet = f.Addresses1.Street,
                    addresActualHouseNumber = f.Addresses1.HouseNumber,
                    addresActualOfficeNumber = f.Addresses1.OfficeNumber,
                })
                .ToList();


                if (firmResult.Count == 0){
                    var e = new { status = 500 };

                    return JsonConvert.SerializeObject(e);

                }// if


                return JsonConvert.SerializeObject(new
                { Firms = firmResult });

            }
            catch (Exception ex){

                var e = new { status = 500 };

                return JsonConvert.SerializeObject(e);

                /*501 Not Implemented («не реализовано»);
                 500 Internal Server Error («внутренняя ошибка сервера») 
                 */
            }
        }// SearchFirm


        public ActionResult SearchСontract() {
            return View();
        }


        [HttpPost]
        public string SearchСontract (string s1) {
            try {

                string contractNumber = Request["contractNumber"];

                var contractResult = dbModel.Contracts
                .Where(c => c.ContractNumber == contractNumber).Select((c) => new
                {
                    contractNumber = c.ContractNumber,
                    dateFrom = c.DateFrom,
                    dateEnd  = c.DateEnd,
                    incoterms = c.DeliveryConditionsIncoterms,
                }).ToList();


                if (contractResult.Count == 0) {

                    var e = new { status = 500 };

                    return JsonConvert.SerializeObject(e);

                }

                return JsonConvert.SerializeObject(new
                { Contracts = contractResult });

            }//try
            catch (Exception ex)
            {
                var e = new { status = 500 };

                return JsonConvert.SerializeObject(e);

                /*501 Not Implemented («не реализовано»);
                 500 Internal Server Error («внутренняя ошибка сервера») 
                 */
            }

        }// SearchСontract


        public ActionResult SearchDocument() {

            return View();
        }// SearchDocument



        [HttpPost]
        public string SearchDocument (string s1)
        {
            try
            {

                string documentsNumber = Request["documentsNumber"];

                var documentResult = dbModel.Documents
                .Where(d => d.DocumentNumber == documentsNumber).Select((d) => new
                {
                    documentNumber = d.DocumentNumber,
                    date = d.Date
                   
                }).ToList();

                var Result = dbModel.Documents.FirstOrDefault((d => d.DocumentNumber == documentsNumber));


                var items = Result.DocumentTitle.Select((s)=> new {

                    t = s.Title,
                    ed = s.UnitMeasurements,
                    amount = s.Amount,
                    price = s.PriceWithoutNDS

                }).ToList();


                  /* 
                   https://metanit.com/sharp/mvc5/10.6.php 
                   http://qaru.site/questions/530111/how-to-return-multiple-variables-with-jsonresult-aspnet-mvc3 
                   */


                  var r = new { Documents = documentResult, DocumentTitle = items };
               
                return JsonConvert.SerializeObject(r);

            }//try
            catch (Exception ex)
            {

                var e = new { status = 500 };

                return JsonConvert.SerializeObject(e);

                /*501 Not Implemented («не реализовано»);
                 500 Internal Server Error («внутренняя ошибка сервера») 
                 */
            }

        }// SearchDocument (string s1)




        // GET: 
        public ActionResult Details(int id)
        {
            using (DB_DocumentsEntities dbModel = new DB_DocumentsEntities())
            {
                return View(dbModel.Firms.Where(f => f.IdFirm == id).FirstOrDefault());
            }

        }//Detail


      

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try{
                using (DB_DocumentsEntities dbModel = new DB_DocumentsEntities()){

                    var resultTable = from f in dbModel.Firms
                                      join d in dbModel.Directors on f.IdDirector equals d.IdDirector
                                      join al in dbModel.Addresses on f.IdAddressLegal equals al.IdAddress
                                      join aa in dbModel.Addresses on f.IdAddressActual equals aa.IdAddress
                                      select new
                                      {
                                          idFirm = f.IdFirm,

                                          titleFirm = f.TitleFirm,

                                          Country1 = al.Country,
                                          City1 = al.City,
                                          Street1 = al.Street,
                                          HouseNumber1 = al.HouseNumber,
                                          OfficeNumber1 = al.OfficeNumber,

                                          Country2 = aa.Country,
                                          City2 = aa.City,
                                          Street2 = aa.Street,
                                          HouseNumber2 = aa.HouseNumber,
                                          OfficeNumber2 = aa.OfficeNumber,

                                          surname = d.Surname,
                                          name = d.Name,
                                          secondNamee = d.SecondName
                                      };


                    return View(resultTable.Where(r => r.idFirm == id).FirstOrDefault());

                }
            }
            catch 
            {
                return View("status: 500");
            }

        }// Edit


        [HttpPost]
        public ActionResult Edit(int id, Firms firm){
            try {
                using (DB_DocumentsEntities dbModel = new DB_DocumentsEntities())
                {

                    dbModel.Entry(firm).State = EntityState.Modified;
                    dbModel.SaveChanges();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }// Edit




    }
}
