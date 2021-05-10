using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gozen.Business.Passenger.Strategy;
using Gozen.Data.Entity;
using Gozen.Models.DTO;
using Mapster;
using Microsoft.Extensions.Caching.Memory;

namespace Gozen.Business.Passenger.Concreates
{
    public class OfflinePassengerManager : IPassengerManager
    {
        private const string PassengerKey = "passenger";
        private const string DocumentTypeKey = "DocumentTypeDto";
        private readonly IMemoryCache _memoryCache;

        //public readonly List<DocumentTypeDto> DummyDocumentTypes;
        //public readonly List<PassengerDto> DummyPassengers;

        public OfflinePassengerManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            #region Dummy Data

            #region DocumentTypeDto

            if (!_memoryCache.TryGetValue(DocumentTypeKey, out List<DocumentTypeDto> dCache))
            {
                dCache = new()
                {
                    new DocumentTypeDto { Id = 1, Type = "Passport", IssueDate = DateTime.UtcNow, IsActive = true },
                    new DocumentTypeDto { Id = 2, Type = "Visa", IssueDate = DateTime.UtcNow, IsActive = true },
                    new DocumentTypeDto { Id = 3, Type = "Travel", IssueDate = DateTime.UtcNow, IsActive = true }
                };
                _memoryCache.Set(DocumentTypeKey, dCache, new MemoryCacheEntryOptions { Priority = CacheItemPriority.NeverRemove, AbsoluteExpiration = DateTime.Now.AddMinutes(30), });
            }

            #endregion

            #region PassengerDto

            if (!_memoryCache.TryGetValue(PassengerKey, out List<PassengerDto> pCache))
            {
                pCache = new()
                {
                    new PassengerDto { Id = 1, Name = "Ofline_01", Surname = "Surname_01", Gender = 0, DocumentTypeId = 1, DocumentNumber = 1111, IssueDate = DateTime.UtcNow, IsActive = true },
                    new PassengerDto { Id = 2, Name = "Ofline_02", Surname = "Surname_01", Gender = 0, DocumentTypeId = 1, DocumentNumber = 1111, IssueDate = DateTime.UtcNow, IsActive = true },
                    new PassengerDto { Id = 3, Name = "Ofline_03", Surname = "Surname_01", Gender = 0, DocumentTypeId = 1, DocumentNumber = 1111, IssueDate = DateTime.UtcNow, IsActive = true },
                    new PassengerDto { Id = 4, Name = "Ofline_04", Surname = "Surname_01", Gender = 0, DocumentTypeId = 1, DocumentNumber = 1111, IssueDate = DateTime.UtcNow, IsActive = true },
                    new PassengerDto { Id = 5, Name = "Ofline_05", Surname = "Surname_01", Gender = 0, DocumentTypeId = 1, DocumentNumber = 1111, IssueDate = DateTime.UtcNow, IsActive = true },
                    new PassengerDto { Id = 6, Name = "Ofline_06", Surname = "Surname_01", Gender = 0, DocumentTypeId = 1, DocumentNumber = 1111, IssueDate = DateTime.UtcNow, IsActive = true },
                    new PassengerDto { Id = 7, Name = "Ofline_07", Surname = "Surname_01", Gender = 0, DocumentTypeId = 1, DocumentNumber = 1111, IssueDate = DateTime.UtcNow, IsActive = true },
                    new PassengerDto { Id = 8, Name = "Ofline_08", Surname = "Surname_01", Gender = 0, DocumentTypeId = 1, DocumentNumber = 1111, IssueDate = DateTime.UtcNow, IsActive = true },
                    new PassengerDto { Id = 9, Name = "Ofline_09", Surname = "Surname_01", Gender = 0, DocumentTypeId = 1, DocumentNumber = 1111, IssueDate = DateTime.UtcNow, IsActive = true }
                };
                _memoryCache.Set(PassengerKey, pCache, new MemoryCacheEntryOptions { Priority = CacheItemPriority.NeverRemove });

                foreach (var dummyPassenger in pCache)
                {
                    dummyPassenger.DocumentType = dCache.Find(c => c.Id == dummyPassenger.DocumentTypeId);
                }
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

                foreach (var passenger in pCache)
                {
                    passenger.DocumentType = dCache.Find(c => c.Id == passenger.DocumentTypeId);
                }

                return pCache;
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

                var passenger = pCache.Find(c => c.Id == id);
                passenger.DocumentType = dCache.Find(c => c.Id == passenger.DocumentTypeId);
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

                p.DocumentType = dCache.Find(c => c.Id == p.DocumentTypeId);
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
                p.Adapt(passenger);
                passenger.DocumentType = dCache.Find(c => c.Id == p.DocumentTypeId);
                passenger.IssueDate = DateTime.UtcNow;

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
                var p = pCache.Find(c => c.Id == id);
                p.IssueDate = DateTime.UtcNow;
                p.IsActive = false;

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
    }
}