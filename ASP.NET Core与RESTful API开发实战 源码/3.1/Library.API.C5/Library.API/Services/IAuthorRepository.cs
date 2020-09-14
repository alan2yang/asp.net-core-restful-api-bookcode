using Library.API.Entities;
using System;

namespace Library.API.Services
{
    public interface IAuthorRepository : IRepositoryBase<Author>, IRepositoryBase2<Author, Guid>
    {
    }
}
