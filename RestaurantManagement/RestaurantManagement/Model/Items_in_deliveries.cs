//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestaurantManagement.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Items_in_deliveries
    {
        public decimal Quantity { get; set; }
        public System.DateTime Delivery_Date { get; set; }
        public string Supplier_Name { get; set; }
        public string Ingredient_Name { get; set; }
    
        public virtual Delivery Delivery { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}