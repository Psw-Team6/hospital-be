using System.Threading.Tasks;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.ApplicationUsers.Repository
{
    public class ApplicationUserRepository: GenericRepository<ApplicationUser>,IApplicationUserRepository
    {
        public ApplicationUserRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ApplicationUser> FindByUsername(string username)
        {
            var user = await DbSet.FirstOrDefaultAsync(user => user.Username == username);
            return user;
        }
    }
}