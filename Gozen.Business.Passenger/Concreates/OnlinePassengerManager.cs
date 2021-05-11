#region Usings

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gozen.Business.Passenger.Strategy;
using Gozen.Data.Repositories;
using Gozen.Models.DTO;
using Mapster;

#endregion

namespace Gozen.Business.Passenger.Concreates
{
    public class OnlinePassengerManager : IPassengerManager
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IPassengerRepository _passengerRepository;

        public OnlinePassengerManager(IPassengerRepository passengerRepository,
            IDocumentTypeRepository documentTypeRepository)
        {
            _passengerRepository = passengerRepository;
            _documentTypeRepository = documentTypeRepository;
        }

        public async Task<List<PassengerDto>> ListPassengers()
        {
            try
            {
                var passengers = await _passengerRepository.GetAllAsync();
                List<PassengerDto> result = new();
                if (passengers != null)
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

        public async Task<PassengerDto> ShowPassengerInfo(int id)
        {
            try
            {
                var passenger = await _passengerRepository.GetByIdAsync(id);
                if (passenger != null) return passenger.Adapt(new PassengerDto());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return null;
        }

        public async Task<bool> AddNewPassenger(PassengerDto p)
        {
            try
            {
                var newPassenger = p.Adapt(new Data.Entity.Passenger());
                return newPassenger != null && await _passengerRepository.CreateAsync(newPassenger);
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
                    return updatedPassenger != null && await _passengerRepository.UpdateAsync(updatedPassenger);
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> RemovePassenger(int id)
        {
            try
            {
                var passenger = await _passengerRepository.GetByIdAsync(id);
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

        public async Task<List<DocumentTypeDto>> GetDocumentTypes()
        {
            try
            {
                var documentTypes = await _documentTypeRepository.GetAllAsync();
                List<DocumentTypeDto> result = new();
                if (documentTypes != null)
                    foreach (var documentType in documentTypes)
                    {
                        var documentTypeDto = documentType.Adapt(new DocumentTypeDto());
                        result.Add(documentTypeDto);
                    }

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}