using Gozen.Business.Passenger;
using Gozen.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Gozen.Business.Passenger.Concreates;
using Gozen.Data.Repositories;
using Gozen.Models.DTO.Enums;
using Gozen.Service.PassengerApi.Helpers;

namespace Gozen.Service.PassengerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ModelValidation]
    public class PassengerController : Controller
    {
        private readonly ILogger<PassengerController> _logger;
        private readonly IPassengerRepository _passengerRepository;
        private IPassengerOperation _passengerOperation;
        public PassengerController(ILogger<PassengerController> logger, IPassengerRepository passengerRepository)
        {
            _logger = logger;
            _passengerRepository = passengerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int scenario = 1)
        {

            if (scenario == (int)ProjectScenerio.Online)
            {
                try
                {
                    _passengerOperation = new PassengerOperation(new OnlinePassengerManager(_passengerRepository));
                    return Ok(await _passengerOperation.ListPassengers());
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message, ex);
                    return BadRequest(ex.Message);
                }
            }
            else
            {

                try
                {
                    return Ok(await _passengerOperation.ListPassengers());
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message, ex);
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                _passengerOperation = new PassengerOperation(new OnlinePassengerManager(_passengerRepository));
                return Ok(await _passengerOperation.ShowPassengerInfo(id));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] PassengerDto obj)
        {
            try
            {
                _passengerOperation = new PassengerOperation(new OnlinePassengerManager(_passengerRepository));
                return Ok(await _passengerOperation.AddNewPassenger(obj));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromBody] PassengerDto obj)
        {
            try
            {
                _passengerOperation = new PassengerOperation(new OnlinePassengerManager(_passengerRepository));
                return Ok(await _passengerOperation.ChangePassengerInfo(obj));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _passengerOperation = new PassengerOperation(new OnlinePassengerManager(_passengerRepository));
                return Ok(await _passengerOperation.RemovePassenger(id));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
