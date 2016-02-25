namespace WebAPIToolkit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v05 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Client = c.String(),
                        Practice = c.Int(nullable: false),
                        Manager = c.String(),
                        PersonInCharge = c.String(),
                        Contact = c.String(),
                        Status = c.Int(nullable: false),
                        Phase = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Projects");
        }
    }
}
