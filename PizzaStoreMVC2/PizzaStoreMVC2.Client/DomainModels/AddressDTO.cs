using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzaStoreMVC2.Client.DomainModels
{
   public class AddressDTO
   {
      public string Street { get; set; }

      public string City { get; set; }

      public string State { get; set; }

      public string Zip { get; set; }

      public override string ToString()
      {
         return string.Format("{0}_{1}_{2}_{3}", Street, City, State, Zip);
      }
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
      //public AddressDTO()
      //{
      //   this.CustomersDAO = new HashSet<CustomerDTO>();
      //   this.StoresDAO = new HashSet<StoreDTO>();
      //}
      //public int Id { get; set; }

      //public bool Active { get; set; }
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
      public virtual ICollection<CustomerDTO> CustomersDAO { get; set; }
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
      public virtual ICollection<StoreDTO> StoresDAO { get; set; }
   }
}