﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class RestaurantDBEntities : DbContext
    {
        public RestaurantDBEntities()
            : base("name=RestaurantDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<Dishes_contents> Dishes_contents { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Items_in_deliveries> Items_in_deliveries { get; set; }
        public virtual DbSet<Ordering_dishes> Ordering_dishes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment_methods> Payment_methods { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<PRIVILEGE> PRIVILEGES { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<Type> Types { get; set; }
    
        public virtual ObjectResult<Employee_of_the_month_Result> Employee_of_the_month(Nullable<System.DateTime> monthYear)
        {
            var monthYearParameter = monthYear.HasValue ?
                new ObjectParameter("MonthYear", monthYear) :
                new ObjectParameter("MonthYear", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Employee_of_the_month_Result>("Employee_of_the_month", monthYearParameter);
        }
    }
}
