namespace EsavdoDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCatModel : DbMigration
    {
        public override void Up()
        {
           
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
