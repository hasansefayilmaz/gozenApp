using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Gozen.Models.DTO;
using Gozen.Models.DTO.Enums;
using Gozen.Web.PassengerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Gozen.Web.PassengerApp.Controllers
{
    [Route("{scenario}/[controller]/[action]")]
    public class PassengerController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public PassengerController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            _logger = logger;
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // GET: PassengerController
        [HttpGet("", Name = "Index")]
        public async Task<IActionResult> Index(string scenario)
        {
            try
            {
                HttpClient client = new() { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };

                var response = await client.GetAsync(client.BaseAddress + "api/" + scenario + "/Passenger/Index/");
                if (!response.IsSuccessStatusCode)
                    return View("Error", new ErrorViewModel
                    {
                        ErrorCode = (int)response.StatusCode + " " + response.ReasonPhrase,
                        ErrorMessage = response.RequestMessage != null ? response.RequestMessage.ToString() : "No message delivered by the service."
                    });

                ViewBag.Scenario = scenario;

                var result = JsonConvert.DeserializeObject<List<PassengerDto>>(response.Content.ReadAsStringAsync().Result);
                return View(result);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception.Message, exception);
                return View("Error", new ErrorViewModel { ErrorMessage = exception.Message });
            }
        }

        // GET: PassengerController/Details/5
        [HttpGet("{id:int}", Name = "Details")]
        public async Task<IActionResult> Details(int id, string scenario)
        {
            try
            {
                HttpClient client = new() { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.GetAsync(client.BaseAddress + "api/" + scenario + "/Passenger/Details/" + id);

                if (!response.IsSuccessStatusCode)
                    return View("Error", new ErrorViewModel
                    {
                        ErrorCode = (int)response.StatusCode + " " + response.ReasonPhrase,
                        ErrorMessage = response.RequestMessage != null ? response.RequestMessage.ToString() : "No message delivered by the service."
                    });

                ViewBag.Scenario = scenario;

                var result = JsonConvert.DeserializeObject<PassengerDto>(response.Content.ReadAsStringAsync().Result);
                return View(result);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception.Message, exception);
                return View("Error", new ErrorViewModel { ErrorMessage = exception.Message });
            }
        }

        // GET: TestController/Create
        [HttpGet("", Name = "Create")]
        public async Task<IActionResult> Create(string scenario)
        {
            try
            {
                HttpClient client = new() { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };

                var response = await client.GetAsync(client.BaseAddress + "api/" + scenario + "/Passenger/GetDocumentTypes/");
                if (!response.IsSuccessStatusCode)
                    return View("Error", new ErrorViewModel
                    {
                        ErrorCode = (int)response.StatusCode + " " + response.ReasonPhrase,
                        ErrorMessage = response.RequestMessage != null ? response.RequestMessage.ToString() : "No message delivered by the service."
                    });

                var dResponse = await client.GetAsync(client.BaseAddress + "api/" + scenario + "/Passenger/GetDocumentTypes/");
                if (!dResponse.IsSuccessStatusCode)
                    return View("Error", new ErrorViewModel
                    {
                        ErrorCode = (int)dResponse.StatusCode + " " + dResponse.ReasonPhrase,
                        ErrorMessage = dResponse.RequestMessage != null ? dResponse.RequestMessage.ToString() : "No message delivered by the service."
                    });

                ViewBag.DocumentTypes = JsonConvert.DeserializeObject<List<DocumentTypeDto>>(dResponse.Content.ReadAsStringAsync().Result);
                ViewBag.Scenario = scenario;

                return View();
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception.Message, exception);
                return View("Error", new ErrorViewModel { ErrorMessage = exception.Message });
            }
        }

        // POST: PassengerController/Create
        [HttpPost("")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PassengerDto passenger, string scenario)
        {
            try
            {
                HttpClient client = new() { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };

                var response = await client.PostAsync(client.BaseAddress + "api/" + scenario + "/Passenger/Create/",
                    new StringContent(JsonConvert.SerializeObject(passenger), Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode)
                    return View("Error", new ErrorViewModel
                    {
                        ErrorCode = (int)response.StatusCode + " " + response.ReasonPhrase,
                        ErrorMessage = response.RequestMessage != null ? response.RequestMessage.ToString() : "No message delivered by the service."
                    });

                var result = Convert.ToBoolean(JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result));
                return result ? RedirectToAction("Index", "Passenger", new { scenario = scenario }) : View(passenger);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception.Message, exception);
                return View("Error", new ErrorViewModel { ErrorMessage = exception.Message });
            }
        }

        // GET: PassengerController/Edit/5
        [HttpGet("{id:int}", Name = "Edit")]
        public async Task<IActionResult> Edit(int id, string scenario)
        {
            try
            {
                HttpClient client = new() { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };

                var response = await client.GetAsync(client.BaseAddress + "api/" + scenario + "/Passenger/Details/" + id);
                if (!response.IsSuccessStatusCode)
                    return View("Error", new ErrorViewModel
                    {
                        ErrorCode = (int)response.StatusCode + " " + response.ReasonPhrase,
                        ErrorMessage = response.RequestMessage != null ? response.RequestMessage.ToString() : "No message delivered by the service."
                    });

                var dResponse = await client.GetAsync(client.BaseAddress + "api/" + scenario + "/Passenger/GetDocumentTypes/");
                if (!dResponse.IsSuccessStatusCode)
                    return View("Error", new ErrorViewModel
                    {
                        ErrorCode = (int)dResponse.StatusCode + " " + dResponse.ReasonPhrase,
                        ErrorMessage = dResponse.RequestMessage != null ? dResponse.RequestMessage.ToString() : "No message delivered by the service."
                    });

                ViewBag.DocumentTypes = JsonConvert.DeserializeObject<List<DocumentTypeDto>>(dResponse.Content.ReadAsStringAsync().Result);
                ViewBag.Scenario = scenario;

                var result = JsonConvert.DeserializeObject<PassengerDto>(response.Content.ReadAsStringAsync().Result);

                return View(result);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception.Message, exception);
                return View("Error", new ErrorViewModel { ErrorMessage = exception.Message });
            }
        }

        // POST: PassengerController/Edit/5
        [HttpPost("{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PassengerDto passenger, string scenario)
        {
            try
            {
                HttpClient client = new() { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };

                var response = await client.PutAsync(client.BaseAddress + "api/" + scenario + "/Passenger/Edit/" + passenger.Id,
                    new StringContent(JsonConvert.SerializeObject(passenger), Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode)
                    return View("Error", new ErrorViewModel
                    {
                        ErrorCode = (int)response.StatusCode + " " + response.ReasonPhrase,
                        ErrorMessage = response.RequestMessage != null ? response.RequestMessage.ToString() : "No message delivered by the service."
                    });

                var result = Convert.ToBoolean(JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result));
                return result ? RedirectToAction("Index", "Passenger", new { scenario = scenario }) : View(passenger);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception.Message, exception);
                return View("Error", new ErrorViewModel { ErrorMessage = exception.Message });
            }
        }

        // GET: PassengerController/Delete/5
        [HttpGet("{id:int}", Name = "Delete")]
        public async Task<IActionResult> Delete(int id, string scenario)
        {
            try
            {

                HttpClient client = new() { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.GetAsync(client.BaseAddress + "api/" + scenario + "/Passenger/Details/" + id);

                if (!response.IsSuccessStatusCode)
                    return View("Error", new ErrorViewModel
                    {
                        ErrorCode = (int)response.StatusCode + " " + response.ReasonPhrase,
                        ErrorMessage = response.RequestMessage != null ? response.RequestMessage.ToString() : "No message delivered by the service."
                    });

                ViewBag.Scenario = scenario;

                var result = JsonConvert.DeserializeObject<PassengerDto>(response.Content.ReadAsStringAsync().Result);
                return View(result);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception.Message, exception);
                return View("Error", new ErrorViewModel { ErrorMessage = exception.Message });
            }
        }

        // GET: PassengerController/Delete/5
        [HttpGet("{id:int}", Name = "DeleteOk")]
        public async Task<IActionResult> DeleteOk(int id, string scenario)
        {
            try
            {
                HttpClient client = new() { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.DeleteAsync(client.BaseAddress + "api/" + scenario + "/Passenger/Delete/" + id);

                if (!response.IsSuccessStatusCode)
                    return View("Error", new ErrorViewModel
                    {
                        ErrorCode = (int)response.StatusCode + " " + response.ReasonPhrase,
                        ErrorMessage = response.RequestMessage != null ? response.RequestMessage.ToString() : "No message delivered by the service."
                    });

                var result = Convert.ToBoolean(JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result));
                return result
                    ? RedirectToAction("Index", "Passenger", new { scenario = scenario })
                    : RedirectToAction("Delete", "Passenger", new { scenario = scenario, id = id });
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception.Message, exception);
                return View("Error", new ErrorViewModel { ErrorMessage = exception.Message });
            }
        }
    }
}