using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Holidays.Model;
using HospitalLibrary.Holidays.Repository;

namespace HospitalLibrary.Holidays.Service
{
    public class HolidayService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HolidayService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Holiday>> GetAllHolidays()
        {
            var holidays = await _unitOfWork.GetRepository<HolidayRepository>().GetAllAsync();
            return holidays;
        }

        public async Task<Holiday> GetById(Guid id)
        {
            return await _unitOfWork.GetRepository<HolidayRepository>().GetByIdAsync(id);
        }

        public async Task<Holiday> ScheduleHoliday(Holiday holiday)
        {
            var newHoliday = await _unitOfWork.HolidayRepository.CreateAsync(holiday);
            await _unitOfWork.CompleteAsync();
            return newHoliday;
        }
    }
}