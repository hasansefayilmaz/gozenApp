using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gozen.Business.Passenger.Strategy;
using Gozen.Data.Repositories;
using Gozen.Models.DTO;
using Mapster;

namespace Gozen.Business.Passenger.Concreates
{
    public class OnlinePassengerManager : IPassengerManager
    {
        private readonly IPassengerRepository _passengerRepository;
        public OnlinePassengerManager(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;

        }

        public async Task<List<PassengerDto>> ListPassengers()
        {
            try
            {
                var passengers = await _passengerRepository.GetAllAsync();
                List<PassengerDto> result = new();
                foreach (var passenger in passengers)
                {
                    var passengerDto = passenger.Adapt(new PassengerDto());
                    result.Add(passengerDto);
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<PassengerDto> ShowPassengerInfo(int pId)
        {
            var passenger = await _passengerRepository.GetByIdAsync(pId);
            return passenger.Adapt(new PassengerDto());
        }

        public async Task<bool> AddNewPassenger(PassengerDto p)
        {
            try
            {
                var newPassenger = p.Adapt(new Data.Entity.Passenger());
                return await _passengerRepository.CreateAsync(newPassenger);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> ChangePassengerInfo(PassengerDto p)
        {
            try
            {
                var passenger = await _passengerRepository.GetByIdAsync(p.Id);
                if (passenger != null)
                {
                    var updatedPassenger = p.Adapt(passenger);
                    return await _passengerRepository.UpdateAsync(updatedPassenger);
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> RemovePassenger(int passengerId)
        {
            try
            {
                var passenger = await _passengerRepository.GetByIdAsync(passengerId);
                if (passenger != null)
                {
                    passenger.IsActive = false;
                    return await _passengerRepository.UpdateAsync(passenger);
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
