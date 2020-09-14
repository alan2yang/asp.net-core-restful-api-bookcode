using GraphQL;
using GraphQL.Types;
using Library.API.GraphQLSchema;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("graphql")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        public GraphQLController(ISchema librarySchema, IDocumentExecuter documentExecuter)
        {
            LibrarySchema = librarySchema;
            DocumentExecuter = documentExecuter;
        }

        public IDocumentExecuter DocumentExecuter { get; }
        public ISchema LibrarySchema { get; }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GraphQLRequest query)
        {
            var result = await DocumentExecuter.ExecuteAsync(options =>
            {
                options.Schema = LibrarySchema;
                options.Query = query.Query;
            });

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}