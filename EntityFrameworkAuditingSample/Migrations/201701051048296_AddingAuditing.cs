namespace EntityFrameworkAuditingSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAuditing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Audits",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ChangeType = c.String(),
                        ObjectType = c.String(),
                        FromJson = c.String(),
                        ToJson = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        AuditUserId = c.String(maxLength: 128),
                        TableName = c.String(),
                        IdentityJson = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuditUserId)
                .Index(t => t.AuditUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Audits", "AuditUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Audits", new[] { "AuditUserId" });
            DropTable("dbo.Audits");
        }
    }
}
