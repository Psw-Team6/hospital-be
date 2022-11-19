using System.Threading.Tasks;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Common;

namespace HospitalLibrary.ApplicationUsers.Repository
{
    public interface IApplicationUserRepository:IGenericRepository<ApplicationUser>
    {
        public Task<ApplicationUser> FindByUsername(string username);
    }
}