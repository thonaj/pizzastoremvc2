using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaStoreMVC2.Client.DomainModels;

namespace PizzaStoreMVC2.Client
{
   public class ClientHelper
   {
      public static List<SelectListItem> GetSauces()
      {
         var sauces = new List<SelectListItem>()
         {
              new SelectListItem() { Text = "Alfredo", Value = "Alfredo" },

              new SelectListItem() { Text = "Tomato", Value = "Tomato", Selected = true },

              new SelectListItem() { Text = "Marinara", Value = "Marinara" }
         };
         return sauces;
      }

      internal static List<SelectListItem> GetCrusts()
      {
         var Crusts = new List<SelectListItem>()
         {
              new SelectListItem() { Text = "Hand Tossed", Value = "Hand Tossed" },

              new SelectListItem() { Text = "Pan Style", Value = "Pan Style", Selected = true },

              new SelectListItem() { Text = "Thin and Crispy", Value = "Thin and Crispy" }
         };
         return Crusts;
      }

      internal static List<SelectListItem> GetCheeses()
      {
         var cheeses = new List<SelectListItem>()
         {
              new SelectListItem() { Text = "Italian Blend", Value = "Italian Blend" },

              new SelectListItem() { Text = "Mozzerella", Value = "Mozzerella", Selected = true },

              new SelectListItem() { Text = "Pepper Jack", Value = "Pepper Jack" }
         };
         return cheeses;
      }

      internal static List<SelectListItem> GetSizes()
      {
         var sizes = new List<SelectListItem>()
         {
              new SelectListItem() { Text = "Medium", Value = "Medium" },

              new SelectListItem() { Text = "Large", Value = "Large", Selected = true },

              new SelectListItem() { Text = "Small", Value = "Small" }
         };
         return sizes;
      }

      internal static List<SelectListItem> GetToppings()
      {
         var toppings = new List<SelectListItem>()
         {
              new SelectListItem() { Text = "Mushroom", Value = "Mushroom" },

              new SelectListItem() { Text = "Pepperoni", Value = "Pepperoni", Selected = true },

              new SelectListItem() { Text = "Diced Tomato", Value = "Diced Tomato" }
         };
         return toppings;
      }
      internal static string ListPrint(List<string> list)
      {
         string toReturn = "";
         foreach (var item in list)
         {
            if(toReturn.Equals(""))
            {
               toReturn += item;
            }
            else
            {
               toReturn += "and" + item;
            }
           
            
         }
         return toReturn;
      }
   }
}