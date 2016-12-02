using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaStoreMVC2.Client.DomainModels
{
   public class OrderDTO
   {
      //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
      public OrderDTO()
      {
         this.Pizzas = new HashSet<PizzaDTO>();
         //this.Name = this.ToString();
      }

      public string Name { get; set; }
      public decimal Value { get; set; }
      public int StoreId { get; set; }
      public int CustomerId { get; set; }
      public PizzaDTO currentPizza { get; set; }

      public virtual CustomerDTO Customer { get; set; }
      public virtual StoreDTO Store { get; set; }
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
      public virtual ICollection<PizzaDTO> Pizzas { get; set; }

      public override string ToString()
      {
         return string.Format("{0}_{1}_{2}", Name.ToString(), Value.ToString(), Pizzas.Count.ToString());
      }
      public Decimal calculateValue()
      {
         Value = 0.00M;
         if(Pizzas!=null)
         {
            foreach (var item in Pizzas)
            {
               Value += item.calculateValue();
            }
         }
         return Value;
      }
   }
}