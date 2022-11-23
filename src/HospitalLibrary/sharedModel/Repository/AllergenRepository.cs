using HospitalLibrary.Common;
using HospitalLibrary.Settings;

namespace HospitalLibrary.sharedModel.Repository
{
    public class AllergenRepository : GenericRepository<Allergen>, IAllergenRepository
    {
        public AllergenRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
        
    }
}