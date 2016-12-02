using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaStoreMVC2.Client.DomainModels
{
   public class PhoneDTO
   {
      public string Number { get; set; }
       //public int Id { get; set; }
         //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
         //public PhoneDTO()
         //{
         //   this.Customers = new HashSet<CustomerDTO>();
         //   this.Stores = new HashSet<StoreDTO>();
         //}
         //public bool Active { get; set; }
         [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
         public virtual ICollection<CustomerDTO> Customers { get; set; }
         [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
         public virtual ICollection<StoreDTO> Stores { get; set; }





   }
  
}