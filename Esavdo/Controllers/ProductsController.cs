using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using EsavdoDAL.Entities;
using EsavdoDAL.Repository;
using PagedList;

namespace Esavdo.Controllers
{
    public class ProductsController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetLogger("WebSite");

        // GET: Product
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            Console.WriteLine(Request.Url.Authority);
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            ViewBag.SortCategoryParameter = string.IsNullOrEmpty(sortBy) ? "Category desc" : "";

            IEnumerable<Product> products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54773/api/");
                //HTTP GET
                var responseTask = client.GetAsync("product");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Product>>();
                    readTask.Wait();

                    products = readTask.Result.AsQueryable();

                    switch (sortBy)
                    {
                        case "Name desc":
                            products = products.OrderByDescending(x => x.Name);
                            break;
                        case "Category desc":
                            products = products.OrderByDescending(x => x.Category);
                            break;
                        case "Category":
                            products = products.OrderBy(x => x.Category);
                            break;
                        default:
                            products = products.OrderBy(x => x.Name);
                            break;
                    }
                }
                else //web api sent error response 
                {
                    //log response status here..

                    products = Enumerable.Empty<Product>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(products.ToPagedList(page ?? 1, 5));
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54773/api/");
                //HTTP GET
                var responseTask = client.GetAsync("product/"+id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    product = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    product = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, Name,Price,Available,Category")] Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54773/api/product");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Product>("product", product);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    logger.Info("User added new Product" + product.Name + "with ID" + product.Id);
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(product);
        }



        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54773/api/");
                //HTTP GET
                var responseTask = client.GetAsync("product/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    product = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    product = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }


            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Available,Category")] Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54773/api/student");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Product>("product", product);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    logger.Info("User edited Product" + product.Name + "with ID" + product.Id);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54773/api/");
                //HTTP GET
                var responseTask = client.GetAsync("product/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    product = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    product = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }


            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54773/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("product/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    logger.Info("User deleted Product with ID" + id);
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}
