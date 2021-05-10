using System.Collections.Generic;
using System.Threading.Tasks;
using Gozen.Business.Passenger.Strategy;
using Gozen.Models.DTO;

namespace Gozen.Business.Passenger
{
    public interface IPassengerOperation
    {
        Task<List<PassengerDto>> ListPassengers();
        Task<PassengerDto> ShowPassengerInfo(int passengerId);
        Task<bool> AddNewPassenger(PassengerDto obj);
        Task<bool> ChangePassengerInfo(PassengerDto obj);
        Task<bool> RemovePassenger(int passengerId);
    }


    public class PassengerOperation : IPassengerOperation
    {
        private readonly IPassengerManager _passengerManager;

        public PassengerOperation(IPassengerManager passengerManager)
        {
            _passengerManager = passengerManager;
        }

        public async Task<List<PassengerDto>> ListPassengers()
        {
            return await _passengerManager.ListPassengers();
        }

        public async Task<PassengerDto> ShowPassengerInfo(int passengerId)
        {
            return await _passengerManager.ShowPassengerInfo(passengerId);
        }

        public async Task<bool> AddNewPassenger(PassengerDto obj)
        {
            return await _passengerManager.AddNewPassenger(obj);
        }

        public async Task<bool> ChangePassengerInfo(PassengerDto obj)
        {
            return await _passengerManager.ChangePassengerInfo(obj);
        }

        public async Task<bool> RemovePassenger(int passengerId)
        {
            return await _passengerManager.RemovePassenger(passengerId);
        }
    }
}