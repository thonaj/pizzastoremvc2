using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaStoreMVC2.Client.DomainModels
{
   public class SizeDTO
   {
      public string Name { get; set; }

      public decimal Value { get; set; }
      //public int Id { get; set; }

      //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
      //public SizeDTO()
      //{
      //   this.Pizzas = new HashSet<PizzaDTO>();
      //}
      //public bool Active { get; set; }
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
      public virtual ICollection<PizzaDTO> Pizzas { get; set; }
   }
}