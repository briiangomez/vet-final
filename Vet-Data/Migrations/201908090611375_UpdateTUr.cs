namespace Vet_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTUr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Turno", "ItemId", c => c.Int(nullable: false));
            CreateIndex("dbo.Turno", "ItemId");
            AddForeignKey("dbo.Turno", "ItemId", "dbo.Item", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Turno", "ItemId", "dbo.Item");
            DropIndex("dbo.Turno", new[] { "ItemId" });
            DropColumn("dbo.Turno", "ItemId");
        }
    }
}
