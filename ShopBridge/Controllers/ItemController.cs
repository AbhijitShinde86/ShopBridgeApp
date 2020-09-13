using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ShopBridge;
using ShopBridge.Helper;
using ShopBridge.Models;
using ShopBridge.Repository;

namespace ShopBridge.Controllers
{
    public class ItemController : Controller
    {
        public ActionResult Index()
        {

            List<Item> itemList = new List<Item>();
            using (ApiModel api = new ApiModel())
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = new HttpResponseMessage();
                client = api.ApiClient();
                response = client.GetAsync("api/ItemAPI/GetItems").Result;
                using (HttpContent content = response.Content)
                {
                    itemList = JsonConvert.DeserializeObject<ItemResponse>(response.Content.ReadAsStringAsync().Result).Data;
                }
            }

            return View(itemList ?? new List<Item>());
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "ItemID")] Item item)
        {
            if (ModelState.IsValid)
            {
                ItemResponse itemResponse = new ItemResponse();
                using (ApiModel api = new ApiModel())
                {
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = new HttpResponseMessage();
                    client = api.ApiClient();
                    response = client.PostAsJsonAsync("api/ItemAPI/AddItem", item).Result;
                    using (HttpContent content = response.Content)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                            return RedirectToAction("Index");
                        else
                        {
                            itemResponse = JsonConvert.DeserializeObject<ItemResponse>(response.Content.ReadAsStringAsync().Result);
                            ModelState.AddModelError("Create", itemResponse.ErrorMsg);
                            return View(item);
                        }

                    }
                }
            }

            return View(item);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                        
            if (!GetSelectedEmployeeData(id,out Item item))
                return HttpNotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,ItemName,Description,Price")] Item item)
        {
            if (ModelState.IsValid)
            {
                ItemResponse itemResponse = new ItemResponse();
                using (ApiModel api = new ApiModel())
                {
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = new HttpResponseMessage();
                    client = api.ApiClient();
                    response = client.PutAsJsonAsync("api/ItemAPI/EditItem", item).Result;
                    using (HttpContent content = response.Content)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                            return RedirectToAction("Index");
                        else
                        {
                            itemResponse = JsonConvert.DeserializeObject<ItemResponse>(response.Content.ReadAsStringAsync().Result);
                            ModelState.AddModelError("Edit", itemResponse.ErrorMsg);
                            return View(item);
                        }

                    }
                }
            }
            return View(item);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!GetSelectedEmployeeData(id, out Item item))
                return HttpNotFound();

            return View(item);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "ItemID")] Item item)
        {
            ItemResponse itemResponse = new ItemResponse();
            using (ApiModel api = new ApiModel())
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = new HttpResponseMessage();
                client = api.ApiClient();
                response = client.DeleteAsync("api/ItemAPI/DeleteItem?ItemID="+item.ItemID).Result;
                using (HttpContent content = response.Content)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                        return RedirectToAction("Index");
                    else
                    {
                        itemResponse = JsonConvert.DeserializeObject<ItemResponse>(response.Content.ReadAsStringAsync().Result);
                        ModelState.AddModelError("Delete", itemResponse.ErrorMsg);
                        return View(item);
                    }

                }
            }
        
        }

        private static bool GetSelectedEmployeeData(int? id, out Item item )
        {
           
            List<Item> empList = new List<Item>();
            using (ApiModel api = new ApiModel())
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = new HttpResponseMessage();
                client = api.ApiClient();
                response = client.GetAsync("api/ItemAPI/GetItem?ItemID=" + id).Result;
                using (HttpContent content = response.Content)
                {
                    empList = JsonConvert.DeserializeObject<ItemResponse>(response.Content.ReadAsStringAsync().Result).Data;
                }
            }

            item = new Item();

            if (empList.Count > 0)
                item = empList[0];

            return empList.Count > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
