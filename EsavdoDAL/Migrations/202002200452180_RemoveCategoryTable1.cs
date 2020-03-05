namespace EsavdoDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCategoryTable1 : DbMigration
    {
        public override void Up()
        {
            Sql("drop table Categories");
        }
        
        public override void Down()
        {
        }
    }
}
