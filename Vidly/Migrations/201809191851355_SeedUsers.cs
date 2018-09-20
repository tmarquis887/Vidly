namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f35bbd18-610b-4fec-a19b-fc934bdd0076', N'guest@vidly.com', 0, N'ADYn5SPLpKRkmQSqCDhs1OsnzEUVAkVuJ2tZ68P1GQokS+mW+VcaxiNKxpQiHTgDww==', N'f3a82afa-694e-4f1a-82b9-67364b8c6bc1', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f66bfcd0-63da-4950-98d6-bc280e77fe20', N'admin@vidly.com', 0, N'ANlJ5NIYzoYQXOR4ohks2KANR8FlHOxwlnw7IYe+SKOJg72iMrUzbUHhWrR1rjubyA==', N'332f13b6-bfe8-4431-9721-b5159c78cb20', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'bad4173a-3a58-404f-9859-a6db8b6ff857', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f66bfcd0-63da-4950-98d6-bc280e77fe20', N'bad4173a-3a58-404f-9859-a6db8b6ff857')
");
        }
        
        public override void Down()
        {
        }
    }
}
