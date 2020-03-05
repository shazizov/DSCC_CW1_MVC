namespace EsavdoDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateProductsTable : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Products (Name, Price, Available, Category) values ('Microwave', 400, 1, 'Home')");
        }
        
        public override void Down()
        {
        }
    }
}
