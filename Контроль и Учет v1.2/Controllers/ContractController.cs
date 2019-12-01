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
    public class ContractController : Controller
    {
        // GET: Contract
        public ActionResult Index()
        {
            return View();
        }

       

        // GET: Contract/Create
        public ActionResult CreateCotract()
        {

            DB_DocumentsEntities dbModel = new DB_DocumentsEntities();
            ViewBag.provider = dbModel.Providers;


            return View();
        }//CreateCotract

        // POST: Contract/Create
        [HttpPost]
        public string CreateCotract(string s1)
        {
            try
            {
                using (DB_DocumentsEntities dbModel = new DB_DocumentsEntities())
                {

                    string dateFromResult;
                    string dateEndResult;

                    string Year1 = Request["Year1"];
                    string Month1 = Request["Month1"];
                    string Day1 = Request["Day1"];

                    string Year2 = Request["Year2"];
                    string Month2 = Request["Month2"];
                    string Day2 = Request["Day2"];

                    int IdProvider = int.Parse (Request["firm"]);


                    dateFromResult = Year1 + "-" + Month1 + "-" + Day1;
                    dateEndResult = Year2 + "-" + Month2 + "-" + Day2;

                    Contracts contract = new Contracts()
                    {
                       ContractNumber = Request["СontractNumber"],
                       DateFrom = DateTime.Parse(dateFromResult),
                       DateEnd = DateTime.Parse(dateEndResult),
                       DeliveryConditionsIncoterms = Request["Incoterms"]
                       
                    };

                    int providerID = int.Parse(Request["firm"]);

                    Providers pr = dbModel.Providers.FirstOrDefault(p => p.IdProvider == providerID);


                    contract.Providers.Add(pr);


                    dbModel.Contracts.Add(contract);

                    dbModel.SaveChanges();


                    return JsonConvert.SerializeObject("{status: '200'}");

                }//using

            }
            catch (Exception ex)
            {
                return $"{{status: '500', message: '{ex.Message}'}}";

                /*501 Not Implemented («не реализовано»);
                 500 Internal Server Error («внутренняя ошибка сервера») 
                 */
            }

        }//CreateCotract



        // GET: Contract/Delete/5
        public ActionResult DeleteCotract()
        {
            return View();
        }//DeleteCotract

        

        // POST: Contract/Delete/5
        [HttpPost]
        public string DeleteCotract(string s1)
        {
            try
            {
                using (DB_DocumentsEntities dbModel = new DB_DocumentsEntities())
                {


                    string СontractNumber = Request["СontractNumber"];
                    string ProviderTitle = Request["ProviderTitle"];


                    var resultTable = dbModel.Contracts.Where(x => x.Providers.Any(pr => pr.TitleProvider == ProviderTitle) && x.ContractNumber == СontractNumber).FirstOrDefault();


                    int Id1 = resultTable.IdContract;

                    dbModel.Contracts.Remove(dbModel.Contracts.FirstOrDefault(con => con.IdContract == Id1)); 

                    dbModel.SaveChanges();



                    return JsonConvert.SerializeObject("{status: '200'}");
                }//using
            }
            catch (Exception ex)
            {
                return $"{{status: '500', message: '{ex.Message}'}}";
                /*501 Not Implemented («не реализовано»);
                 500 Internal Server Error («внутренняя ошибка сервера») 
                 */
            }

        }// DeleteCotract
        
    


        // GET: Contract/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: Contract/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }



        // POST: Contract/Edit/5
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
