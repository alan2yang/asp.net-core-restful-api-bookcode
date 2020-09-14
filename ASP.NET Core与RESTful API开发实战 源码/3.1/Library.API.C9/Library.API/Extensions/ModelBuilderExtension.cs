using Library.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.API.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            // Authors
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = new Guid("72D5B5F5-3008-49B7-B0D6-CC337F1A3330"),
                    Name = "Author 1",
                    BirthDate = new DateTimeOffset(new DateTime(1960, 11, 18)),
                    BirthPlace = "Beijing",
                    Email = "author1@xxx.com"
                },
                new Author
                {
                    Id = new Guid("7D04A48E-BE4E-468E-8CE2-3AC0A0C79549"),
                    Name = "Author 2",
                    BirthDate = new DateTimeOffset(new DateTime(1976, 8, 23)),
                    BirthPlace = "Hubei",
                    Email = "author2@xxx.com"
                },
                new Author
                {
                    Id = new Guid("8406B13E-A793-4B12-84CB-7FE2A694B9AA"),
                    Name = "Author 3",
                    BirthDate = new DateTimeOffset(new DateTime(1973, 2, 8)),
                    BirthPlace = "Hubei",
                    Email = "author3@xxx.com"
                },
                new Author
                {
                    Id = new Guid("74556ABD-1A6C-4D20-A8A7-271DD4393B2E"),
                    Name = "Author 4",
                    BirthDate = new DateTimeOffset(new DateTime(1978, 7, 13)),
                    BirthPlace = "Shandong",
                    Email = "author4@xxx.com"
                },
                new Author
                {
                    Id = new Guid("1029DB57-C15C-4C0C-80A0-C811B7995CB4"),
                    Name = "Author 5",
                    BirthDate = new DateTimeOffset(new DateTime(1973, 4, 25)),
                    BirthPlace = "Beijing",
                    Email = "author5@xxx.com"
                },
                new Author
                {
                    Id = new Guid("0F978CF6-DF6D-47A9-8EF2-D2723CC29CC8"),
                    Name = "Author 6",
                    BirthDate = new DateTimeOffset(new DateTime(1981, 5, 4)),
                    BirthPlace = "Beijing",
                    Email = "author6@xxx.com"
                },
                new Author
                {
                    Id = new Guid("10EE3976-D672-4411-AE1C-3267BAA940EB"),
                    Name = "Author 7",
                    BirthDate = new DateTimeOffset(new DateTime(1954, 9, 21)),
                    BirthPlace = "Shandong",
                    Email = "author7@xxx.com"
                },
                new Author
                {
                    Id = new Guid("2633A79C-9F4A-48D5-AE5A-70945FB8583C"),
                    Name = "Author 8",
                    BirthDate = new DateTimeOffset(new DateTime(1981, 9, 4)),
                    BirthPlace = "Shandong",
                    Email = "author8@xxx.com"
                });

            // Books
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = new Guid("7D8EBDA9-2634-4C0F-9469-0695D6132153"),
                    Title = "Book 1",
                    Description = "Description of Book 1",
                    Pages = 281,
                    AuthorId = new Guid("72D5B5F5-3008-49B7-B0D6-CC337F1A3330")
                },
                new Book
                {
                    Id = new Guid("1ED47697-AA7D-48C2-AA39-305D0E13B3AA"),
                    Title = "Book 2",
                    Description = "Description of Book 2",
                    Pages = 370,
                    AuthorId = new Guid("72D5B5F5-3008-49B7-B0D6-CC337F1A3330")
                },
                new Book
                {
                    Id = new Guid("5F82C852-375D-4926-A3B7-84B63FC1BFAE"),
                    Title = "Book 3",
                    Description = "Description of Book 3",
                    Pages = 229,
                    AuthorId = new Guid("7D04A48E-BE4E-468E-8CE2-3AC0A0C79549")
                },
                new Book
                {
                    Id = new Guid("418A5B20-460B-4604-BE17-2B0809E19ACD"),
                    Title = "Book 4",
                    Description = "Description of Book 4",
                    Pages = 440,
                    AuthorId = new Guid("7D04A48E-BE4E-468E-8CE2-3AC0A0C79549")
                });
        }
    }
}