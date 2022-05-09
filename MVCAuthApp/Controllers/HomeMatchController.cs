using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVCAuthApp.Controllers
{
    public class HomeMatchController : Controller
    {
        // GET: HomeMatch
        public ActionResult Index()
        {
            string api_url = "http://api.football-api.com/2.0/matches?match_date=8.12.2018&Authorization=565ec012251f932ea4000001fa542ae9d994470e73fdb314a8a56d76";
            string result = RequestApiService(api_url);

            Debug.WriteLine(result);



            
            return View();
        }

        public static string RequestApiService(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse err = ex.Response;
                using (Stream stream = err.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    string errorText = reader.ReadToEnd();

                }
                throw;


            }
            
        }

        // GET: HomeMatch/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeMatch/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeMatch/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeMatch/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeMatch/Edit/5
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

        // GET: HomeMatch/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeMatch/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
