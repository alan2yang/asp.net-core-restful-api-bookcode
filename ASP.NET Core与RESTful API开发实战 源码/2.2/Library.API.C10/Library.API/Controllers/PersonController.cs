using Microsoft.AspNetCore.Mvc;

#region NewsController,MapToApiVersion示例

namespace Library.API.Controllers
{
    [Route("api/news")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class NewsController : ControllerBase
    {
        [HttpGet]
        [ApiExplorerSettings(GroupName = "v1")]
        public ActionResult<string> Get() => "Result from v1";

        [HttpGet, MapToApiVersion("2.0")]
        [ApiExplorerSettings(GroupName = "v2")]
        public ActionResult<string> GetV2() => "Result from v2";
    }
}

#endregion NewsController,MapToApiVersion示例

#region PersonController，使用查询字符串指定版本

namespace Library.API.Controllers.V1
{
    [Route("api/person")]
    [ApiVersion("1.0")]
    public class PersonController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => "Result from v1";
    }
}

namespace Library.API.Controllers.V2
{
    [Route("api/person")]
    [ApiVersion("2.0")]
    public class PersonController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => "Result from v2";
    }
}

#endregion PersonController，使用查询字符串指定版本

#region StudentController，使用URL路径指定版本

namespace Library.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/students")]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => "Result from v1";
    }
}

namespace Library.API.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/students")]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => "Result from v2";
    }
}

#endregion StudentController，使用URL路径指定版本

#region HelloWorldController，Deprecated示例

namespace Library.API.Controllers
{
    [ApiVersion("2.0")]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/[controller]")]
    [NonController]
    public class HelloWorldController : Controller
    {
        [HttpGet]
        public string Get() => "Hello world!";

        [HttpGet, MapToApiVersion("2.0")]
        public string GetV2() => "Hello world v2.0!";
    }
}

#endregion HelloWorldController，Deprecated示例

#region ProjectController，使用Convention

namespace Library.API.Controllers.V1
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        [HttpGet]
        public string Get() => "Result from v1";
    }
}

namespace Library.API.Controllers.V2
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        [HttpGet]
        public string Get() => "Result from v2";
    }
}

#endregion ProjectController，使用Convention