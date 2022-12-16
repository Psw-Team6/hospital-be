using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Consiliums.Model;

namespace HospitalLibrary.Consiliums.Repository
{
    public interface IConsiliumRepository : IGenericRepository<Consilium>
    {
        Task<IEnumerable<Consilium>> GetConsiliumsForDoctor();
    }
}