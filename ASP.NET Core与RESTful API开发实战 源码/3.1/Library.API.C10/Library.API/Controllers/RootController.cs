using Library.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Library.API.Controllers
{
    [Route("api")]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var links = new List<Link>();
            links.Add(new Link(HttpMethods.Get,
              "self",
              Url.Link(nameof(GetRoot), null)));

            links.Add(new Link(HttpMethods.Get,
                "get authors",
                Url.Link(nameof(AuthorController.GetAuthorsAsync), null)));

            links.Add(new Link(HttpMethods.Post,
                "create author",
                Url.Link(nameof(AuthorController.CreateAuthorAsync), null)));

            links.Add(new Link(HttpMethods.Post,
                "add user",
                Url.Link(nameof(AuthenticateController.AddUserAsync), null)));

            links.Add(new Link(HttpMethods.Post,
                "get token",
                Url.Link(nameof(AuthenticateController.GenerateTokenAsync), null)));

            return Ok(links);
        }
    }
}