using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using System;
using System.Collections.Generic;
using MusicStore.Models;
using Xunit;
using System.Linq;
using Moq;
using MusicStore.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MusicStoreTest
{
    public class UnitTest1
    {


        [Fact]
        public void DetAuthorIndex()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DetAuthorIndexDatabase")
                .Options;

            var context = new ApplicationDbContext(options);
            var controller = new AuthorsController(context);


            using (var ctx = new ApplicationDbContext(options))
            {
                var authors = new List<Author>
                {
                    new Author {Firstname = "test", Lastname = "test"},
                    new Author {Firstname = "test2", Lastname = "test2"},
                    new Author {Firstname = "test3", Lastname = "test3"},
                };

                ctx.Author.AddRange(authors);
                ctx.SaveChanges();
            }


            var result = controller.Index().Result;


            Assert.IsType<ViewResult>(result);
            using (var ctx = new ApplicationDbContext(options))
            {
                Assert.Equal(3, ctx.Author.Count());
            }
        }

        [Fact]
        public void AddAuthorToDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AddAuthorDatabase")
                .Options;

            var context = new ApplicationDbContext(options);
            var controller = new AuthorsController(context);
            var result = controller.Create(new Author { Firstname = "test", Lastname = "testing", BirthYear = 2000 }).Result;


            Assert.IsType<RedirectToActionResult>(result);
            using (var ctx = new ApplicationDbContext(options))
            {
                Assert.Equal(1, ctx.Author.Count());
                Assert.Equal("test", ctx.Author.Single().Firstname);
            }
        }

        [Fact]
        public void GetAuthorDetails()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetDetailsDatabase")
                .Options;
            var context = new ApplicationDbContext(options);

            var controller = new AuthorsController(context);


            using (var ctx = new ApplicationDbContext(options))
            {
                var author = new Author { Firstname = "test", Lastname = "test" };
                ctx.Author.Add(author);
                ctx.SaveChanges();
            }

            var result = controller.Details(1).Result;
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Author>(
                viewResult.ViewData.Model);
        }


        [Fact]
        public void EditAuthor()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EditAuthorDatabase")
                .Options;
            var context = new ApplicationDbContext(options);

            var controller = new AuthorsController(context);



            using (var ctx = new ApplicationDbContext(options))
            {
                var author = new Author { Firstname = "test", Lastname = "test" };
                ctx.Author.Add(author);
                ctx.SaveChanges();
            }

            var result = controller.Edit(1, new Author { Firstname = "edited", Lastname = "edited", BirthYear = 2000, Id = 1 }).Result;
            Assert.IsType<RedirectToActionResult>(result);
            using (var ctx = new ApplicationDbContext(options))
            {
                var author = ctx.Author.Find(1);
                Assert.Equal("edited", author.Firstname);
                ctx.SaveChanges();
            }
        }


        [Fact]
        public void DeleteAuthor()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteAuthorDatabase")
                .Options;
            var context = new ApplicationDbContext(options);

            var controller = new AuthorsController(context);



            using (var ctx = new ApplicationDbContext(options))
            {
                var author = new Author { Firstname = "test", Lastname = "test" };
                ctx.Author.Add(author);
                ctx.SaveChanges();
            }

            var result = controller.DeleteConfirmed(1).Result;
            Assert.IsType<RedirectToActionResult>(result);
            using (var ctx = new ApplicationDbContext(options))
            {
                Assert.Equal(0, ctx.Author.Count());
                ctx.SaveChanges();
            }
        }

    }
}
