using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PizzaStoreMVC2.Client.DomainModels
{
   public class PizzaOptions
   {
      [Key]
      //public int Id { get; set; }
      public string Sauce { get; set; }
      public string Crust { get; set; }
      public List<string> Cheeses { get; set; }
      public List<string> Toppings { get; set; }
      public string Size { get; set; }

      public PizzaOptions()
      {
         Cheeses = new List<string>();
         Toppings = new List<string>();
      }
   }
}