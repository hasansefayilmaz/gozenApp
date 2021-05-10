using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Gozen.Models.DTO;
using Gozen.Web.PassengerApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Gozen.Web.PassengerApp.Controllers
{
    public class PassengerController : Controller
    {
        public PassengerController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // GET: PassengerController
        public async Task<IActionResult> Index()
        {
            try
            {
                HttpClient client = new()
                { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.GetAsync(client.BaseAddress + "api/PassengerDto/");

                if (!response.IsSuccessStatusCode) 
                    return View("Error", new ErrorViewModel { RequestId = string.IsNullOrEmpty(response.ReasonPhrase) ? response.ReasonPhrase : "Service is not reachable !" });

                var result =
                    JsonConvert.DeserializeObject<List<PassengerDto>>(response.Content.ReadAsStringAsync().Result);
                return View(result);
            }
            catch
            {
                return View("Error", new ErrorViewModel {RequestId = "An error occurred during the process !" });
            }
        }

        // GET: PassengerController/Details/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                HttpClient client = new()
                { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.GetAsync(client.BaseAddress + "api/PassengerDto/Details/" + id);

                if (!response.IsSuccessStatusCode) 
                    return View("Error", new ErrorViewModel { RequestId = string.IsNullOrEmpty(response.ReasonPhrase) ? response.ReasonPhrase : "Service is not reachable !" });

                var result = JsonConvert.DeserializeObject<PassengerDto>(response.Content.ReadAsStringAsync().Result);
                return View(result);
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = "An error occurred during the process !" });
            }
        }

        // GET: TestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PassengerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PassengerDto passenger)
        {
            try
            {
                HttpClient client = new()
                { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.PostAsync(client.BaseAddress + "api/PassengerDto/Create/",
                    new StringContent(JsonConvert.SerializeObject(passenger), Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode) 
                    return View("Error", new ErrorViewModel { RequestId = string.IsNullOrEmpty(response.ReasonPhrase) ? response.ReasonPhrase : "Service is not reachable !" });

                var result = response.Content.ReadAsStringAsync().Result;
                var passengers = JsonConvert.DeserializeObject<PassengerDto>(result);

                return View(passengers);
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = "An error occurred during the process !" });
            }
        }

        // GET: PassengerController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                HttpClient client = new()
                { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.GetAsync(client.BaseAddress + "api/PassengerDto/Details/" + id);

                if (!response.IsSuccessStatusCode) 
                    return View("Error", new ErrorViewModel { RequestId = string.IsNullOrEmpty(response.ReasonPhrase) ? response.ReasonPhrase : "Service is not reachable !" });

                var result = JsonConvert.DeserializeObject<PassengerDto>(response.Content.ReadAsStringAsync().Result);
                return View(result);
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = "An error occurred during the process !" });
            }
        }

        // POST: TestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PassengerDto passenger)
        {
            try
            {
                HttpClient client = new()
                { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.PutAsync(client.BaseAddress + "api/PassengerDto/Edit/" + passenger.Id,
                    new StringContent(JsonConvert.SerializeObject(passenger), Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode) 
                    return View("Error", new ErrorViewModel { RequestId = string.IsNullOrEmpty(response.ReasonPhrase) ? response.ReasonPhrase : "Service is not reachable !" });

                var result = response.Content.ReadAsStringAsync().Result;
                var passengers = JsonConvert.DeserializeObject<PassengerDto>(result);

                return View();
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = "An error occurred during the process !" });
            }
        }

        // GET: TestController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                HttpClient client = new()
                { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.GetAsync(client.BaseAddress + "api/PassengerDto/Details/" + id);

                if (!response.IsSuccessStatusCode) 
                    return View("Error", new ErrorViewModel { RequestId = string.IsNullOrEmpty(response.ReasonPhrase) ? response.ReasonPhrase : "Service is not reachable !" });

                var result = JsonConvert.DeserializeObject<PassengerDto>(response.Content.ReadAsStringAsync().Result);
                return View(result);
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = "An error occurred during the process !" });
            }
        }

        // POST: PassengerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection formCollection)
        {
            try
            {
                HttpClient client = new()
                { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.GetAsync(client.BaseAddress + "api/PassengerDto/Delete/" + id);

                if (!response.IsSuccessStatusCode) 
                    return View("Error", new ErrorViewModel { RequestId = string.IsNullOrEmpty(response.ReasonPhrase) ? response.ReasonPhrase : "Service is not reachable !" });

                var result = response.Content.ReadAsStringAsync().Result;
                var passengers = JsonConvert.DeserializeObject<PassengerDto>(result);

                return View(passengers);
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = "An error occurred during the process !" });
            }
        }
    }
}