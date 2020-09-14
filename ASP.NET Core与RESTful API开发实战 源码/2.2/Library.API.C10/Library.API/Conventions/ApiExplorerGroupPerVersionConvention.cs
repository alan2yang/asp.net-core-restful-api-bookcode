using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace Library.API.Conventions
{
    public class ApiExplorerGroupPerVersionConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace;
            var apiVersion = controllerNamespace.ToLower()
                .Split('.')
                .FirstOrDefault(m => m == "v1" || m == "v2");

            if (string.IsNullOrWhiteSpace(apiVersion))
            {
                apiVersion = "v1";
            }

            controller.ApiExplorer.GroupName = apiVersion;
        }
    }
}