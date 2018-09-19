namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyAnnotationsToGenre : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Genres", "GenreType", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Genres", "GenreType", c => c.String());
        }
    }
}
