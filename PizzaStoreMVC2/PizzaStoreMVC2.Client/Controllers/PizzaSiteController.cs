using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Net.Http.Headers;
using PizzaStoreMVC2.Client.DomainModels;
using System.Threading.Tasks;
using PizzaStoreMVC2.Client.ViewModels;
using Newtonsoft.Json;
using System.Reflection;

namespace PizzaStoreMVC2.Client.Controllers
{
   public class PizzaSiteController : Controller
   {
      HttpClient client;
      PizzaSiteModel pizzaSiteModel = new PizzaSiteModel();
      // GET: PizzaSite
      public ActionResult Index()
      {
         
         var storeResult = getStoresAsync();
         if (storeResult != null)
         {
            pizzaSiteModel.StoreOptions = makeStoreList(storeResult);
            return View(pizzaSiteModel);
         }
         else
         {
            return View();
         }
      }

      //Post: PizzaSite
      [System.Web.Mvc.HttpPost]
      public RedirectToRouteResult Index(PizzaSiteModel model)

      {
         pizzaSiteModel.storeString = model.storeString;
         StoreDTO store;
         
         store = getStoresAsync().Result.Where(s => s.LocationId==model.storeString).FirstOrDefault();
         pizzaSiteModel.currentStore = store;
         TempData["model"] = pizzaSiteModel;
         return RedirectToAction("ChooseUser");

      }

      //Get: PizzaSite/ChooseUser
      public ActionResult ChooseUser()
      {
         pizzaSiteModel = TempData["model"] as PizzaSiteModel;
         
         var userResult = getCustomersAsync();
         if (userResult != null)
         {
            pizzaSiteModel.UserOptions = makeUserList(userResult);
            TempData["model"] = pizzaSiteModel;
            return View(pizzaSiteModel);
         }
         else
         {
            return View();
         }
      }

      //Post: PizzaSite/ChooseUser
      [System.Web.Mvc.HttpPost]
      public RedirectToRouteResult ChooseUser(PizzaSiteModel model)

      {
         pizzaSiteModel = TempData["model"] as PizzaSiteModel;
         CustomerDTO customer;
         pizzaSiteModel.userString = model.userString;
         
         customer = getCustomersAsync().Result.Where(s => s.Name.ToString() == model.userString).FirstOrDefault();
         pizzaSiteModel.currentCustomer = customer;
         TempData["model"] = pizzaSiteModel;
         return RedirectToAction("Order");

      }

      //Get: PizzaSite/Order
      public ActionResult Order()
      {

         pizzaSiteModel = TempData["model"] as PizzaSiteModel;
         
         var toppingresult = getToppingsAsync().Result;
         var sauceresult = getSaucesAsync();
         var crustresult = getCrustAsync();
         var sizeresult = getSizesAsync();
         var cheeseresult = getCheesesAsync().Result;
         if (sauceresult != null && crustresult != null && sizeresult != null && toppingresult != null && cheeseresult != null)
         {
            pizzaSiteModel.SauceOptions = makeSauceList(sauceresult);
            pizzaSiteModel.CrustOptions = makeCrustList(crustresult);
            pizzaSiteModel.SizeOptions = makeSizeList(sizeresult);
            pizzaSiteModel.ToppingOptions = toppingresult;
            pizzaSiteModel.CheeseOptions = cheeseresult;
            TempData["model"] = pizzaSiteModel;
            return View(pizzaSiteModel);

         }
         else
         {
            return View();
         }
      }

      //Post: PizzaSite/Order
      [System.Web.Mvc.HttpPost]
      public RedirectToRouteResult Order(PizzaSiteModel model)
      {
         pizzaSiteModel = TempData["model"] as PizzaSiteModel;
         
         var pizza = new PizzaDTO();        
         pizza.Sauce=getSaucesAsync().Result.Where(m => m.Name==model.sauceString).FirstOrDefault();
         pizza.Size=getSizesAsync().Result.Where(m => m.Name==model.sizeString).FirstOrDefault();
         pizza.Crust=getCrustAsync().Result.Where(m => m.Name == model.crustString).FirstOrDefault();
         pizza.cheeses = new List<CheeseDTO>();
         pizza.toppings = new List<ToppingDTO>();         
         for (int x=0;  x< model.CheeseOptions.Count(); x++)
         {
            if(model.CheeseOptions[x].chosen)
            {
               var option = new CheeseDTO();
               option.chosen = true;
               option.Name = pizzaSiteModel.CheeseOptions[x].Name;
               option.Value = pizzaSiteModel.CheeseOptions[x].Value;
               pizza.cheeses.Add(option);
            }            
         }
         for (int x=0;x< model.ToppingOptions.Count();x++)
         {
            if(model.ToppingOptions[x].chosen)
            {
               var option = new ToppingDTO();
               option.chosen = true;
               option.Name = pizzaSiteModel.ToppingOptions[x].Name;
               option.Value = pizzaSiteModel.ToppingOptions[x].Value;              
               pizza.toppings.Add(option);
            }
         }
         pizza.Name= string.Format("{0}_{1}", pizzaSiteModel.currentCustomer.Name.ToString(), new Guid());
         model.currentOrder.currentPizza = pizza;
         

        
         pizzaSiteModel.currentOrder.Pizzas.Add(model.currentOrder.currentPizza);
         pizzaSiteModel.currentOrder.Value = pizzaSiteModel.currentOrder.calculateValue();
         pizzaSiteModel.currentOrder.Customer = pizzaSiteModel.currentCustomer;
         pizzaSiteModel.currentOrder.Store = pizzaSiteModel.currentStore;
         TempData["model"] = pizzaSiteModel;
         
         return RedirectToAction("revieworder");

      }

      //Get: PizzaSite/ReviewOrder
      public ActionResult ReviewOrder()
      {
         pizzaSiteModel = TempData["model"] as PizzaSiteModel;
         ViewBag.model = pizzaSiteModel;
         TempData["model"] = pizzaSiteModel;
         return View(pizzaSiteModel);
      }
      //Post: PizzaSite/ReviewOrder
      [System.Web.Mvc.HttpPost]
      [MultipleButton(Name = "action", Argument = "Add")]
      public RedirectToRouteResult AddPizza(PizzaSiteModel model)
      {
         pizzaSiteModel = TempData["model"] as PizzaSiteModel;
         TempData["model"] = pizzaSiteModel;
         return RedirectToAction("Order");
      }

      [System.Web.Mvc.HttpPost]
      [MultipleButton(Name = "action", Argument = "Buy")]
      public RedirectToRouteResult CompleteOrder(PizzaSiteModel model)
      {
         pizzaSiteModel = TempData["model"] as PizzaSiteModel;
         pizzaSiteModel.currentOrder.Name = string.Format("{0}_{1}_{2}", pizzaSiteModel.currentStore.LocationId, pizzaSiteModel.currentCustomer.Name.ToString(), pizzaSiteModel.currentOrder.Pizzas.Count.ToString());
         foreach (var item in pizzaSiteModel.currentOrder.Pizzas)
         {
            item.Order = new OrderDTO();
            item.Order.Name = pizzaSiteModel.currentOrder.Name;
            item.Order.Store = pizzaSiteModel.currentStore;
            item.Order.Customer = pizzaSiteModel.currentCustomer;
            
            
         }
         TempData["model"] = pizzaSiteModel;
         return RedirectToAction("PurchaseComplete");
      }

      //Get: PizzaSite/PurchaseComplete
      public ActionResult PurchaseComplete()
      {
         pizzaSiteModel = TempData["model"] as PizzaSiteModel;
         var result = insertOrderAsync(pizzaSiteModel.currentOrder).Result;
         foreach (var item in pizzaSiteModel.currentOrder.Pizzas)
         {
            insertPizzaAsync(item);
            foreach (var item2 in item.cheeses)
            {
               var pizzacheese = new PizzaCheeseDTO();
               pizzacheese.Cheese = item2;
               pizzacheese.Pizza = item;
               
               insertPizzaCheeseAsync(pizzacheese);
            }
         }
         pizzaSiteModel.orderhistory = getOrdersAsync().Result;
         TempData["model"] = pizzaSiteModel;
         //return View(pizzaSiteModel);

         return View(pizzaSiteModel);
      }





      //make list functions***************************************************************************************************
      public List<SelectListItem> makeStoreList(Task<List<StoreDTO>> result)
      {
         var list = new List<SelectListItem>();
         foreach (var item in result.Result)
         {
            var itm = new SelectListItem();
            itm.Text = item.LocationId;
            itm.Value = item.LocationId;
            list.Add(itm);
         }
         return list;
      }

      public List<SelectListItem> makeUserList(Task<List<CustomerDTO>> result)
      {
         var list = new List<SelectListItem>();
         foreach (var item in result.Result)
         {
            var itm = new SelectListItem();
            itm.Text = item.Name.First + " " + item.Name;
            itm.Value = item.Name.ToString();
            list.Add(itm);
         }
         return list;
      }

     

      public List<SelectListItem> makeSauceList(Task<List<SauceDTO>> result)
      {
         var list = new List<SelectListItem>();
         foreach (var item in result.Result)
         {
            var itm = new SelectListItem();
            itm.Text = item.Name;
            itm.Value = item.Name;
            list.Add(itm);
         }
         return list;
      }
      public List<SelectListItem> makeCrustList(Task<List<CrustDTO>> result)
      {
         var list = new List<SelectListItem>();
         foreach (var item in result.Result)
         {
            var itm = new SelectListItem();
            itm.Text = item.Name;
            itm.Value = item.Name;
            list.Add(itm);
         }
         return list;
      }
      public List<SelectListItem> makeSizeList(Task<List<SizeDTO>> result)
      {
         var list = new List<SelectListItem>();
         foreach (var item in result.Result)
         {
            var itm = new SelectListItem();
            itm.Text = item.Name;
            itm.Value = item.Name;
            list.Add(itm);
         }
         return list;
      }

      //async get functions*****************************************************************************
      public async Task<List<StoreDTO>> getStoresAsync()
      {
         List<StoreDTO> list = null;
         client = new HttpClient();
         client.BaseAddress = new Uri("http://ec2-52-23-205-25.compute-1.amazonaws.com/pizzastoreapi/api/");
         client.MaxResponseContentBufferSize = 256000;
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         HttpResponseMessage response = client.GetAsync("store").Result;
         if (response.IsSuccessStatusCode)
         {
            var data = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<List<StoreDTO>>(data);
            list = product;
         }
         return list;
      }
      public async Task<List<CustomerDTO>> getCustomersAsync()
      {
         List<CustomerDTO> list = null;
         client = new HttpClient();
         client.BaseAddress = new Uri("http://ec2-52-23-205-25.compute-1.amazonaws.com/pizzastoreapi/api/");
         client.MaxResponseContentBufferSize = 256000;
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         HttpResponseMessage response = client.GetAsync("customer").Result;
         if (response.IsSuccessStatusCode)
         {
            var data = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<List<CustomerDTO>>(data);
            list = product;
         }
         return list;
      }
      public async Task<List<ToppingDTO>> getToppingsAsync()
      {
         List<ToppingDTO> list = null;
         client = new HttpClient();
         client.BaseAddress = new Uri("http://ec2-52-23-205-25.compute-1.amazonaws.com/pizzastoreapi/api/");
         client.MaxResponseContentBufferSize = 256000;
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         HttpResponseMessage response = client.GetAsync("topping").Result;
         if (response.IsSuccessStatusCode)
         {
            var data = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<List<ToppingDTO>>(data);
            list = product;
         }
         return list;
      }
      public async Task<List<CrustDTO>> getCrustAsync()
      {
         List<CrustDTO> list = null;
         client = new HttpClient();
         client.BaseAddress = new Uri("http://ec2-52-23-205-25.compute-1.amazonaws.com/pizzastoreapi/api/");
         client.MaxResponseContentBufferSize = 256000;
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         HttpResponseMessage response = client.GetAsync("crust").Result;
         if (response.IsSuccessStatusCode)
         {
            var data = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<List<CrustDTO>>(data);
            list = product;
         }
         return list;
      }
      public async Task<List<SizeDTO>> getSizesAsync()
      {
         List<SizeDTO> list = null;
         client = new HttpClient();
         client.BaseAddress = new Uri("http://ec2-52-23-205-25.compute-1.amazonaws.com/pizzastoreapi/api/");
         client.MaxResponseContentBufferSize = 256000;
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         HttpResponseMessage response = client.GetAsync("size").Result;
         if (response.IsSuccessStatusCode)
         {
            var data = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<List<SizeDTO>>(data);
            list = product;
         }
         return list;
      }
      public async Task<List<SauceDTO>> getSaucesAsync()
      {
         List<SauceDTO> list = null;
         client = new HttpClient();
         client.BaseAddress = new Uri("http://ec2-52-23-205-25.compute-1.amazonaws.com/pizzastoreapi/api/");
         client.MaxResponseContentBufferSize = 256000;
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         HttpResponseMessage response = client.GetAsync("sauce").Result;
         if (response.IsSuccessStatusCode)
         {
            var data = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<List<SauceDTO>>(data);
            list = product;
         }
         return list;
      }
      public async Task<List<CheeseDTO>> getCheesesAsync()
      {
         List<CheeseDTO> list = null;
         client = new HttpClient();
         client.BaseAddress = new Uri("http://ec2-52-23-205-25.compute-1.amazonaws.com/pizzastoreapi/api/");
         client.MaxResponseContentBufferSize = 256000;
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         HttpResponseMessage response = client.GetAsync("cheese").Result;
         if (response.IsSuccessStatusCode)
         {
            var data = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<List<CheeseDTO>>(data);
            list = product;
         }
         return list;
      }

      public async Task<List<OrderDTO>> getOrdersAsync()
      {
         List<OrderDTO> list = null;
         client = new HttpClient();
         client.BaseAddress = new Uri("http://ec2-52-23-205-25.compute-1.amazonaws.com/pizzastoreapi/api/");
         client.MaxResponseContentBufferSize = 256000;
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         HttpResponseMessage response = client.GetAsync("order").Result;
         if (response.IsSuccessStatusCode)
         {
            var data = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<List<OrderDTO>>(data);
            list = product;
         }
         return list;
      }





      //async insert functions*******************************************************************************
      public async Task<HttpResponseMessage> insertOrderAsync([Bind(Include ="Name, Value, Customer, Store")]OrderDTO order)
      {
         client = new HttpClient();
         client.BaseAddress = new Uri(@"http://ec2-52-23-205-25.compute-1.amazonaws.com/pizzastoreapi/api/");
         client.DefaultRequestHeaders.Accept.Clear();
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         HttpResponseMessage response = await client.PostAsJsonAsync("order", order).ConfigureAwait(continueOnCapturedContext: false);
         //response.EnsureSuccessStatusCode();


         return response;
      }
      public async Task<HttpResponseMessage> insertPhoneAsync([Bind(Include = "Number")]PhoneDTO phone )
      {
         client = new HttpClient();
         client.BaseAddress = new Uri(@"http://ec2-52-23-205-25.compute-1.amazonaws.com/pizzastoreapi/api/");
         client.DefaultRequestHeaders.Accept.Clear();
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         var response = (await client.PostAsJsonAsync("phone", phone ).ConfigureAwait(continueOnCapturedContext: false));
         return response;
      }
      public async Task<HttpResponseMessage> insertCustomerAsync([Bind(Include = "Address, Name, Email, Phone")]CustomerDTO customer)
      {
         client = new HttpClient();
         client.BaseAddress = new Uri(@"http://ec2-52-23-205-25.compute-1.amazonaws.com/pizzastoreapi/api/");
         client.DefaultRequestHeaders.Accept.Clear();
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         var response = (await client.PostAsJsonAsync("customer", customer).ConfigureAwait(continueOnCapturedContext: false));
         return response;
      }

      public async Task<HttpResponseMessage> insertPizzaAsync([Bind(Include = "Name, Crust, Order, Sauce, Size, toppings, cheeses, Value")]PizzaDTO pizza)
      {
         client = new HttpClient();
         client.BaseAddress = new Uri(@"http://ec2-52-23-205-25.compute-1.amazonaws.com/pizzastoreapi/api/");
         client.DefaultRequestHeaders.Accept.Clear();
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         var response = (await client.PostAsJsonAsync("pizza", pizza).ConfigureAwait(continueOnCapturedContext: false));
         return response;
      }

      public async Task<HttpResponseMessage> insertPizzaCheeseAsync([Bind(Include = "Cheese, Pizza")]PizzaCheeseDTO pizzacheese)
      {
         client = new HttpClient();
         client.BaseAddress = new Uri(@"http://ec2-52-23-205-25.compute-1.amazonaws.com/pizzastoreapi/api/");
         client.DefaultRequestHeaders.Accept.Clear();
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         var response = (await client.PostAsJsonAsync("pizzacheese", pizzacheese).ConfigureAwait(continueOnCapturedContext: false));
         return response;
      }



















      //// GET: api/PizzaSite
      //public IEnumerable<string> Get()
      //{
      //    return new string[] { "value1", "value2" };
      //}

      //// GET: api/PizzaSite/5
      //public string Get(int id)
      //{
      //    return "value";
      //}

      //// POST: api/PizzaSite
      //public void Post([FromBody]string value)
      //{
      //}

      //// PUT: api/PizzaSite/5
      //public void Put(int id, [FromBody]string value)
      //{
      //}

      //// DELETE: api/PizzaSite/5
      //public void Delete(int id)
      //{
      //}

   }
   [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
   public class MultipleButtonAttribute : ActionNameSelectorAttribute
   {
      public string Name { get; set; }
      public string Argument { get; set; }

      public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
      {
         var isValidName = false;
         var keyValue = string.Format("{0}:{1}", Name, Argument);
         var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);

         if (value != null)
         {
            controllerContext.Controller.ControllerContext.RouteData.Values[Name] = Argument;
            isValidName = true;
         }

         return isValidName;
      }

      
   }

}
