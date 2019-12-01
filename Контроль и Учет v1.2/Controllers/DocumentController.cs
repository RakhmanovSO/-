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
    public class DocumentController : Controller
    {

        DB_DocumentsEntities dbModel = new DB_DocumentsEntities();


        // GET: Document
        public ActionResult Index()
        {
            return View();
        }

        // GET: Document/Create
        public ActionResult CreateDocument() {


            int SelectedIndex = 1;


            SelectList firm = new SelectList(dbModel.Firms, "IdFirm", "TitleFirm", SelectedIndex);
            ViewBag.Firm = firm;


            SelectList provider = new SelectList(dbModel.Providers.Where(p => p.IdProvider == SelectedIndex), "IdProvider", "TitleProvider");
            ViewBag.Provider = provider;


            SelectList contract = new SelectList(dbModel.Contracts.Where(c => c.IdContract == SelectedIndex), "IdContract", "ContractNumber");
            ViewBag.Contract = contract;

            return View();

        } // Create()


        public ActionResult GetItemsProvider(int id) {

          
           
            var pr2 = dbModel.Firms.FirstOrDefault( p=> p.IdFirm==id).Providers.ToList();


            return PartialView(pr2);

        }//GetItemsProvider


        public ActionResult GetItemsContract(int id)
        {

            var cont = dbModel.Providers.FirstOrDefault(c =>c.IdProvider == id).Contracts.ToList();

            return PartialView(cont);

        }//GetItemsContract


        public class SecondClassDocument
        {

            public int contractID { get; set; }
            public string numberDocument { get; set; }

            public string Year { get; set; }

            public string Month { get; set; }

            public string Day { get; set; }

            public double NDS { get; set; }

            public string[] ArrayDocTitle { get; set; }

        }//class SecondClassDocument

        public class SecondClassDocumentModel
        {
            public IEnumerable<SecondClassDocument> SecondDocument { get; set; }
        }


        

        // POST: Document/Create
        [HttpPost]
        public JsonResult CreateDocument(SecondClassDocument SCD)
        {
            try
            {
               
                    
                    var Doc = SCD; 

        
                    string dateDocument;
                    dateDocument = Doc.Year + "-" + Doc.Month + "-" + Doc.Day;


                    Documents document = new Documents()
                    {
                        DocumentNumber = Doc.numberDocument,
                        Date = DateTime.Parse(dateDocument),
                        NDS = Doc.NDS

                    };

                    Contracts contract = dbModel.Contracts.FirstOrDefault(c => c.IdContract == Doc.contractID);

                    document.Contracts.Add(contract);

                    var DocumentNew = dbModel.Documents.Add(document);

                    dbModel.SaveChanges();

                    var DocumentNewID = DocumentNew.IdDocument;  /// Id нового документа для таблицы наименований


                    // 5 полей ввода  Doc.ArrayDocTitle.Count() 10/5=2;

                    int CountArray = Doc.ArrayDocTitle.Count();

                    int max = (CountArray / 5);

                    int t = 0;

                    for (int i = 1; i < max ; i++) {

                        DocumentTitle docTitle = new DocumentTitle() {

                            Title = Doc.ArrayDocTitle[t],
                             UnitMeasurements = Doc.ArrayDocTitle[t+1],
                             Amount = double.Parse (Doc.ArrayDocTitle[t+2]),
                             PriceWithoutNDS = double.Parse(Doc.ArrayDocTitle[t+3]),
                             SumWithoutNDS = double.Parse(Doc.ArrayDocTitle[t+4])

                         };

                        Documents doc = dbModel.Documents.FirstOrDefault(d => d.IdDocument == DocumentNewID);
                        docTitle.Documents.Add(doc);

                        dbModel.DocumentTitle.Add(docTitle);

                        t = t+5;

                   }// for


                   dbModel.SaveChanges();


                    return Json("{status: '200'}");


            }
            catch (Exception ex)
            {
                return Json($"{{status: '500', message: '{ex.Message} {ex.StackTrace}'}}");

                /*501 Not Implemented («не реализовано»);
                 500 Internal Server Error («внутренняя ошибка сервера») 
                 */
            }
        }//CreateDocumen


        // GET: Document/Delete/5
        public ActionResult DeleteDocument()
        {
           

            int SelectedIndex = 1;

            SelectList firm = new SelectList(dbModel.Firms, "IdFirm", "TitleFirm", SelectedIndex);

            ViewBag.Firm = firm;


            SelectList provider = new SelectList(dbModel.Providers.Where(p => p.IdProvider == SelectedIndex));

            ViewBag.Provider = provider;

            SelectList contract = new SelectList(dbModel.Contracts.Where(c => c.IdContract == SelectedIndex));

            ViewBag.Contract = contract;


            return View();
        }

        // POST: Document/Delete/5
        [HttpPost]
        public string DeleteDocument(string s2)
        {
            try
            {
               

                    int contractID = int.Parse(Request["contract"]);

                    string numberDocument = Request["numberDocument"];


                    string Year1 = Request["Year1"];
                    string Month1 = Request["Month1"];
                    string Day1 = Request["Day1"];

                    string dateDocument = Year1 + "-" + Month1 + "-" + Day1;
                   
                    var dateDoc = DateTime.Parse(dateDocument);

                   ///// ?????

                    var resultTable = dbModel.Documents.Where(x => x.Contracts.Any(con => con.IdContract == contractID) && x.DocumentNumber == numberDocument && x.Date == dateDoc).FirstOrDefault();

                    int Id1 = resultTable.IdDocument;

                    dbModel.Documents.Remove(dbModel.Documents.FirstOrDefault(doc => doc.IdDocument == Id1));

                    dbModel.SaveChanges();


                    ///// ?????

                    var resultTable2 = from doc in dbModel.Documents
                                       join docTitle in dbModel.DocumentTitle on doc.IdDocument equals docTitle.IdTitle
                                       join con in dbModel.Contracts on doc.IdDocument equals con.IdContract
                                       select new
                                       {
                                           DocumentID = doc.IdDocument,
                                           DocNumber = doc.DocumentNumber,
                                           DocDate = doc.Date,

                                           DocTitleID = docTitle.IdTitle,

                                           ContractID = con.IdContract
                                       };

                   //  var result = resultTable2.Where(x => x. && x.).FirstOrDefault();


                    return JsonConvert.SerializeObject("{status: '200'}");


               
            }
            catch (Exception ex)
            {
                return $"{{status: '500', message: '{ex.Message}'}}";
                /*501 Not Implemented («не реализовано»);
                 500 Internal Server Error («внутренняя ошибка сервера») 
                 */
            }
        }


      
    }
}
