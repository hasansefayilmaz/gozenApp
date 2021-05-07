﻿using Gozen.Data.Core;
using Gozen.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Gozen.Data.Repositories
{
    public interface IPassengerRepository : IRepository<Passenger>
    {
    }
    public class PassengerRepository : Repository<Passenger>, IPassengerRepository
    {
        public PassengerRepository(GozenDbContext gozenDbContext) : base(gozenDbContext)
        {

        }
    }
}
