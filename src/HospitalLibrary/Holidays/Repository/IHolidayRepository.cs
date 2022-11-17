using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Holidays.Model;

namespace HospitalLibrary.Holidays.Repository
{
    public interface IHolidayRepository : IGenericRepository<Holiday>
    {
        Task<IEnumerable<Holiday>> GetAllHolidaysForDoctor(Guid doctorId);
    }
}