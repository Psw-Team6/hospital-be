using HospitalLibrary.Common;
using HospitalLibrary.Settings;

namespace HospitalLibrary.sharedModel.Repository
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
        
    }
}