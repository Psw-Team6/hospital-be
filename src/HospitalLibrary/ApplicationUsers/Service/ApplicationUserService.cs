using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.ApplicationUsers.Model;
using HospitalLibrary.Common;
using HospitalLibrary.CustomException;

namespace HospitalLibrary.ApplicationUsers.Service
{
    public class ApplicationUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApplicationUser> Authenticate(string username, string password)
        {
            var user = await _unitOfWork.UserRepository.FindByUsername(username);
            if (user == null)
            {
                throw new AuthenticationException("Bad credentials!");
            }

            if (!PasswordHasher.VerifyPassword(password, user.Password))
            {
                throw new AuthenticationException("Bad credentials!");
            }

            if (!user.Enabled)
            {
                throw new AuthenticationException("Please verify you account!");
            }

            return user;
        }

        public async Task<List<ApplicationUser>> GetAll()
        {
            return await _unitOfWork.UserRepository.GetAllAsync() as List<ApplicationUser>;
        }
    }
}