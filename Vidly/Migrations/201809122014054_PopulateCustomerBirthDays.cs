namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCustomerBirthDays : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Customers SET BirthDate = '1990-11-22' WHERE Id = 1");
        }
        
        public override void Down()
        {
        }
    }
}
