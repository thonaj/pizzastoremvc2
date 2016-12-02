using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaStoreMVC2.Client.DomainModels
{
   public class StoreDTO
   {
      public string LocationId { get; set; }
      public virtual AddressDTO Address { get; set; }
      public virtual ICollection<OrderDTO> Orders { get; set; }
      public virtual PhoneDTO Phone { get; set; }
      public int AddressId { get; set; }
      public int PhoneId { get; set; }
      //public StoreDTO()
      //{
      //   this.Orders = new HashSet<OrderDTO>();
      //}
      //public int Id { get; set; }
   }
}