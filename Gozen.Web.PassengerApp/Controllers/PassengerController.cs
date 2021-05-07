using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Gozen.Models.DTO;
using Gozen.Web.PassengerApp.Models;
using Microsoft.Extensions.Configuration;

namespace Gozen.Web.PassengerApp.Controllers
{
    public class PassengerController : Controller
    {
        private IConfiguration Configuration { get; }
        public PassengerController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // GET: PassengerController
        public async Task<IActionResult> Index()
        {
            try
            {
                HttpClient client = new() { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.GetAsync(client.BaseAddress + "api/Passenger/");

                if (!response.IsSuccessStatusCode) return View();

                var result = JsonConvert.DeserializeObject<List<PassengerDto>>(response.Content.ReadAsStringAsync().Result);
                return View(result);
            }
            catch
            {
                return View("Error", new ErrorViewModel());
            }
        }

        // GET: PassengerController/Details/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                HttpClient client = new() { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.GetAsync(client.BaseAddress + "api/Passenger/Details/" + id);

                if (!response.IsSuccessStatusCode) return View();

                var result = JsonConvert.DeserializeObject<PassengerDto>(response.Content.ReadAsStringAsync().Result);
                return View(result);
            }
            catch
            {
                return View("Error", new ErrorViewModel());
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
                HttpClient client = new() { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.PostAsync(client.BaseAddress + "api/Passenger/Create/", new StringContent(JsonConvert.SerializeObject(passenger), Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode) return View();

                var result = response.Content.ReadAsStringAsync().Result;
                var passengers = JsonConvert.DeserializeObject<PassengerDto>(result);

                return View(passengers);
            }
            catch
            {
                return View("Error", new ErrorViewModel());
            }
        }

        // GET: PassengerController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                HttpClient client = new() { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.GetAsync(client.BaseAddress + "api/Passenger/Details/" + id);

                if (!response.IsSuccessStatusCode) return View();

                var result = JsonConvert.DeserializeObject<PassengerDto>(response.Content.ReadAsStringAsync().Result);
                return View(result);
            }
            catch
            {
                return View("Error", new ErrorViewModel());
            }
        }

        // POST: TestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PassengerDto passenger)
        {
            try
            {

                HttpClient client = new() { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.PutAsync(client.BaseAddress + "api/Passenger/Edit/" + passenger.Id, new StringContent(JsonConvert.SerializeObject(passenger), Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode) return View();

                var result = response.Content.ReadAsStringAsync().Result;
                var passengers = JsonConvert.DeserializeObject<PassengerDto>(result);

                return View();
            }
            catch
            {
                return View("Error", new ErrorViewModel());
            }
        }

        // GET: TestController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                HttpClient client = new() { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.GetAsync(client.BaseAddress + "api/Passenger/Details/" + id);

                if (!response.IsSuccessStatusCode) return View();

                var result = JsonConvert.DeserializeObject<PassengerDto>(response.Content.ReadAsStringAsync().Result);
                return View(result);
            }
            catch
            {
                return View("Error", new ErrorViewModel());
            }
        }

        // POST: PassengerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection formCollection)
        {
            try
            {
                HttpClient client = new() { BaseAddress = new Uri(Configuration.GetConnectionString("DefaultConnection")) };
                var response = await client.GetAsync(client.BaseAddress + "api/Passenger/Delete/" + id);

                if (!response.IsSuccessStatusCode) return View();

                var result = response.Content.ReadAsStringAsync().Result;
                var passengers = JsonConvert.DeserializeObject<PassengerDto>(result);

                return View(passengers);
            }
            catch
            {
                return View("Error", new ErrorViewModel());
            }
        }
    }
}
