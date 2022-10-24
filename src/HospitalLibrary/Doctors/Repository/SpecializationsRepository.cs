using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Doctors.Repository
{
    public class SpecializationsRepository:GenericRepository<Specialization>, ISpecializationsRepository
    {
        public SpecializationsRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}