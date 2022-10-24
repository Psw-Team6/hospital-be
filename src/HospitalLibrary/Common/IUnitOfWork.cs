using System;
using System.Threading.Tasks;
using HospitalLibrary.Doctors.Repository;

namespace HospitalLibrary.Common
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        ISpecializationsRepository SpecializationsRepository { get; }
        Task CompleteAsync();
    }
}