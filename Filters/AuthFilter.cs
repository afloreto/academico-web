using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Academico.Filters;

public class AuthFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var ignorar = context.ActionDescriptor.EndpointMetadata
            .OfType<IgnoreAuthFilterAttribute>()
            .Any();

        if (ignorar) return;

        var usuario = context.HttpContext.Session.GetString("UsuarioNome");

        if (string.IsNullOrEmpty(usuario))
        {
            context.Result = new RedirectToActionResult("Login", "Auth", null);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}