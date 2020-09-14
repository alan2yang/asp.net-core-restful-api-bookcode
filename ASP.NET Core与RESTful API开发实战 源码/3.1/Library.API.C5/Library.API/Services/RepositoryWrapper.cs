using Library.API.Entities;

namespace Library.API.Services
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IAuthorRepository _authorRepository = null;
        private IBookRepository _bookRepository = null;

        public RepositoryWrapper(LibraryDbContext libraryDbContext)
        {
            LibraryDbContext = libraryDbContext;
        }

        public IAuthorRepository Author => _authorRepository ?? new AuthorRepository(LibraryDbContext);
        public IBookRepository Book => _bookRepository ?? new BookRepository(LibraryDbContext);
        public LibraryDbContext LibraryDbContext { get; }
    }
}