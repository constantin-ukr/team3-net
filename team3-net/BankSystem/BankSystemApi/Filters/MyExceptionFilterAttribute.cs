using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;


namespace BankSystemApi.Filters
{
    public class MyExceptionFilterAttribute:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if(context.Exception.GetType()==typeof(ArgumentNullException))
            {

                context.Result = new NotFoundResult();
            }
            else
            {
                context.Result = new BadRequestResult();
            }
            context.ExceptionHandled = true;
        }
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(ArgumentNullException))
            {

                context.Result = new NotFoundResult();
            }
            else
            {
                context.Result = new BadRequestResult();
            }
            context.ExceptionHandled = true;
        }
    }
}
