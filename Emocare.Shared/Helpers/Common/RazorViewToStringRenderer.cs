using Emocare.Domain.Interfaces.Helper.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using RazorLight;
namespace Emocare.Shared.Helpers.Common
{

        public class RazorViewToStringRenderer : IRazorViewToStringRenderer
        {
            private readonly RazorLightEngine _razorEngine;

        public RazorViewToStringRenderer()
        {
            _razorEngine = new RazorLightEngineBuilder()
         .UseEmbeddedResourcesProject(typeof(RazorViewToStringRenderer).Assembly, rootNamespace: "Emocare.Shared")
         .UseMemoryCachingProvider()
         .Build();
        }

        public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
            {
                var templateFile = $"{viewName}.cshtml"; // "OtpEmail.cshtml"
                return await _razorEngine.CompileRenderAsync(templateFile, model);
            }
        }
}