using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaStoreMVC2.Client.DomainModels
{
   public class ToppingDTO
   {
      public string Name { get; set; }

      public decimal Value { get; set; }
      public bool chosen { get; set; }
      //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
      //public ToppingDTO()
      //{
      //   this.PizzaToppings = new HashSet<PizzaToppingDTO>();
      //}
      //public int Id { get; set; }

      //public bool Active { get; set; }
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
      public virtual ICollection<PizzaToppingDTO> PizzaToppings { get; set; }
   }
}