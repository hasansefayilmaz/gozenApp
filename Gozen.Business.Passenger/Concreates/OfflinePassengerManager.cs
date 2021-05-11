#region Usings

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gozen.Business.Passenger.Strategy;
using Gozen.Models.DTO;
using Mapster;
using Microsoft.Extensions.Caching.Memory;

#endregion

namespace Gozen.Business.Passenger.Concreates
{
    public class OfflinePassengerManager : IPassengerManager
    {
        private const string PassengerKey = "passenger";
        private const string DocumentTypeKey = "DocumentTypeDto";
        private readonly IMemoryCache _memoryCache;

        public OfflinePassengerManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;

            #region Dummy Data

            #region DocumentTypeDto

            if (!_memoryCache.TryGetValue(DocumentTypeKey, out List<DocumentTypeDto> dCache))
            {
                dCache = new List<DocumentTypeDto>()
                {
                    new DocumentTypeDto {Id = 1, Type = "Passport", IssueDate = DateTime.UtcNow, IsActive = true},
                    new DocumentTypeDto {Id = 2, Type = "Visa", IssueDate = DateTime.UtcNow, IsActive = true},
                    new DocumentTypeDto {Id = 3, Type = "Travel", IssueDate = DateTime.UtcNow, IsActive = true}
                };
                _memoryCache.Set(DocumentTypeKey, dCache,
                    new MemoryCacheEntryOptions
                        {Priority = CacheItemPriority.NeverRemove, AbsoluteExpiration = DateTime.Now.AddMinutes(30)});
            }

            #endregion

            #region PassengerDto

            if (!_memoryCache.TryGetValue(PassengerKey, out List<PassengerDto> pCache))
            {
                pCache = new List<PassengerDto>()
                {
                    new PassengerDto
                    {
                        Id = 1, Name = "Ofline_01", Surname = "Surname_01", Gender = 0, DocumentTypeId = 1,
                        DocumentNumber = 1111, IssueDate = DateTime.UtcNow, IsActive = true
                    },
                    new PassengerDto
                    {
                        Id = 2, Name = "Ofline_02", Surname = "Surname_01", Gender = 0, DocumentTypeId = 1,
                        DocumentNumber = 1111, IssueDate = DateTime.UtcNow, IsActive = true
                    },
                    new PassengerDto
                    {
                        Id = 3, Name = "Ofline_03", Surname = "Surname_01", Gender = 0, DocumentTypeId = 1,
                        DocumentNumber = 1111, IssueDate = DateTime.UtcNow, IsActive = true
                    },
                    new PassengerDto
                    {
                        Id = 4, Name = "Ofline_04", Surname = "Surname_01", Gender = 0, DocumentTypeId = 1,
                        DocumentNumber = 1111, IssueDate = DateTime.UtcNow, IsActive = true
                    }
                };
                _memoryCache.Set(PassengerKey, pCache,
                    new MemoryCacheEntryOptions {Priority = CacheItemPriority.NeverRemove});

                foreach (var dummyPassenger in pCache)
                    dummyPassenger.DocumentType = dCache.Find(c => c.Id == dummyPassenger.DocumentTypeId);
            }

            #endregion

            #endregion
        }

        public async Task<List<PassengerDto>> ListPassengers()
        {
            try
            {
                _memoryCache.TryGetValue(PassengerKey, out List<PassengerDto> pCache);
                _memoryCache.TryGetValue(DocumentTypeKey, out List<DocumentTypeDto> dCache);

                var passengers = pCache.FindAll(x => x.IsActive);
                if (passengers != null)
                    foreach (var passenger in passengers)
                    {
                        var passengerDocumentType = dCache.Find(c => c.Id == passenger.DocumentTypeId);
                        if (passengerDocumentType != null) passenger.DocumentType = passengerDocumentType;
                    }

                return passengers;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            finally
            {
                await Task.Delay(new TimeSpan(0));
            }
        }

        public async Task<PassengerDto> ShowPassengerInfo(int id)
        {
            try
            {
                _memoryCache.TryGetValue(PassengerKey, out List<PassengerDto> pCache);
                _memoryCache.TryGetValue(DocumentTypeKey, out List<DocumentTypeDto> dCache);

                var passenger = pCache.Find(c => c.Id == id && c.IsActive);
                if (passenger != null)
                {
                    var passengerDocumentType = dCache.Find(c => c.Id == passenger.DocumentTypeId);
                    if (passengerDocumentType != null) passenger.DocumentType = passengerDocumentType;
                }

                return passenger;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            finally
            {
                await Task.Delay(new TimeSpan(0));
            }
        }

        public async Task<bool> AddNewPassenger(PassengerDto p)
        {
            try
            {
                _memoryCache.TryGetValue(PassengerKey, out List<PassengerDto> pCache);
                _memoryCache.TryGetValue(DocumentTypeKey, out List<DocumentTypeDto> dCache);

                var documentType = dCache.Find(c => c.Id == p.DocumentTypeId);
                if (documentType != null) p.DocumentType = documentType;
                p.Id = pCache.Count + 1;
                p.IssueDate = DateTime.UtcNow;
                p.IsActive = true;
                pCache.Add(p);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                await Task.Delay(new TimeSpan(0));
            }
        }

        public async Task<bool> ChangePassengerInfo(PassengerDto p)
        {
            try
            {
                _memoryCache.TryGetValue(PassengerKey, out List<PassengerDto> pCache);
                _memoryCache.TryGetValue(DocumentTypeKey, out List<DocumentTypeDto> dCache);

                var passenger = pCache.Find(c => c.Id == p.Id);
                if (passenger != null)
                {
                    p.Adapt(passenger);
                    passenger.DocumentType = dCache.Find(c => c.Id == p.DocumentTypeId);
                    passenger.IssueDate = DateTime.UtcNow;
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                await Task.Delay(new TimeSpan(0));
            }
        }

        public async Task<bool> RemovePassenger(int id)
        {
            try
            {
                _memoryCache.TryGetValue(PassengerKey, out List<PassengerDto> pCache);
                var passenger = pCache.Find(c => c.Id == id);
                if (passenger != null)
                {
                    passenger.IssueDate = DateTime.UtcNow;
                    passenger.IsActive = false;
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                await Task.Delay(new TimeSpan(0));
            }
        }

        public async Task<List<DocumentTypeDto>> GetDocumentTypes()
        {
            try
            {
                _memoryCache.TryGetValue(DocumentTypeKey, out List<DocumentTypeDto> dCache);
                var documentTypes = dCache.FindAll(x => x.IsActive);
                return documentTypes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            finally
            {
                await Task.Delay(new TimeSpan(0));
            }
        }
    }
}