using PizzaStoreMVC2.Client.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaStoreMVC2.Client.ViewModels
{
   public class PizzaSiteModel
   {
      public PizzaSiteModel()
      {
         this.currentOrder = new OrderDTO();
      }
      public List<SelectListItem> SauceOptions { get; set; }
      public List<SelectListItem> CrustOptions { get; set; }
      public List<CheeseDTO> CheeseOptions { get; set; }
      public List<ToppingDTO> ToppingOptions { get; set; }
      public List<SelectListItem> SizeOptions { get; set; }
      public List<SelectListItem> StoreOptions { get; set; }
      public List<SelectListItem> UserOptions { get; set; }
      public string storeString { get; set; }
      public string userString { get; set; }
      public string crustString { get; set; }
      public string sizeString { get; set; }
      public string sauceString { get; set; }
      public List<OrderDTO> orderhistory { get; set; }

      public OrderDTO currentOrder { get; set; }
      public CustomerDTO currentCustomer { get; set; }
      public StoreDTO currentStore { get; set; }


   }
}