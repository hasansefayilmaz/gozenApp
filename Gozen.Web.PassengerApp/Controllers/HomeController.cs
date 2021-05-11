#region Usings

using System;
using System.Diagnostics;
using Gozen.Web.PassengerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace Gozen.Web.PassengerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception.Message, exception);
                return BadRequest(exception.Message);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            try
            {
                return View(new ErrorViewModel
                    {ErrorMessage = Activity.Current?.DisplayName ?? HttpContext.TraceIdentifier});
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception.Message, exception);
                return BadRequest(exception.Message);
            }
        }
    }
}