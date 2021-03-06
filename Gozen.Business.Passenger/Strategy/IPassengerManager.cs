#region Usings

using System.Collections.Generic;
using System.Threading.Tasks;
using Gozen.Models.DTO;

#endregion

namespace Gozen.Business.Passenger.Strategy
{
    public interface IPassengerManager
    {
        Task<List<PassengerDto>> ListPassengers();
        Task<PassengerDto> ShowPassengerInfo(int id);
        Task<bool> AddNewPassenger(PassengerDto p);
        Task<bool> ChangePassengerInfo(PassengerDto p);
        Task<bool> RemovePassenger(int id);

        Task<List<DocumentTypeDto>> GetDocumentTypes();
    }
}