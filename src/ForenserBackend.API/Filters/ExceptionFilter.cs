using ForenserBackend.Communication.Errors;
using ForenserBackend.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ForenserBackend.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {

        // realiza chamada ao receber uma excessão
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ForenserException)
            {
                // para erros tipados
                ThrowNewException(context);
            }
            else
            {
                // erros não tipados
                ThrowUnknownException(context);
            }
        }

        private void ThrowNewException(ExceptionContext context)
        {
            var forenserException = (ForenserException)context.Exception;
            context.HttpContext.Response.StatusCode = forenserException.statusCode;
            context.Result = new ObjectResult(forenserException.getErrors());


        }
        private void ThrowUnknownException(ExceptionContext context)
        {
            var errorRespone = new ErrorMessageJson("Unknown Exception");
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorRespone);
        }
    }
}
