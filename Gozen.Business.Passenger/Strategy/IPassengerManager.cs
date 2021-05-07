using System.Collections.Generic;
using System.Threading.Tasks;
using Gozen.Models.DTO;

namespace Gozen.Business.Passenger.Strategy
{
    public interface IPassengerManager
    {
        Task<List<PassengerDto>> ListPassengers();
        Task<PassengerDto> ShowPassengerInfo(int pId);
        Task<bool> AddNewPassenger(PassengerDto p);
        Task<bool> ChangePassengerInfo(PassengerDto p);
        Task<bool> RemovePassenger(int passengerId);

    }
}
