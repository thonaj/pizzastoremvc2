using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace PizzaStoreMVC2.Client.DomainModels
{
   public class PizzaModel 
   {
      
      public int Id { get; set; }


      public List<SelectListItem> SauceOptions { get; set; }
      public List<SelectListItem> CrustOptions { get; set; }
      public List<CheeseDTO>  CheeseOptions { get; set; }
      public List<ToppingDTO> ToppingOptions { get; set; }
      public List<SelectListItem> SizeOptions { get; set; }


      public PizzaOptions PizzaOptions { get; set; }


      public PizzaModel()
      {

         //SauceOptions = ClientHelper.GetSauces();
         //CrustOptions = ClientHelper.GetCrusts();
         //CheeseOptions = ClientHelper.GetCheeses();
         //ToppingOptions = ClientHelper.GetToppings();
         //SizeOptions = ClientHelper.GetSizes();

      }

     

     

   }
}