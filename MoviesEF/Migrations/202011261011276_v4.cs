namespace MoviesEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovieDirectors", "DirectorId", "dbo.Directors");
            DropForeignKey("dbo.MovieDirectors", "MovieId", "dbo.Movies");
            DropIndex("dbo.MovieDirectors", new[] { "MovieId" });
            DropIndex("dbo.MovieDirectors", new[] { "DirectorId" });
            AlterColumn("dbo.MovieDirectors", "MovieId", c => c.Int());
            AlterColumn("dbo.MovieDirectors", "DirectorId", c => c.Int());
            CreateIndex("dbo.MovieDirectors", "MovieId");
            CreateIndex("dbo.MovieDirectors", "DirectorId");
            AddForeignKey("dbo.MovieDirectors", "DirectorId", "dbo.Directors", "Id");
            AddForeignKey("dbo.MovieDirectors", "MovieId", "dbo.Movies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieDirectors", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieDirectors", "DirectorId", "dbo.Directors");
            DropIndex("dbo.MovieDirectors", new[] { "DirectorId" });
            DropIndex("dbo.MovieDirectors", new[] { "MovieId" });
            AlterColumn("dbo.MovieDirectors", "DirectorId", c => c.Int(nullable: false));
            AlterColumn("dbo.MovieDirectors", "MovieId", c => c.Int(nullable: false));
            CreateIndex("dbo.MovieDirectors", "DirectorId");
            CreateIndex("dbo.MovieDirectors", "MovieId");
            AddForeignKey("dbo.MovieDirectors", "MovieId", "dbo.Movies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MovieDirectors", "DirectorId", "dbo.Directors", "Id", cascadeDelete: true);
        }
    }
}
