#region Usings

using System;
using System.Threading.Tasks;
using Gozen.Business.Passenger;
using Gozen.Business.Passenger.Concreates;
using Gozen.Data.Repositories;
using Gozen.Models.DTO;
using Gozen.Models.DTO.Statics;
using Gozen.Service.PassengerApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

#endregion

namespace Gozen.Service.PassengerApi.Controllers
{
    [ApiController]
    [Route("api/{scenario}/[controller]/[action]")]
    [ModelValidation]
    public class PassengerController : Controller
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly ILogger<PassengerController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IPassengerRepository _passengerRepository;

        private IPassengerOperation _passengerOperation;

        public PassengerController(IPassengerRepository passengerRepository,
            IDocumentTypeRepository documentTypeRepository, IMemoryCache memoryCache,
            ILogger<PassengerController> logger)
        {
            _passengerRepository = passengerRepository;
            _documentTypeRepository = documentTypeRepository;
            _memoryCache = memoryCache;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string scenario)
        {
            switch (scenario)
            {
                case Scenario.Online:

                    try
                    {
                        _passengerOperation =
                            new PassengerOperation(new OnlinePassengerManager(_passengerRepository,
                                _documentTypeRepository));
                        return Ok(await _passengerOperation.ListPassengers());
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical(ex.Message, ex);
                        return BadRequest(ex.Message);
                    }
                case Scenario.Offline:
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id, string scenario)
        {
            switch (scenario)
            {
                case Scenario.Online:

                    try
                    {
                        _passengerOperation =
                            new PassengerOperation(new OnlinePassengerManager(_passengerRepository,
                                _documentTypeRepository));
                        return Ok(await _passengerOperation.ShowPassengerInfo(id));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical(ex.Message, ex);
                        return BadRequest(ex.Message);
                    }
                case Scenario.Offline:
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PassengerDto obj, string scenario)
        {
            switch (scenario)
            {
                case Scenario.Online:

                    try
                    {
                        _passengerOperation =
                            new PassengerOperation(new OnlinePassengerManager(_passengerRepository,
                                _documentTypeRepository));
                        return Ok(await _passengerOperation.AddNewPassenger(obj));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical(ex.Message, ex);
                        return BadRequest(ex.Message);
                    }
                case Scenario.Offline:
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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] PassengerDto obj, string scenario)
        {
            switch (scenario)
            {
                case Scenario.Online:

                    try
                    {
                        _passengerOperation =
                            new PassengerOperation(new OnlinePassengerManager(_passengerRepository,
                                _documentTypeRepository));
                        return Ok(await _passengerOperation.ChangePassengerInfo(obj));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical(ex.Message, ex);
                        return BadRequest(ex.Message);
                    }
                case Scenario.Offline:
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, string scenario)
        {
            switch (scenario)
            {
                case Scenario.Online:

                    try
                    {
                        _passengerOperation =
                            new PassengerOperation(new OnlinePassengerManager(_passengerRepository,
                                _documentTypeRepository));
                        return Ok(await _passengerOperation.RemovePassenger(id));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical(ex.Message, ex);
                        return BadRequest(ex.Message);
                    }
                case Scenario.Offline:
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

        [HttpGet]
        public async Task<IActionResult> GetDocumentTypes(string scenario)
        {
            switch (scenario)
            {
                case Scenario.Online:

                    try
                    {
                        _passengerOperation =
                            new PassengerOperation(new OnlinePassengerManager(_passengerRepository,
                                _documentTypeRepository));
                        return Ok(await _passengerOperation.GetDocumentTypes());
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical(ex.Message, ex);
                        return BadRequest(ex.Message);
                    }
                case Scenario.Offline:
                    try
                    {
                        _passengerOperation = new PassengerOperation(new OfflinePassengerManager(_memoryCache));
                        return Ok(await _passengerOperation.GetDocumentTypes());
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