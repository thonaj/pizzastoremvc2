using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaStoreMVC2.Client.DomainModels;
using PizzaStoreMVC2.Client;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace PizzaStoreMVC2.Client.Controllers
{
    public class PizzaController : Controller
    {
      

      HttpClient client;
      PizzaModel model = new PizzaModel();
      // GET: Pizza
      public ActionResult Index()

      {
         client = new HttpClient();
         client.BaseAddress = new Uri("http://ec2-52-23-205-25.compute-1.amazonaws.com/pizzastoreapi/api/");
         client.MaxResponseContentBufferSize = 256000;
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         var toppingresult = getToppingsAsync().Result;
         var sauceresult = getSaucesAsync();
         var crustresult = getCrustAsync();
         var sizeresult = getSizesAsync();
         var cheeseresult = getCheesesAsync().Result;
         if (sauceresult != null && crustresult!=null && sizeresult!=null && toppingresult!=null && cheeseresult!=null)
         {
            model.SauceOptions = makeSauceList(sauceresult);
            model.CrustOptions = makeCrustList(crustresult);
            model.SizeOptions = makeSizeList(sizeresult);
            model.ToppingOptions = toppingresult;
            model.CheeseOptions = cheeseresult;
            var listpm = new List<PizzaModel>();
            listpm.Add(model);
            return View(listpm);

         }
         else
         {
            return View();
         }
      }



      [HttpPost]

      public string Index(PizzaModel model)

      {
        
         return string.Format("{0} pizza with {1} crust and {2} sauce loaded with {3}  cheese and topped with {4}", model.PizzaOptions.Size, model.PizzaOptions.Crust, model.PizzaOptions.Sauce, ClientHelper.ListPrint(model.PizzaOptions.Cheeses), ClientHelper.ListPrint(model.PizzaOptions.Toppings));

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
      public async Task<List<ToppingDTO>> getToppingsAsync()
      {
         List<ToppingDTO> list = null;
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
         HttpResponseMessage response = client.GetAsync("cheese").Result;
         if (response.IsSuccessStatusCode)
         {
            var data = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<List<CheeseDTO>>(data);
            list = product;
         }
         return list;
      }

   }
}




