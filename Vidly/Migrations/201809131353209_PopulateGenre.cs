namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenre : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Genres ON");
            Sql("INSERT INTO Genres (Id, GenreType) VALUES (1,'Action')");
            Sql("INSERT INTO Genres (Id, GenreType) VALUES (2,'Adventure')");
            Sql("INSERT INTO Genres (Id, GenreType) VALUES (3,'Animation')");
            Sql("INSERT INTO Genres (Id, GenreType) VALUES (4,'Comedy')");
            Sql("INSERT INTO Genres (Id, GenreType) VALUES (5,'Crime')");
            Sql("INSERT INTO Genres (Id, GenreType) VALUES (6,'Drama')");
            Sql("INSERT INTO Genres (Id, GenreType) VALUES (7,'Historical')");
            Sql("INSERT INTO Genres (Id, GenreType) VALUES (8,'Horror')");
            Sql("INSERT INTO Genres (Id, GenreType) VALUES (9,'Musical')");
            Sql("INSERT INTO Genres (Id, GenreType) VALUES (10,'Romance')");
            Sql("INSERT INTO Genres (Id, GenreType) VALUES (11,'Science Fiction')");
            Sql("INSERT INTO Genres (Id, GenreType) VALUES (12,'Sports')");
            Sql("INSERT INTO Genres (Id, GenreType) VALUES (13,'War')");
            Sql("INSERT INTO Genres (Id, GenreType) VALUES (14,'Western')");
            Sql("SET IDENTITY_INSERT Genres OFF");
        }
        
        public override void Down()
        {
        }
    }
}
