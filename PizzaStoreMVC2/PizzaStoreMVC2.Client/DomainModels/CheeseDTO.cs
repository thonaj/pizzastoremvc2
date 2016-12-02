using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzaStoreMVC2.Client.DomainModels
{
   public class CheeseDTO
   {
      public string Name { get; set; }

      public decimal Value { get; set; }
      public bool chosen { get; set; }
      //public CheeseDTO()
      //{
      //   this.PizzaCheeseDAO = new HashSet<PizzaCheeseDTO>();
      //}
      //public int Id { get; set; }

      //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
      //public virtual ICollection<PizzaCheeseDTO> PizzaCheeseDAO { get; set; }
   }
}