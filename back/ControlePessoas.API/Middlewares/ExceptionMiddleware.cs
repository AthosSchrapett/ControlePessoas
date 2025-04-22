using ControlePessoas.API.Middlewares.Uteis;
using ControlePessoas.Domain.Exceptions;
using System.Net;

namespace ControlePessoas.API.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = ex switch
        {
            NaoEncontradoException => StatusCodes.Status404NotFound,
            ArgumentNullException => StatusCodes.Status400BadRequest,
            FiltroInvalidoException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError,
        };

        context.Response.ContentType = "application/json";

        ErrorResponse errorResponse = new(context.Response.StatusCode, ex.Message);
        await context.Response.WriteAsync(errorResponse.ToString());
    }
}
