using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaStoreMVC2.Client.DomainModels
{
   public class NameDTO
   {
      public string First { get; set; }

      public string Last { get; set; }

      public override string ToString()
      {
         return string.Format("{0}_{1}", First, Last);
      }
      //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
      //public NameDTO()
      //{
      //   this.Customers = new HashSet<CustomerDTO>();
      //}
      //public int Id { get; set; }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
      public virtual ICollection<CustomerDTO> Customers { get; set; }
   }
}