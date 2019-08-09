namespace Vet_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdMedico : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MedicoEspecialidad");
            AddColumn("dbo.Especialidad", "Duracion", c => c.Int(nullable: false));
            AddColumn("dbo.MedicoEspecialidad", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.MedicoEspecialidad", "ID");
            DropColumn("dbo.MedicoEspecialidad", "Duracion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicoEspecialidad", "Duracion", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.MedicoEspecialidad");
            DropColumn("dbo.MedicoEspecialidad", "ID");
            DropColumn("dbo.Especialidad", "Duracion");
            AddPrimaryKey("dbo.MedicoEspecialidad", new[] { "MedicoID", "EspecialidadID" });
        }
    }
}
