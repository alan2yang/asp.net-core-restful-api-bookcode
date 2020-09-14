using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Data
{
    public class LibraryMockData
    {
        // 获取 LibraryMockData 实例
        public static LibraryMockData Current { get; } = new LibraryMockData();
        public List<AuthorDto> Authors { get; set; }
        public List<BookDto> Books { get; set; }
        public LibraryMockData()
        {
            Authors = new List<AuthorDto> {
                new AuthorDto { Id = new Guid("72D5B5F5-3008-49B7-B0D6-CC337F1A3330") , Name = "Author 1", Age=46, Email = "author1@xxx.com" },
                new AuthorDto { Id = new Guid("7D04A48E-BE4E-468E-8CE2-3AC0A0C79549"), Name = "Author 2",Age=38, Email = "author2@xxx.com" }
            };

            Books = new List<BookDto> {
                new BookDto {
                    Id = new Guid("7D8EBDA9-2634-4C0F-9469-0695D6132153"),
                    Title = "Book 1" ,
                    Description = "Description of Book 1",
                    Pages = 281,
                    AuthorId = new Guid("72D5B5F5-3008-49B7-B0D6-CC337F1A3330")},
                new BookDto {
                    Id = new Guid("1ED47697-AA7D-48C2-AA39-305D0E13B3AA"),
                    Title = "Book 2",
                    Description = "Description of Book 2",
                    Pages = 370,
                    AuthorId = new Guid("72D5B5F5-3008-49B7-B0D6-CC337F1A3330")},
                new BookDto {
                    Id = new Guid("5F82C852-375D-4926-A3B7-84B63FC1BFAE"),
                    Title = "Book 3",
                    Description = "Description of Book 3",
                    Pages = 229,
                    AuthorId = new Guid("7D04A48E-BE4E-468E-8CE2-3AC0A0C79549")},
                new BookDto { Id = new Guid("418A5B20-460B-4604-BE17-2B0809E19ACD"),
                    Title = "Book 4",
                    Description = "Description of Book 4",
                    Pages=440,
                    AuthorId= new Guid("7D04A48E-BE4E-468E-8CE2-3AC0A0C79549")}
            };
        }
    }
}
