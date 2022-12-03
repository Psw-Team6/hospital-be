using HospitalLibrary.Common;
using HospitalLibrary.Settings;

namespace HospitalLibrary.SharedModel.Repository
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
        
    }
}