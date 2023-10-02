using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api;

public class FiltroExcepciones : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        try
        {
            throw context.Exception;
        }
        catch (KeyNotFoundException e)
        {
            context.Result = new JsonResult(e.Message) { StatusCode = 404 };
        }
        catch (Exception e)
        {
            context.Result = new JsonResult(e.Message) { StatusCode = 500 };
        }
    }
}