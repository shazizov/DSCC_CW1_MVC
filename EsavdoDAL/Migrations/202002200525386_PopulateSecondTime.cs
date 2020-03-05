namespace EsavdoDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateSecondTime : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Products (Name, Price, Available, Category) values ('Umbrella', 550, 0, 'Home')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Bathroom Scale', 300, 1, 'Home')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Curtain', 100, 1, 'Home')");
            Sql("insert into Products (Name, Price, Available, Category) values ('USB Flash Drive', 50, 0, 'Office')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Smoke Detector', 150, 0, 'Office')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Keyboard', 200, 1, 'Office')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Laptop', 680, 1, 'Office')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Digital Watch', 470, 1, 'Jewelry&Watches')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Bracelet', 70, 0, 'Jewelry&Watches')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Fine Earrings', 800, 1, 'Jewelry&Watches')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Gemstones', 980, 1, 'Jewelry&Watches')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Jacket', 230, 1, 'Men Fashion')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Sweater', 210, 1, 'Men Fashion')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Sock', 55, 1, 'Men Fashion')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Belt', 79, 1, 'Men Fashion')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Shirt', 340, 1, 'Women Fashion')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Dress', 500, 1, 'Women Fashion')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Jeans', 600, 1, 'Women Fashion')");
            Sql("insert into Products (Name, Price, Available, Category) values ('Gloves', 420, 1, 'Women Fashion')");
        }
        
        public override void Down()
        {
        }
    }
}
