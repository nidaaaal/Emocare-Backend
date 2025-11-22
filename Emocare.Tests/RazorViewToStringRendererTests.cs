using Xunit;
using Emocare.Shared.Helpers.Common; // Your class to test
using RazorLight; // For the exception
using System.Threading.Tasks;
using Emocare.Shared.TestTemplates;

namespace Emocare.Tests
{
    public class RazorViewToStringRendererTests
    {
        [Fact]
        public async Task RenderViewToStringAsync_Should_RenderTemplate_When_TemplateExists()
        {
            // Arrange
            // We create a real instance of the class we are testing.
            var renderer = new RazorViewToStringRenderer();

            var model = new SimpleEmailModel { Name = "TestUser" };

            // The key must match the path from Step 1
            // File: Emocare.Shared/TestTemplates/SimpleEmail.cshtml
            // Key: "TestTemplates.SimpleEmail"
            var templateKey = "TestTemplates.SimpleEmail";

            // Act
            var result = await renderer.RenderViewToStringAsync(templateKey, model);

            // Assert
            // We use Trim() to ignore any potential leading/trailing whitespace
            Assert.Equal("Hello TestUser!", result.Trim());
        }

        [Fact]
        public async Task RenderViewToStringAsync_Should_ThrowException_When_TemplateDoesNotExist()
        {
            // Arrange
            var renderer = new RazorViewToStringRenderer();
            var model = new { Name = "Test" }; // Anonymous model is fine
            var badTemplateKey = "This.Template.DoesNotExist";

            // Act & Assert
            // We assert that the call throws the TemplateNotFoundException
            // just like the error you were originally getting.
            await Assert.ThrowsAsync<TemplateNotFoundException>(() =>
                renderer.RenderViewToStringAsync(badTemplateKey, model)
            );
        }
    }
}