namespace Vet_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Direccion = c.String(),
                        Localidad = c.String(),
                        NroDocumento = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Factura",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Numero = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        TurnoId = c.Int(nullable: false),
                        Importe = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LetraFactura = c.Int(nullable: false),
                        FormaPago = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .ForeignKey("dbo.Turno", t => t.TurnoId)
                .Index(t => t.ClienteId)
                .Index(t => t.TurnoId);
            
            CreateTable(
                "dbo.ItemFactura",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FacturaId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Factura", t => t.FacturaId)
                .ForeignKey("dbo.Item", t => t.ItemId)
                .Index(t => t.FacturaId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Turno",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MedicoId = c.Int(nullable: false),
                        EspecialidadId = c.Int(nullable: false),
                        MascotaId = c.Int(nullable: false),
                        SalaId = c.Int(nullable: false),
                        FechaInicio = c.DateTime(precision: 7, storeType: "datetime2"),
                        FechaFin = c.DateTime(precision: 7, storeType: "datetime2"),
                        EstadoId = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Especialidad", t => t.EspecialidadId)
                .ForeignKey("dbo.Mascota", t => t.MascotaId)
                .ForeignKey("dbo.Medico", t => t.MedicoId)
                .ForeignKey("dbo.Sala", t => t.SalaId)
                .Index(t => t.MedicoId)
                .Index(t => t.EspecialidadId)
                .Index(t => t.MascotaId)
                .Index(t => t.SalaId);
            
            CreateTable(
                "dbo.Especialidad",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MedicoEspecialidad",
                c => new
                    {
                        MedicoID = c.Int(nullable: false),
                        EspecialidadID = c.Int(nullable: false),
                        Duracion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MedicoID, t.EspecialidadID })
                .ForeignKey("dbo.Medico", t => t.MedicoID)
                .ForeignKey("dbo.Especialidad", t => t.EspecialidadID)
                .Index(t => t.MedicoID)
                .Index(t => t.EspecialidadID);
            
            CreateTable(
                "dbo.Medico",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Matricula = c.Int(nullable: false),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        NroDocumento = c.Int(nullable: false),
                        FechaNacimiento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HorarioMedico",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Dia = c.Int(nullable: false),
                        HoraDesde = c.DateTime(nullable: false),
                        HoraHasta = c.DateTime(nullable: false),
                        MedicoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Medico", t => t.MedicoId)
                .Index(t => t.MedicoId);
            
            CreateTable(
                "dbo.Mascota",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        FechaNacimiento = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ClienteId = c.Int(nullable: false),
                        RazaId = c.Int(nullable: false),
                        Foto = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .ForeignKey("dbo.Raza", t => t.RazaId)
                .Index(t => t.ClienteId)
                .Index(t => t.RazaId);
            
            CreateTable(
                "dbo.Raza",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sala",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Locacion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Factura", "TurnoId", "dbo.Turno");
            DropForeignKey("dbo.Turno", "SalaId", "dbo.Sala");
            DropForeignKey("dbo.Turno", "MedicoId", "dbo.Medico");
            DropForeignKey("dbo.Turno", "MascotaId", "dbo.Mascota");
            DropForeignKey("dbo.Mascota", "RazaId", "dbo.Raza");
            DropForeignKey("dbo.Mascota", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Turno", "EspecialidadId", "dbo.Especialidad");
            DropForeignKey("dbo.MedicoEspecialidad", "EspecialidadID", "dbo.Especialidad");
            DropForeignKey("dbo.HorarioMedico", "MedicoId", "dbo.Medico");
            DropForeignKey("dbo.MedicoEspecialidad", "MedicoID", "dbo.Medico");
            DropForeignKey("dbo.ItemFactura", "ItemId", "dbo.Item");
            DropForeignKey("dbo.ItemFactura", "FacturaId", "dbo.Factura");
            DropForeignKey("dbo.Factura", "ClienteId", "dbo.Cliente");
            DropIndex("dbo.Mascota", new[] { "RazaId" });
            DropIndex("dbo.Mascota", new[] { "ClienteId" });
            DropIndex("dbo.HorarioMedico", new[] { "MedicoId" });
            DropIndex("dbo.MedicoEspecialidad", new[] { "EspecialidadID" });
            DropIndex("dbo.MedicoEspecialidad", new[] { "MedicoID" });
            DropIndex("dbo.Turno", new[] { "SalaId" });
            DropIndex("dbo.Turno", new[] { "MascotaId" });
            DropIndex("dbo.Turno", new[] { "EspecialidadId" });
            DropIndex("dbo.Turno", new[] { "MedicoId" });
            DropIndex("dbo.ItemFactura", new[] { "ItemId" });
            DropIndex("dbo.ItemFactura", new[] { "FacturaId" });
            DropIndex("dbo.Factura", new[] { "TurnoId" });
            DropIndex("dbo.Factura", new[] { "ClienteId" });
            DropTable("dbo.Sala");
            DropTable("dbo.Raza");
            DropTable("dbo.Mascota");
            DropTable("dbo.HorarioMedico");
            DropTable("dbo.Medico");
            DropTable("dbo.MedicoEspecialidad");
            DropTable("dbo.Especialidad");
            DropTable("dbo.Turno");
            DropTable("dbo.Item");
            DropTable("dbo.ItemFactura");
            DropTable("dbo.Factura");
            DropTable("dbo.Cliente");
        }
    }
}
