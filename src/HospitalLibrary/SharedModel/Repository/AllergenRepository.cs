using HospitalLibrary.Common;
using HospitalLibrary.Settings;

namespace HospitalLibrary.SharedModel.Repository
{
    public class AllergenRepository : GenericRepository<Allergen>, IAllergenRepository
    {
        public AllergenRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
        
    }
}