namespace Ticketing_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tickets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SendDate = c.DateTime(nullable: false),
                        Sender = c.String(),
                        Description = c.String(),
                        Heading = c.String(),
                        Type = c.String(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tickets");
        }
    }
}
