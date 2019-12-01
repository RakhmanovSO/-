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
    public class FirmController : Controller
    {

        DB_DocumentsEntities dbModel = new DB_DocumentsEntities();

        // GET: Firm/Index
        public ActionResult Index()
        {
 
                return View(dbModel.Firms.ToList());

        }//Index()

        [HttpGet]
        public string AjaxList()
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
                    DirectorSecondName = f.Directors.SecondName
                })
                .ToList();

                //offset++;

                return JsonConvert.SerializeObject(new
                { Firms = firms, });

           

        } // AjaxList()



        // GET: /Firm/Create
        [HttpGet]
        public ActionResult Create()
        {
          
                ViewBag.aType = dbModel.addressTypes;

        
            return View();
        }

        // POST: /Firm/Create
        [HttpPost]
        public string Create(string s1, string s2) {
            try
            {
               

                    byte t1 = byte.Parse(Request["AddressType1"]);

                    Addresses addresses1 = new Addresses()
                    {
                        Country = Request["Country1"],
                        City = Request["City1"],
                        Street = Request["Street1"],
                        HouseNumber = Request["HouseNumber1"],
                        OfficeNumber = int.Parse(Request["OfficeNumber1"]),
                        addressTypes = dbModel.addressTypes.First(t => t.addressTypeId == t1)
                    };

                    byte t2 = byte.Parse(Request["AddressType2"]);

                    Addresses addresses2 = new Addresses()
                    {
                        Country = Request["Country2"],
                        City = Request["City2"],
                        Street = Request["Street2"],
                        HouseNumber = Request["HouseNumber2"],
                        OfficeNumber = int.Parse(Request["OfficeNumber2"]),
                        addressTypes = dbModel.addressTypes.First(t => t.addressTypeId == t2)
                    };

                    Directors director = new Directors()
                    {
                        Surname = Request["Surname"],
                        Name = Request["Name"],
                        SecondName = Request["SecondName"]
                    };



                    var result1 = dbModel.Addresses.Add(addresses1);
                    var result2 = dbModel.Addresses.Add(addresses2);
                    var result3 = dbModel.Directors.Add(director);


                    dbModel.SaveChanges();


                    var Id1 = result1.IdAddress;
                    var Id2 = result2.IdAddress;
                    var Id3 = result3.IdDirector;


                    Firms f = new Firms()
                    {
                        TitleFirm = Request["FirmTitle"],

                        IdAddressActual = Id1,

                        IdAddressLegal = Id2,

                        IdDirector = Id3
                    };

                    dbModel.Firms.Add(f);

                    dbModel.SaveChanges();

                    var r = new { status = 200 };

                    return JsonConvert.SerializeObject(r);

                    //return JsonConvert.SerializeObject("{status: '200'}");

                
            }
            catch (Exception ex)
            {
                var r = new { status = 500 };

                return JsonConvert.SerializeObject(r);

                /*501 Not Implemented («не реализовано»);
                 500 Internal Server Error («внутренняя ошибка сервера») 
                 */
            }

        }// string Create(Firms firm)


        // GET: /Firm/Delete
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }//Delete()



        [HttpPost]
        public string Delete(string s1, string s2) {
            try
            {

       
                    string firmTitle = Request["FirmTitle"];
                    string surnameDirector = Request["Surname"];


                    var resultTable = from f in dbModel.Firms
                                      join d in dbModel.Directors on f.IdDirector equals d.IdDirector
                                      join al in dbModel.Addresses on f.IdAddressLegal equals al.IdAddress
                                      join aa in dbModel.Addresses on f.IdAddressActual equals aa.IdAddress
                                      select new
                                      {
                                          titleFirm = f.TitleFirm,
                                          surname = d.Surname,
                                          idFirm = f.IdFirm,
                                          idAress1 = al.IdAddress,
                                          idAress2 = aa.IdAddress,
                                          idDirect = d.IdDirector

                                      };


                    var result = resultTable.Where(r => r.titleFirm == firmTitle && r.surname == surnameDirector).FirstOrDefault();


                    int Id1 = result.idAress1;
                    int Id2 = result.idAress2;
                    int Id3 = result.idDirect;
                    int Id4 = result.idFirm;



                    dbModel.Addresses.Remove(dbModel.Addresses.FirstOrDefault(p => p.IdAddress == Id1));
                    dbModel.Addresses.Remove(dbModel.Addresses.FirstOrDefault(p => p.IdAddress == Id2));
                    dbModel.Directors.Remove(dbModel.Directors.FirstOrDefault(d => d.IdDirector == Id3));
                    dbModel.Firms.Remove(dbModel.Firms.FirstOrDefault(f => f.IdFirm == Id4));

                    dbModel.SaveChanges();

                    var g = new { status = 200 };

                    return JsonConvert.SerializeObject(g);

                
            }
            catch (Exception ex)
            {
                var r = new { status = 500 };

                return JsonConvert.SerializeObject(r);
                /*501 Not Implemented («не реализовано»);
                 500 Internal Server Error («внутренняя ошибка сервера») 
                 */
            }

        }// Delete




        

   
 

    }///
}///



/*USE DB_Documents

SELECT * FROM Firms 
JOIN Directors ON Directors.IdDirector = Firms.IdDirector

 */
