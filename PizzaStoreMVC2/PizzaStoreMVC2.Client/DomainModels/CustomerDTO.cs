using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaStoreMVC2.Client.DomainModels
{
   public class CustomerDTO
   {
      public int NameId { get; set; }
      public int AddressId { get; set; }
      public int PhoneId { get; set; }
      public int EmailId { get; set; }
      public virtual AddressDTO Address { get; set; }
      public virtual NameDTO Name { get; set; }
      public virtual EmailDTO Email { get; set; }
      public virtual PhoneDTO Phone { get; set; }
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
      public virtual ICollection<OrderDTO> Orders { get; set; }
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
      public CustomerDTO()
      {
         this.Orders = new HashSet<OrderDTO>();
      }
   }
}