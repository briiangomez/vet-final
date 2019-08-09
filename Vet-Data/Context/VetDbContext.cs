using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet_Data.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Vet_Data.Context
{
    public class VetDbContext : DbContext
    {
        public VetDbContext() : base("name=Veterinaria")
        {

        }

        public virtual DbSet<Especialidad> Especialidad { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<ItemFactura> ItemFactura { get; set; }
        public virtual DbSet<Sala> Sala { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Medico> Medico { get; set; }
        public virtual DbSet<MedicoEspecialidad> MedicoEspecialidad { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Mascota> Mascota { get; set; }
        public virtual DbSet<Raza> Raza { get; set; }
        public virtual DbSet<Turno> Turno { get; set; }
        public virtual DbSet<HorarioMedico> HorarioMedico { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);

//            modelBuilder.Entity<Mascota>()
//.HasRequired(c => c.Cliente)
//.WithMany()
//.WillCascadeOnDelete(false);

            //            modelBuilder.Entity<Cliente>()
            //        .HasRequired(s => s.Mascota)
            //        .WithMany()
            //        .WillCascadeOnDelete(false);
        }


    }
}
