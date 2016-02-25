namespace WebAPIToolkit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v02 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Contact = c.String(),
                        DueDate = c.DateTime(),
                        Manager = c.String(),
                        Name = c.String(),
                        State = c.Int(nullable: false),
                        Project_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.Project_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectTasks", "Project_Id", "dbo.Projects");
            DropIndex("dbo.ProjectTasks", new[] { "Project_Id" });
            DropTable("dbo.ProjectTasks");
        }
    }
}
