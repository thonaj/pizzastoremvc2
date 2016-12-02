using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaStoreMVC2.Client.DomainModels
{
   public class PizzaToppingDTO
   {
      public int ToppingId { get; set; }
      public int PizzaId { get; set; }
      public virtual ToppingDTO Topping { get; set; }

      public override string ToString()
      {
         return string.Format("{0}_{1}", PizzaId, ToppingId);
      }
      //public int Id { get; set; }

      public virtual PizzaDTO Pizza { get; set; }
   }
}