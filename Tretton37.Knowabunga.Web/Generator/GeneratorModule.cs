using System.Collections.Generic;
using Nancy;
using Tretton37.Knowabunga.Core;

namespace Tretton37.Knowabunga.Web.Generator
{
    public class GeneratorModule : NancyModule
    {
        public GeneratorModule(IPairGenerator pairGenerator)
        {
            Get["/"] = _ =>
            {
                var pairs = pairGenerator.Generate();
                return View["Views/Generated.cshtml", pairs];
            };
        }
    }
}