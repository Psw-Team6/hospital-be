using HospitalLibrary.Common;
using HospitalLibrary.Consiliums.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Consiliums.Repository
{
    public class ConsiliumRepository : GenericRepository<Consilium> , IConsiliumRepository
    {
        public ConsiliumRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}