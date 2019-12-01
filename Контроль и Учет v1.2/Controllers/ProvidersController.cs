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
    public class ProvidersController : Controller
    {

        DB_DocumentsEntities dbModel = new DB_DocumentsEntities();

        // GET: Providers
        public ActionResult Index()
        {
                return View(dbModel.Providers.ToList());
        }



        // GET: Providers/Create
        [HttpGet]
        public ActionResult CreateProvider()
        {
           

                ViewBag.aType = dbModel.addressTypes;

                ViewBag.firms = dbModel.Firms;

                return View();
           
        }//CreateProvider

        // POST: Providers/Create
        [HttpPost]
        public string CreateProvider(string s1, string s2)
        {
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


                    byte t3 = byte.Parse(Request["AddressType3"]);

                    Addresses addresses3 = new Addresses()
                    {
                        Country = Request["Country3"],
                        City = Request["City3"],
                        Street = Request["Street3"],
                        HouseNumber = Request["HouseNumber3"],
                        OfficeNumber = int.Parse(Request["OfficeNumber3"]),
                        addressTypes = dbModel.addressTypes.First(t => t.addressTypeId == t3)
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
                    var result4 = dbModel.Addresses.Add(addresses3);


                    dbModel.SaveChanges();


                    var Id1 = result1.IdAddress;
                    var Id2 = result2.IdAddress;     
                    var Id3 = result3.IdDirector;
                    var Id4 = result4.IdAddress;

                    Providers pr = new Providers()
                    {
                        TitleProvider = Request["FirmTitle"],

                        IdAddressProviderActual = Id1,

                       IdAddressProviderLegal = Id2,

                        IdDirector = Id3,

                        IdAddressStore = Id4


                    };


                    int firmID = int.Parse(Request["firm"]);

                    Firms firm = dbModel.Firms.FirstOrDefault(f => f.IdFirm == firmID);

                    pr.Firms.Add(firm);

                    dbModel.Providers.Add(pr);
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
        }//CreateProvider


        // GET: Providers/Delete/5
        public ActionResult DeleteProvider()
        {
           

                return View();
         
        }

        // POST: Providers/Delete/5
        [HttpPost]
        public string DeleteProvider (string s1, string s2)
        {
            try
            {
              

                    string firmTitle = Request["FirmTitle"];
                    string surnameDirector = Request["Surname"];


                    var resultTable = from p in dbModel.Providers
                                      join d in dbModel.Directors on p.IdDirector equals d.IdDirector
                                      join al in dbModel.Addresses on p.IdAddressProviderLegal equals al.IdAddress
                                      join aa in dbModel.Addresses on p.IdAddressProviderActual equals aa.IdAddress
                                      join astor in dbModel.Addresses on p.IdAddressStore equals astor.IdAddress
                                      select new
                                      {
                                          titleFirm = p.TitleProvider,
                                          surname = d.Surname,
                                          idAress1 = al.IdAddress,
                                          idAress2 = aa.IdAddress,
                                          idAress3 = astor.IdAddress,
                                          idDirect = d.IdDirector,
                                          idProvider = p.IdProvider,

                                          idFirm = p.Firms

                                      };


                    var result = resultTable.Where(p => p.titleFirm == firmTitle && p.surname == surnameDirector).FirstOrDefault();


                    int Id1 = result.idAress1;
                    int Id2 = result.idAress2;
                    int Id3 = result.idAress3;
                    int Id4 = result.idDirect;
                    int Id5 = result.idProvider;

                    var firm = result.idFirm.FirstOrDefault();

                    if (firm != null)
                    {
                        dbModel.Firms.Remove(firm);
                    }

                    dbModel.Addresses.Remove(dbModel.Addresses.FirstOrDefault(p => p.IdAddress == Id1));
                    dbModel.Addresses.Remove(dbModel.Addresses.FirstOrDefault(p => p.IdAddress == Id2));
                    dbModel.Addresses.Remove(dbModel.Addresses.FirstOrDefault(p => p.IdAddress == Id3));
                    dbModel.Directors.Remove(dbModel.Directors.FirstOrDefault(d => d.IdDirector == Id4));
                    dbModel.Providers.Remove(dbModel.Providers.FirstOrDefault(p => p.IdProvider == Id5));



                    dbModel.SaveChanges();

                    var r = new { status = 200 };

                    return JsonConvert.SerializeObject(r);


                    //return JsonConvert.SerializeObject("{status: '200'}");
                
            }
            catch (Exception ex)
            {
                var r = new { status = 500 };

                return JsonConvert.SerializeObject(r);

                /*
                return $"{{status: '500', message: '{ex.Message} {ex.StackTrace}'}}";
                /*501 Not Implemented («не реализовано»);
                 500 Internal Server Error («внутренняя ошибка сервера») 
                 */
            }

        }// Delete

     
    }
}
