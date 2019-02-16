using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAPIClient.Models;

namespace WebAPIClient.Controllers
{
    public class HomeController : Controller
    {
        //Hosted web API REST Service base url  
        string Baseurl = "http://localhost/WebAPIDemo/";
        //string Baseurl = "http://localhost:49195/";
         
        public async Task<ActionResult> Index()
        {
            //var cart = ShoppingCart.GetCart(this.HttpContext);

            //// Set up our ViewModel
            //var viewModel = new ShoppingCartViewModel
            //{
            //    poinformation = cart.GetCartItems(),
            //    count = cart.GetTotal()
            //};
            List<POInformation> ItemInfo = new List<POInformation>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllItem using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/POMaster/Get");
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Item list  
                    ItemInfo = JsonConvert.DeserializeObject<List<POInformation>>(ItemResponse);
                }
                //returning the Item list to view  
                return View(ItemInfo);
            }
        }
        public async Task<ActionResult> EditItem(string Id)
        {
            ITEM ItemInfo = new ITEM();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllItem using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Item/Get?ItemNumber=" + Id);
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Item list  
                    ItemInfo = JsonConvert.DeserializeObject<ITEM>(ItemResponse);
                }
                //returning the Item list to view  
                return View(ItemInfo);
            }
        }
        public ActionResult CreateItem()
        {

            //returning the Item list to view  
            return View();

        }
        [HttpPost]
        [ActionName("CreateItem")]
        public async Task<ActionResult> CreateItem(ITEM i)
        {
            try
            {
                ITEM Value = new ITEM();
                if (ModelState.IsValid)
                {
                    TryUpdateModel(Value);
                    using (var client = new HttpClient())
                    {
                        //Passing service base url  
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        //Define request data format  
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //Sending request to find web api REST service resource GetAllItem using HttpClient  
                        var myContent = JsonConvert.SerializeObject(Value);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        ByteArrayContent byteContent = new ByteArrayContent(buffer);
                        HttpResponseMessage Res = await client.PostAsJsonAsync<ITEM>("api/ITEM/POST", Value);
                        //Checking the response is successful or not which is sent using HttpClient  
                        ITEM ItemInfo = new ITEM();
                        if (Res.IsSuccessStatusCode)
                        {
                            //Storing the response details recieved from web api   
                            var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                            //Deserializing the response recieved from web api and storing into the Item list  
                            ItemInfo = JsonConvert.DeserializeObject<ITEM>(ItemResponse);
                            return RedirectToAction("Index");
                        }
                    }
                    //returning the Item list to view  
                    //  return View(ItemInfo);
                }
            }
            catch (Exception e)
            {

                throw;
            }

            return View();
        }
        [HttpPost]
        [ActionName("EditItem")]
        public async Task<ActionResult> EditItem_Post(ITEM i)
        {
            try
            {
                ITEM Value = new ITEM();
                if (ModelState.IsValid)
                {
                    TryUpdateModel(Value);
                    using (var client = new HttpClient())
                    {
                        //Passing service base url  
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        //Define request data format  
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //Sending request to find web api REST service resource GetAllItem using HttpClient  
                        var myContent = JsonConvert.SerializeObject(Value);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        ByteArrayContent byteContent = new ByteArrayContent(buffer);
                        HttpResponseMessage Res = await client.PutAsJsonAsync<ITEM>("api/Item/Put", Value);
                        //Checking the response is successful or not which is sent using HttpClient  
                        ITEM ItemInfo = new ITEM();
                        if (Res.IsSuccessStatusCode)
                        {
                            //Storing the response details recieved from web api   
                            var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                            //Deserializing the response recieved from web api and storing into the Item list  
                            ItemInfo = JsonConvert.DeserializeObject<ITEM>(ItemResponse);
                            return RedirectToAction("Index");
                        }
                    }
                    //returning the Item list to view  
                    //  return View(ItemInfo);
                }
            }
            catch (Exception e)
            {

                throw;
            }

            return View();
        }
        public async Task<ActionResult> DetailsItem(string Id)
        {
            ITEM ItemInfo = new ITEM();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllItem using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Item/Get?ItemNumber=" + Id);
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Item list  
                    ItemInfo = JsonConvert.DeserializeObject<ITEM>(ItemResponse);
                }
                //returning the Item list to view  
                return View(ItemInfo);
            }
        }

        public async Task<ActionResult> DeleteItem(string Id)
        {
            ITEM ItemInfo = new ITEM();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllItem using HttpClient  
                HttpResponseMessage Res = await client.DeleteAsync("api/Item/Delete?ItemCode=" + Id);
                //returning the Item list to view  
                return RedirectToAction("Index");
            }

        }
        public async Task<ActionResult> ViewItems()
        {
            List<ITEM> ItemInfo = new List<ITEM>();
            //returning the Item list to view  
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllItem using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/ITEM/Get");
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Item list  
                    ItemInfo = JsonConvert.DeserializeObject<List<ITEM>>(ItemResponse);
                }
                //returning the Item list to view  
                return View(ItemInfo);
            }
        }
        public ActionResult CreateSupplier()
        {
            //returning the Item list to view  
            return View();
        }
        public async Task<ActionResult> ViewSupplier()
        {
            List<SUPPLIER> ItemInfo = new List<SUPPLIER>();
            //returning the Item list to view  
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllItem using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Supplier/Get");
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Item list  
                    ItemInfo = JsonConvert.DeserializeObject<List<SUPPLIER>>(ItemResponse);
                }
                //returning the Item list to view  
                return View(ItemInfo);
            }
        }

        [HttpPost]
        [ActionName("CreateSupplier")]
        public async Task<ActionResult> CreateSupplier_Post(SUPPLIER s)
        {
            SUPPLIER Value = new SUPPLIER();
            if (ModelState.IsValid)
            {
                TryUpdateModel(Value);
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //Sending request to find web api REST service resource GetAllItem using HttpClient  
                    var myContent = JsonConvert.SerializeObject(Value);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    ByteArrayContent byteContent = new ByteArrayContent(buffer);
                    HttpResponseMessage Res = await client.PostAsJsonAsync<SUPPLIER>("api/Supplier/POST", Value);
                    //Checking the response is successful or not which is sent using HttpClient  
                    SUPPLIER SupplierInfo = new SUPPLIER();
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Item list  
                        // SupplierInfo = JsonConvert.DeserializeObject<SUPPLIER>(ItemResponse);
                        return RedirectToAction("ViewSupplier");
                    }
                }
            }
            //returning the Item list to view  
            return View();
        }
        public async Task<ActionResult> EditSupplier(string id)
        {
            SUPPLIER ItemInfo = new SUPPLIER();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllItem using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Supplier/Get?SUPPLIERNumber=" + id);
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Item list  
                    ItemInfo = JsonConvert.DeserializeObject<SUPPLIER>(ItemResponse);
                }
                //returning the Item list to view  
                return View(ItemInfo);
            }

        }
        [HttpPost]
        [ActionName("EditSupplier")]
        public async Task<ActionResult> EditSupplier_Post(SUPPLIER s)
        {
            SUPPLIER Value = new SUPPLIER();
            if (ModelState.IsValid)
            {
                TryUpdateModel(Value);
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //Sending request to find web api REST service resource GetAllItem using HttpClient  
                    var myContent = JsonConvert.SerializeObject(Value);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    ByteArrayContent byteContent = new ByteArrayContent(buffer);
                    HttpResponseMessage Res = await client.PutAsJsonAsync<SUPPLIER>("api/Supplier/Put", Value);
                    //Checking the response is successful or not which is sent using HttpClient  
                    SUPPLIER SupplierInfo = new SUPPLIER();
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Item list  
                        SupplierInfo = JsonConvert.DeserializeObject<SUPPLIER>(ItemResponse);
                        return RedirectToAction("ViewSupplier");
                    }
                }
            }
            return View(s);
        }
        public async Task<ActionResult> DetailsSupplier(string id)
        {
            SUPPLIER ItemInfo = new SUPPLIER();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllItem using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Supplier/Get?SUPPLIERNumber=" + id);
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Item list  
                    ItemInfo = JsonConvert.DeserializeObject<SUPPLIER>(ItemResponse);
                }
                //returning the Item list to view  
                return View(ItemInfo);
            }
        }
        public async Task<ActionResult> DeleteSupplier(string Id)
        {
            ITEM ItemInfo = new ITEM();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllItem using HttpClient  
                HttpResponseMessage Res = await client.DeleteAsync("api/Supplier/Delete?SUPPLIERCode=" + Id);
                //returning the Item list to view  
                return RedirectToAction("ViewSupplier");
            }

        }
        public async Task<ActionResult> CreatePOMaster()
        {
            List<ITEM> ItemInfo = new List<ITEM>();
            List<SUPPLIER> supplierInfo = new List<SUPPLIER>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllItem using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/ITEM/Get");
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Item list  
                    ItemInfo = JsonConvert.DeserializeObject<List<ITEM>>(ItemResponse);

                }
                Res = await client.GetAsync("api/SUPPLIER/Get");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Item list  
                    supplierInfo = JsonConvert.DeserializeObject<List<SUPPLIER>>(ItemResponse);
                }
                //Information info = new Information();
                //info.items = ItemInfo;
                //info.suppliers = supplierInfo;
                //returning the Item list to view  
                return View(new POInformation());
            }
        }
        public ActionResult ViewPOMaster()
        {
            //returning the Item list to view  
            return View();
        }
        [HttpPost]
        [ActionName("CreatePOMaster")]
        public async Task<ActionResult> CreatePOMaster_Post(POInformation pi)
        {
            POInformation Value = new POInformation();
            if (ModelState.IsValid)
            {
                TryUpdateModel(Value);
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //Sending request to find web api REST service resource GetAllItem using HttpClient  
                    var myContent = JsonConvert.SerializeObject(Value);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    ByteArrayContent byteContent = new ByteArrayContent(buffer);
                    List<POInformation> poinfo = new List<POInformation>();
                    poinfo.Add(Value);
                    HttpResponseMessage Res = await client.PostAsJsonAsync<List<POInformation>>("api/POMaster/POST", poinfo);
                    //Checking the response is successful or not which is sent using HttpClient  
                    POInformation POMasterInfo = new POInformation();
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Item list  
                        // POMasterInfo = JsonConvert.DeserializeObject<POInformation>(ItemResponse);
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(pi);
        }
        public async Task<ActionResult> EditPOMaster(string id)
        {
            List<POInformation> ItemInfo = new List<POInformation>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllItem using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/POMaster/Get?PoNumber=" + id);
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Item list  
                    ItemInfo = JsonConvert.DeserializeObject<List<POInformation>>(ItemResponse);

                }
                //returning the Item list to view  
                return View(ItemInfo.FirstOrDefault());
            }
        }
        [HttpPost]
        [ActionName("EditPOMaster")]
        public async Task<ActionResult> EditPOMaster_Post(POInformation p)
        {
            POInformation Value = new POInformation();
            if (ModelState.IsValid)
            {
                TryUpdateModel(Value);
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //Sending request to find web api REST service resource GetAllItem using HttpClient  
                    var myContent = JsonConvert.SerializeObject(Value);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    ByteArrayContent byteContent = new ByteArrayContent(buffer);
                    List<POInformation> plist = new List<POInformation>();
                    plist.Add(Value);
                    HttpResponseMessage Res = await client.PutAsJsonAsync<List<POInformation>>("api/POMaster/Put", plist);
                    //Checking the response is successful or not which is sent using HttpClient  
                    POInformation SupplierInfo = new POInformation();
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Item list  
                        //  SupplierInfo = JsonConvert.DeserializeObject<POInformation>(ItemResponse);
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(p);
        }
        public async Task<ActionResult> DetailsPOMaster(string id)
        {
            List<POInformation> ItemInfo = new List<POInformation>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllItem using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/POMaster/Get?PoNumber=" + id);
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ItemResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Item list  
                    ItemInfo = JsonConvert.DeserializeObject<List<POInformation>>(ItemResponse);
                }
                //returning the Item list to view  
                return View(ItemInfo.FirstOrDefault());
            }
        }
        public async Task<ActionResult> DeletePOMaster(string Id)
        {
            POInformation ItemInfo = new POInformation();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllItem using HttpClient  
                HttpResponseMessage Res = await client.DeleteAsync("api/POMaster/Delete?POMASTERCode=" + Id);
                //returning the Item list to view  
                return RedirectToAction("Index");
            }

        }
    }
}