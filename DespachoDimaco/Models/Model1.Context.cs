﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dimacodevEntities : DbContext
    {
        public dimacodevEntities()
            : base("name=dimacodevEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<colaborador> colaborador { get; set; }
        public virtual DbSet<colaboradorHojaRuta> colaboradorHojaRuta { get; set; }
        public virtual DbSet<costosHojaRuta> costosHojaRuta { get; set; }
        public virtual DbSet<guias> guias { get; set; }
        public virtual DbSet<hojaRuta> hojaRuta { get; set; }
        public virtual DbSet<hojaRutaDetalle> hojaRutaDetalle { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }
        public virtual DbSet<vehiculo> vehiculo { get; set; }
    }
}
