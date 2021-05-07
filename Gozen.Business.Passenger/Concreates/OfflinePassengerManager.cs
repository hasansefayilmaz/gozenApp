using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gozen.Business.Passenger.Strategy;
using Gozen.Models.DTO;

namespace Gozen.Business.Passenger.Concreates
{
    public class OfflinePassengerManager : IPassengerManager
    {
        public OfflinePassengerManager()
        {
            //_passengerRepository = passengerRepository;
        }

        public Task<bool> AddNewPassenger(PassengerDto p)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePassengerInfo(PassengerDto p)
        {
            throw new NotImplementedException();
        }

        public Task<List<PassengerDto>> ListPassengers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemovePassenger(int passengerId)
        {
            throw new NotImplementedException();
        }

        public Task<PassengerDto> ShowPassengerInfo(int pId)
        {
            throw new NotImplementedException();
        }
    }
}
