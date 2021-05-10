using System;
using System.Threading.Tasks;
using Gozen.Business.Passenger;
using Gozen.Business.Passenger.Concreates;
using Gozen.Data.Repositories;
using Gozen.Models.DTO;
using Gozen.Models.DTO.Enums;
using Gozen.Service.PassengerApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Gozen.Service.PassengerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ModelValidation]
    public class PassengerController : Controller
    {
        private readonly ILogger<PassengerController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IPassengerRepository _passengerRepository;

        private IPassengerOperation _passengerOperation;

        public PassengerController(IPassengerRepository passengerRepository, IMemoryCache memoryCache,
            ILogger<PassengerController> logger)
        {
            _passengerRepository = passengerRepository;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        [HttpGet("{scenario:int}")]
        public async Task<IActionResult> Index(int scenario = 1)
        {
            switch (scenario)
            {
                case (int)Scenerio.Online:

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
                case (int)Scenerio.Offline:
                    try
                    {
                        _passengerOperation = new PassengerOperation(new OfflinePassengerManager(_memoryCache));
                        return Ok(await _passengerOperation.ListPassengers());
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical(ex.Message, ex);
                        return BadRequest(ex.Message);
                    }
                default:
                    return Ok();
            }
        }

        [HttpGet("{scenario:int}/{id:int}")]
        public async Task<IActionResult> Details(int id, int scenario = 1)
        {
            switch (scenario)
            {
                case (int)Scenerio.Online:

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
                case (int)Scenerio.Offline:
                    try
                    {
                        _passengerOperation = new PassengerOperation(new OfflinePassengerManager(_memoryCache));
                        return Ok(await _passengerOperation.ShowPassengerInfo(id));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical(ex.Message, ex);
                        return BadRequest(ex.Message);
                    }
                default:
                    return Ok();
            }
        }

        [HttpPost("{scenario:int}")]
        public async Task<IActionResult> Create([FromBody] PassengerDto obj, int scenario = 1)
        {
            switch (scenario)
            {
                case (int)Scenerio.Online:

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
                case (int)Scenerio.Offline:
                    try
                    {
                        _passengerOperation = new PassengerOperation(new OfflinePassengerManager(_memoryCache));
                        return Ok(await _passengerOperation.AddNewPassenger(obj));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical(ex.Message, ex);
                        return BadRequest(ex.Message);
                    }
                default:
                    return Ok();
            }
        }

        [HttpPut("{scenario:int}/{id:int}")]
        public async Task<IActionResult> Edit([FromBody] PassengerDto obj, int scenario = 1)
        {
            switch (scenario)
            {
                case (int)Scenerio.Online:

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
                case (int)Scenerio.Offline:
                    try
                    {
                        _passengerOperation = new PassengerOperation(new OfflinePassengerManager(_memoryCache));
                        return Ok(await _passengerOperation.ChangePassengerInfo(obj));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical(ex.Message, ex);
                        return BadRequest(ex.Message);
                    }
                default:
                    return Ok();
            }
        }

        [HttpDelete("{scenario:int}/{id:int}")]
        public async Task<IActionResult> Delete(int id, int scenario = 1)
        {
            switch (scenario)
            {
                case (int)Scenerio.Online:

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
                case (int)Scenerio.Offline:
                    try
                    {
                        _passengerOperation = new PassengerOperation(new OfflinePassengerManager(_memoryCache));
                        return Ok(await _passengerOperation.RemovePassenger(id));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical(ex.Message, ex);
                        return BadRequest(ex.Message);
                    }
                default:
                    return Ok();
            }
        }
    }
}