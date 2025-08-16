using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Exceptions;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException( ExceptionContext context )
    {
        int statusCode = context.Exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,

            ValidationException => StatusCodes.Status400BadRequest,

            _ => StatusCodes.Status500InternalServerError
        };

        context.Result = new ObjectResult( new
        {
            error = context.Exception.Message,
        } )
        {
            StatusCode = statusCode
        };
    }
}