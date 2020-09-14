using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorController : ControllerBase
    {
        public AuthorController(IAuthorRepository authorRepository)
        {
            AuthorRepository = authorRepository;
        }

        public IAuthorRepository AuthorRepository { get; }

        [HttpGet("{authorId}", Name = nameof(GetAuthor))]
        public IActionResult GetAuthor(Guid authorId)
        {
            var author = AuthorRepository.GetAuthor(authorId);
            if (author == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(author);
            }
        }

        [HttpDelete("{authorId}")]
        public IActionResult DeleteAuthor(Guid authorId)
        {
            var author = AuthorRepository.GetAuthor(authorId);
            if (author == null)
            {
                return NotFound();
            }

            AuthorRepository.DeleteAuthor(author);
            return NoContent();
        }

        [HttpGet()]
        public ActionResult<List<AuthorDto>> GetAuthors()
        {
            return AuthorRepository.GetAuthors().ToList();
        }

        [HttpPost()]
        public IActionResult CreateAuthor(AuthorForCreationDto authorForCreationDto)
        {
            var autoDto = new AuthorDto
            {
                Name = authorForCreationDto.Name,
                Age = authorForCreationDto.Age,
                Email = authorForCreationDto.Email
            };

            AuthorRepository.AddAuthor(autoDto);
            return CreatedAtRoute(nameof(GetAuthor), new { authorId = autoDto.Id }, autoDto);
        }
    }
}