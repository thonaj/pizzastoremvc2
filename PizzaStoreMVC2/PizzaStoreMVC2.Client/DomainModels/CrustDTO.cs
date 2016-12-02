using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaStoreMVC2.Client.DomainModels
{
   public class CrustDTO
   {
      public string Name { get; set; }

      public decimal Value { get; set; }
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
      //public CrustDTO()
      //{
      //   this.Pizzas = new HashSet<PizzaDTO>();
      //}
      //public int Id { get; set; }

      //public bool Active { get; set; }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
      public virtual ICollection<PizzaDTO> Pizzas { get; set; }
   }
}