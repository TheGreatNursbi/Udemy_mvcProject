namespace Proj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Populating : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MembershipTypes (Id, Name, SignUpFee, DurationInMonth, DiscountRate) VALUES(1, 'Pay As Yo Go', 0, 0, 0)");
            Sql("INSERT INTO MembershipTypes (Id, Name, SignUpFee, DurationInMonth, DiscountRate) VALUES(2, 'Monthly', 30, 1, 10)");
            Sql("INSERT INTO MembershipTypes (Id, Name, SignUpFee, DurationInMonth, DiscountRate) VALUES(3, 'Quaterly', 90, 3, 15)");
            Sql("INSERT INTO MembershipTypes (Id, Name, SignUpFee, DurationInMonth, DiscountRate) VALUES(4, 'Manually', 300, 12, 20)");
            Sql("INSERT INTO Genres (Id, Name) VALUES(1, 'Action')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(2, 'Drama')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(3, 'Historic')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(4, 'Adult Content')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(5, 'Comedy')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(6, 'Art House')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(7, 'Cartoon')");
        }
        
        public override void Down()
        {
        }
    }
}
