 using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }
        
        [Route("Error/{statuscode}")]
        public IActionResult Index(int statuscode)
        {
            //var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            //switch (statuscode){
            //    case 404:
            //        ViewBag.ErrorMessage = "Sorry, resource you requested could not be found."  ;
            //        logger.LogWarning($"404 error occured path = " + $"{statusCodeResult.OriginalPath} and querystring ="
            //            + $"{statusCodeResult.OriginalQueryString}");
            //        break;

            //}
            return View("NotFound");
        }
        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            //var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ////ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            ////ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            ////ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;
            //logger.LogError($"The path {exceptionHandlerPathFeature.Path}" + $" threw an exception {exceptionHandlerPathFeature.Error}");

            return View("Error");
        }
    }
}