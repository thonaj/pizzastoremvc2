using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaStoreMVC2.Client.DomainModels
{
   public class PizzaCheeseDTO
   {
      public int CheeseId { get; set; }
      public int PizzaId { get; set; }
      public virtual CheeseDTO Cheese { get; set; }

      public override string ToString()
      {
         return string.Format("{0}_{1}", PizzaId, CheeseId);
      }
      //public int Id { get; set; }


      public virtual PizzaDTO Pizza { get; set; }
   }
}