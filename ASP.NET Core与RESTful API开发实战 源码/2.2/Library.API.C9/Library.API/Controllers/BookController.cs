using AutoMapper;
using Library.API.Entities;
using Library.API.Filters;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/authors/{authorId}/books")]
    [ApiController]
    [ServiceFilter(typeof(CheckAuthorExistFilterAttribute))]
    [Authorize]    
    public class BookController : ControllerBase
    {
        public BookController(IRepositoryWrapper repositoryWrapper,
            IMapper mapper,
            IMemoryCache memoryCache,
            IHashFactory hashFactory)
        {
            RepositoryWrapper = repositoryWrapper;
            Mapper = mapper;
            MemoryCache = memoryCache;
            HashFactory = hashFactory;
        }

        public IHashFactory HashFactory { get; }
        public IMapper Mapper { get; }
        public IMemoryCache MemoryCache { get; }
        public IRepositoryWrapper RepositoryWrapper { get; }

        [HttpPost(Name = nameof(AddBookAsync))]        
        public async Task<IActionResult> AddBookAsync(Guid authorId, BookForCreationDto bookForCreationDto)
        {
            var book = Mapper.Map<Book>(bookForCreationDto);

            book.AuthorId = authorId;
            RepositoryWrapper.Book.Create(book);
            if (!await RepositoryWrapper.Book.SaveAsync())
            {
                throw new Exception("创建资源Book失败");
            }

            var bookDto = Mapper.Map<BookDto>(book);
            return CreatedAtRoute(nameof(GetBookAsync), new { bookId = bookDto.Id }, CreateLinksForBook(bookDto));
        }

        [HttpDelete("{bookId}", Name = nameof(DeleteBookAsync))]
        public async Task<IActionResult> DeleteBookAsync(Guid authorId, Guid bookId)
        {
            var book = await RepositoryWrapper.Book.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }

            RepositoryWrapper.Book.Delelte(book);
            if (!await RepositoryWrapper.Book.SaveAsync())
            {
                throw new Exception("删除资源Book失败");
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("{bookId}", Name = nameof(GetBookAsync))]
        public async Task<ActionResult<BookDto>> GetBookAsync(Guid authorId, Guid bookId)
        {
            var book = await RepositoryWrapper.Book.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }

            var entityHash = HashFactory.GetHash(book);
            Response.Headers[HeaderNames.ETag] = entityHash;

            var bookDto = Mapper.Map<BookDto>(book);
            return CreateLinksForBook(bookDto);
        }

        [AllowAnonymous]
        [HttpGet(Name = nameof(GetBooksAsync))]
        public async Task<ActionResult<ResourceCollection<BookDto>>> GetBooksAsync(Guid authorId)
        {
            List<BookDto> bookDtoList = new List<BookDto>();
            string key = $"{authorId}_books";
            if (!MemoryCache.TryGetValue(key, out bookDtoList))
            {
                var books = await RepositoryWrapper.Book.GetBooksAsync(authorId);
                bookDtoList = Mapper.Map<IEnumerable<BookDto>>(books).ToList();

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                options.AbsoluteExpiration = DateTime.Now.AddMinutes(30);
                options.Priority = CacheItemPriority.Normal;
                MemoryCache.Set(key, bookDtoList, options);
            }

            bookDtoList = bookDtoList.Select(CreateLinksForBook).ToList();
            var bookList = new ResourceCollection<BookDto>(bookDtoList);
            return CreateLinksForBooks(bookList);
        }

        [HttpPatch("{bookId}", Name = nameof(PartiallyUpdateBookAsync))]
        [CheckIfMatchHeaderFilter]
        public async Task<IActionResult> PartiallyUpdateBookAsync(Guid authorId, Guid bookId, JsonPatchDocument<BookForUpdateDto> patchDocument)
        {
            var book = await RepositoryWrapper.Book.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }

            var entityHash = HashFactory.GetHash(book);
            if (Request.Headers.TryGetValue(HeaderNames.IfMatch, out var requestETag)
                && requestETag != entityHash)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed);
            }

            var bookUpdateDto = Mapper.Map<BookForUpdateDto>(book);
            patchDocument.ApplyTo(bookUpdateDto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(bookUpdateDto, book, typeof(BookForUpdateDto), typeof(Book));

            RepositoryWrapper.Book.Update(book);
            if (!await RepositoryWrapper.Book.SaveAsync())
            {
                throw new Exception("更新资源Book失败");
            }

            var entityNewHash = HashFactory.GetHash(book);
            Response.Headers[HeaderNames.ETag] = entityNewHash;

            return NoContent();
        }

        [HttpPut("{bookId}", Name = nameof(UpdateBookAsync))]
        [CheckIfMatchHeaderFilter]
        public async Task<IActionResult> UpdateBookAsync(Guid authorId, Guid bookId, BookForUpdateDto updatedBook)
        {
            var book = await RepositoryWrapper.Book.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }

            var entityHash = HashFactory.GetHash(book);
            if (Request.Headers.TryGetValue(HeaderNames.IfMatch, out var requestETag)
                && requestETag != entityHash)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed);
            }

            Mapper.Map(updatedBook, book, typeof(BookForUpdateDto), typeof(Book));
            RepositoryWrapper.Book.Update(book);
            if (!await RepositoryWrapper.Book.SaveAsync())
            {
                throw new Exception("更新资源Book失败");
            }

            var entityNewHash = HashFactory.GetHash(book);
            Response.Headers[HeaderNames.ETag] = entityNewHash;

            return NoContent();
        }

        private BookDto CreateLinksForBook(BookDto book)
        {
            book.Links.Add(new Link(HttpMethods.Get,
                "self",
                Url.Link(nameof(GetBookAsync), new { bookId = book.Id })));

            book.Links.Add(new Link(HttpMethods.Delete,
                "delete book",
                Url.Link(nameof(DeleteBookAsync), new { bookId = book.Id })));

            book.Links.Add(new Link(HttpMethods.Put,
               "update book",
               Url.Link(nameof(BookController.UpdateBookAsync), new { bookId = book.Id })));

            book.Links.Add(new Link(HttpMethods.Patch,
               "update book partially",
               Url.Link(nameof(BookController.PartiallyUpdateBookAsync), new { bookId = book.Id })));

            return book;
        }

        private ResourceCollection<BookDto> CreateLinksForBooks(ResourceCollection<BookDto> books)
        {
            books.Links.Add(new Link(HttpMethods.Get,
                "self",
                Url.Link(nameof(GetBooksAsync), null)));

            books.Links.Add(new Link(HttpMethods.Post,
                "create book",
                Url.Link(nameof(AddBookAsync), null)));
            return books;
        }
    }
}