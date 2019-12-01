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
    public class Specification
    {

        public string titleFirm { get; set; }
        public string firmDirectorSurname { get; set; }
        public string firmDirectorName { get; set; }
        public string firmDirectorSecondName { get; set; }


        public string titleProvider { get; set; }
        public string providerDirectorSurname { get; set; }
        public string providerDirectorName { get; set; }
        public string providerDirectorSecondName { get; set; }


        public string numberContract { get; set; }

        public DateTime dateFromContract { get; set; } // дата заключения договора

        public string dateOfCreationSpecification { get; set; } // дата создания спецификации

        public double NDS { get; set; }  // НДС

        public string[] ArrayTitlesSpecification { get; set; }

 
        public IEnumerable<DocumentTitle> DocumentTitle { get; set; }

        public Specification(List<DocumentTitle> items)
        {

            ArrayTitlesSpecification = new string[items.Count()];

            for (int i = 0; i < ArrayTitlesSpecification.Length; i++)
            {

                DocunentItem item = new DocunentItem()
                {
                    Title = items[i].Title,
                    unitMeasurements = items[i].UnitMeasurements,
                    Amount = items[i].Amount,
                    Price = items[i].PriceWithoutNDS,
                    Sum = items[i].Amount * items[i].PriceWithoutNDS


                };

                ArrayTitlesSpecification[i] = JsonConvert.SerializeObject(item);
            }

        }

    }//Specification

    public class SpecificationModel
    {
        public IEnumerable<Specification> Specification { get; set; }
    }


    public class DocunentItem {
  
        public string Title { get; set; }

        public double Amount { get; set; }

        public double Price { get; set; }

        public string unitMeasurements { get; set; }

        public double Sum { get; set; }

    }

    public class SpecificationController : Controller
    {

        DB_DocumentsEntities dbModel = new DB_DocumentsEntities();

        // GET: Specification
        public ActionResult Index()
        {
            return View();
        }


        // GET: Specification/Create
        public ActionResult Create()
        {

            int SelectedIndex = 1;

            SelectList firm = new SelectList(dbModel.Firms, "IdFirm", "TitleFirm", SelectedIndex);
            ViewBag.Firm = firm;


            SelectList provider = new SelectList(dbModel.Providers.Where(p => p.IdProvider == SelectedIndex), "IdProvider", "TitleProvider");
            ViewBag.Provider = provider;


            SelectList contract = new SelectList(dbModel.Contracts.Where(c => c.IdContract == SelectedIndex), "IdContract", "ContractNumber");
            ViewBag.Contract = contract;

            return View();
        }


        public ActionResult GetItemsProvider(int id)
        {

            DB_DocumentsEntities dbModel = new DB_DocumentsEntities();


            var pr2 = dbModel.Firms.FirstOrDefault(p => p.IdFirm == id).Providers.ToList();


            return PartialView(pr2);

        }//GetItemsProvider


        public ActionResult GetItemsContract(int id)
        {
            DB_DocumentsEntities dbModel = new DB_DocumentsEntities();

            var cont = dbModel.Providers.FirstOrDefault(c => c.IdProvider == id).Contracts.ToList();

            return PartialView(cont);

        }//GetItemsContract



        public ActionResult NewSpecification()
        {
            try
            {
                int IdFirm = int.Parse(Request["firm"]);
                int IdProvider = int.Parse(Request["provider"]);
                int IdContract = int.Parse(Request["contract"]);

                DateTime dateFromResult; // От
                DateTime dateEndResult;  // До

                string dateSpecificationResult = DateTime.Today.ToString(); // дата создания спецификации


                string Year1 = Request["Year1"];
                string Month1 = Request["Month1"];
                string Day1 = Request["Day1"];

                string Year2 = Request["Year2"];
                string Month2 = Request["Month2"];
                string Day2 = Request["Day2"];



                dateFromResult = DateTime.Parse(Year1 + "-" + Month1 + "-" + Day1);

                dateEndResult = DateTime.Parse(Year2 + "-" + Month2 + "-" + Day2);



                var contr = dbModel.Contracts.FirstOrDefault(c => c.IdContract == IdContract);


                // var carDocuments = contr.Documents.Where(doc => doc.Date > dateFromResult && doc.Date < dateEndResult);


                var carDocuments = contr.Documents.ToList();

                List<Documents> list = new List<Documents>();

                foreach (var doc in carDocuments) {

                    if (doc.Date > dateFromResult && doc.Date < dateEndResult) {

                        list.Add(doc);
                    }

                }

                // var docItems = carDocuments.Join(dbModel.DocumentTitle, doc => doc.IdDocument, it => it.IdTitle, (doc, it) => new DocunentItem { Title = it.Title, Price = it.PriceWithoutNDS, Amount = it.Amount, unitMeasurements = it.UnitMeasurements }).ToList();

                
                var docItems = new List<DocumentTitle>();

                foreach (var doc in list) {

                    foreach (var item in doc.DocumentTitle){

                        docItems.Add(item);
                    }

                }//foreach
 
                
               var resultItems = new List<DocumentTitle>();

               foreach (var item in docItems){

                    var item1 = resultItems.FirstOrDefault(i=> i.Title == item.Title && i.PriceWithoutNDS == item.PriceWithoutNDS);

                   if ( item1 != null ) {

                        item1.Amount += item.Amount;

                   }// if
                   else {

                        resultItems.Add(item);

                   }// else

                  

            }//foreach
            

               // var items = docItems.Distinct().ToList();



                var firm = dbModel.Firms.FirstOrDefault(f => f.IdFirm == IdFirm);

                var provider = dbModel.Providers.FirstOrDefault(p => p.IdProvider == IdProvider);


                

                 Specification specification1 = new Specification(resultItems) {

                    titleFirm = firm.TitleFirm,
                    firmDirectorSurname = firm.Directors.Surname,
                    firmDirectorName = firm.Directors.Name,
                    firmDirectorSecondName = firm.Directors.SecondName,

                    titleProvider = provider.TitleProvider,
                    providerDirectorSurname = provider.Directors.Surname,
                    providerDirectorName = provider.Directors.Name,
                    providerDirectorSecondName = provider.Directors.SecondName,

                    numberContract = contr.ContractNumber,

                    dateFromContract = contr.DateFrom,

                    dateOfCreationSpecification = dateSpecificationResult

                };

               
                return View("NewSpecification", specification1);

                

               


            }
            catch (Exception ex)
            {
                return View("Error", ex);

                /*501 Not Implemented («не реализовано»);
                 500 Internal Server Error («внутренняя ошибка сервера») 
                 */
            }
        }





        // GET: Specification/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Specification/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }






    }
}
