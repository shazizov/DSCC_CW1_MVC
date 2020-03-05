namespace EsavdoDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropUzbekNameColumn : DbMigration
    {
        public override void Up()
        {
            Sql("alter table Products drop column NameInUzbek");
        }
        
        public override void Down()
        {
        }
    }
}
