using Emocare.Domain.Interfaces.Helper.Common;
using RazorLight;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Emocare.Shared.Helpers.Common
{
    public class RazorViewToStringRenderer : IRazorViewToStringRenderer
    {
        private readonly RazorLightEngine _razorEngine;

        public RazorViewToStringRenderer()
        {
            // 1. Define the specific namespace where your templates live.
            // Format: AssemblyName.FolderName.SubFolderName
            var rootNamespace = "Emocare.Shared.Views";

            _razorEngine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(RazorViewToStringRenderer).Assembly, rootNamespace)
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
        {
            // 2. With Embedded Resources, the key is just the file name.
            // Ensure viewName matches the file exactly (e.g., "OtpEmailView").
            var templateKey = $"{viewName}.cshtml";

            return await _razorEngine.CompileRenderAsync(templateKey, model);
        }
    }
}