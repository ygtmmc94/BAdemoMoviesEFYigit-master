﻿namespace MoviesEF.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MoviesEF.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<MoviesEF.Contexts.MoviesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MoviesEF.Contexts.MoviesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Database.ExecuteSqlCommand("delete from MovieDirectors");
            context.Database.ExecuteSqlCommand("delete from Reviews");
            context.Database.ExecuteSqlCommand("delete from Movies");
            context.Database.ExecuteSqlCommand("delete from Directors");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('MovieDirectors', RESEED, 0)");    //ID'leri sıfırlar.
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Reviews', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Movies', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Directors', RESEED, 0)");
            //Movie tablosundaki veriler
            List<Movie> movieList = new List<Movie>
            {
                new Movie { Id = 1, Name = "Avatar", ProductionYear = "2009", BoxOfficeReturn = 1000000.0000,
                    MovieDirectors = new List<MovieDirector>(), Reviews = new List<Review>() },
                new Movie { Id = 2, Name = "Sherlock Holmes", ProductionYear = "2009",
                    MovieDirectors = new List<MovieDirector>(), Reviews = new List<Review>() },
                new Movie { Id = 3, Name = "Law Abiding Citizen", ProductionYear = "2009", BoxOfficeReturn = 300000.0000,
                    MovieDirectors = new List<MovieDirector>(), Reviews = new List<Review>() },
                new Movie { Id = 4, Name = "Alien", ProductionYear = "1979", BoxOfficeReturn = 7000000.0000,
                    MovieDirectors = new List<MovieDirector>(), Reviews = new List<Review>() },
                new Movie { Id = 5, Name = "Aliens", ProductionYear = "1986", BoxOfficeReturn = 10000000.0000,
                    MovieDirectors = new List<MovieDirector>(), Reviews = new List<Review>() },
                new Movie { Id = 6, Name = "Alien 3", ProductionYear = "1992", BoxOfficeReturn = 4000000.0000,
                    MovieDirectors = new List<MovieDirector>(), Reviews = new List<Review>() }
            };
            //Director tablosundaki veriler
            List<Director> directorList = new List<Director>
            {
                new Director { Id = 1, Name = "James", Surname = "Cameron", Retired = false,
                    MovieDirectors = new List<MovieDirector>() },
                new Director { Id = 2, Name = "Guy", Surname = "Ritchie", Retired = false,
                    MovieDirectors = new List<MovieDirector>() },
                new Director { Id = 3, Name = "F. Gary", Surname = "Gray", Retired = false,
                    MovieDirectors = new List<MovieDirector>() },
                new Director { Id = 4, Name = "Ridley", Surname = "Scott", Retired = false,
                    MovieDirectors = new List<MovieDirector>() },
                new Director { Id = 5, Name = "David", Surname = "Fincher", Retired = false,
                    MovieDirectors = new List<MovieDirector>() }
            };
            //Review tablosundaki veriler
            List<Review> reviewList = new List<Review>
            {
                new Review { Id = 1, Content = "Very good movie.", Rating = 9, Reviewer = "Cagil Alsac", MovieId = 1 }
            };
            foreach (Movie movie in movieList)
            {
                List<Review> reviews = reviewList.Where(r => r.MovieId == movie.Id).ToList();
                foreach (Review review in reviews)
                {
                    movie.Reviews.Add(review);
                }
            }
            //MovieDirector tablosundaki veriler
            List<MovieDirector> movieDirectorList = new List<MovieDirector>
            {
                new MovieDirector { Id = 1, MovieId = 1, DirectorId = 1 },
                new MovieDirector { Id = 2, MovieId = 2, DirectorId = 2 },
                new MovieDirector { Id = 3, MovieId = 3, DirectorId = 3 },
                new MovieDirector { Id = 4, MovieId = 4, DirectorId = 4 },
                new MovieDirector { Id = 5, MovieId = 5, DirectorId = 1 },
                new MovieDirector { Id = 6, MovieId = 6, DirectorId = 5 }
            };
            foreach (Movie movie in movieList)
            {
                List<MovieDirector> movieDirectors = movieDirectorList.Where(m => m.MovieId == movie.Id).ToList();
                foreach (MovieDirector movieDirector in movieDirectors)
                {
                    movie.MovieDirectors.Add(movieDirector);
                }
            }
            foreach (Director director in directorList)
            {
                List<MovieDirector> movieDirectors = movieDirectorList.Where(m => m.DirectorId == director.Id).ToList();
                foreach (MovieDirector movieDirector in movieDirectors)
                {
                    director.MovieDirectors.Add(movieDirector);
                }
            }
            //VERİLERİ DB'E YÜKLEME KODLARI
            foreach (Movie movie in movieList)
            {
                context.Movies.AddOrUpdate(m => m.Name,     //m.Name'e ait bir kayıt varsa ekleme yapmaz update yapar, m.Name'e ait bir kayıt yoksa ekleme yapar.
                    new Movie
                    {
                        Name = movie.Name,
                        ProductionYear = movie.ProductionYear,
                        BoxOfficeReturn = movie.BoxOfficeReturn,
                        MovieDirectors = movie.MovieDirectors,
                        Reviews = movie.Reviews
                    }
                );
            }
            foreach (Director director in directorList)
            {
                context.Directors.AddOrUpdate(d => new { d.Name, d.Surname },
                    new Director
                    {
                        Name = director.Name,
                        Surname = director.Surname,
                        Retired = director.Retired,
                        MovieDirectors = director.MovieDirectors
                    }
                );
            }
        }
    }
}
