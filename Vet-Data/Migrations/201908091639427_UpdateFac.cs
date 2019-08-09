namespace Vet_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFac : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Turno", "EstadoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Turno", "EstadoId", c => c.Int(nullable: false));
        }
    }
}
