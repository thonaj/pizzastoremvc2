using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaStoreMVC2.Client.DomainModels
{
   public class PizzaDTO
   {
      public PizzaDTO()
      {
         this.Crust = new CrustDTO();
         this.Sauce = new SauceDTO();
         this.Size = new SizeDTO();
         this.toppings = new List<ToppingDTO>();
         this.cheeses = new List<CheeseDTO>();
         //this.Name = this.ToString();
      }
      public string Name { get; set; }
      public int CrustId { get; set; }
      public int SauceId { get; set; }
      public int SizeId { get; set; }
      public Nullable<int> OrderId { get; set; }
      public virtual CrustDTO Crust { get; set; }
      public virtual OrderDTO Order { get; set; }
      public virtual SauceDTO Sauce { get; set; }
      public virtual SizeDTO Size { get; set; }
      public List<ToppingDTO> toppings { get; set; }
      public List<CheeseDTO> cheeses { get; set; }
      public decimal Value { get; set; }
      public decimal calculateValue()
      {
         Value = 0.00M;
         Value += Crust.Value;
         Value += Sauce.Value;
         Value += Size.Value;
         if(toppings!=null)
         {
            foreach (var item in toppings)
            {
               Value += item.Value;
            }
         }
         if(cheeses!=null)
         {
            foreach (var item in cheeses)
            {
               Value += item.Value;
            }
         }
         return Value;
      }
      //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
      //public virtual ICollection<PizzaCheeseDTO> PizzaCheese { get; set; }
      //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
      //public virtual ICollection<PizzaToppingDTO> PizzaToppings { get; set; }
   }
}