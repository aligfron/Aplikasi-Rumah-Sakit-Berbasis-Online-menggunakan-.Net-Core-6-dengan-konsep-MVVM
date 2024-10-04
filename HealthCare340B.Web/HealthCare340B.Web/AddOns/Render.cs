using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.AddOns
{
    public static class Render
    {
        public static async Task<string> ViewToStringAsync<TModel>(this Controller controller, string viewName, TModel model, bool partial = false)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;
            }

            controller.ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                // Use GetRequiredService to avoid null reference issues
                var viewEngine = controller.HttpContext.RequestServices
                    .GetRequiredService<ICompositeViewEngine>();

                // Find the view, throw an exception if not found
                var viewResult = viewEngine.FindView(controller.ControllerContext, viewName, !partial);
                if (!viewResult.Success)
                {
                    // Log the error and throw an exception
                    throw new InvalidOperationException($"View '{viewName}' not found.");
                }

                // Set up the ViewContext and render the view to the StringWriter
                var viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
