using HospitalLibrary.BloodUnits.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.gRPC
{
    public interface IUrgentBloodSupplyService
    {
        public Task OrderBloodUrgentlyAsync(string bloodType, int bloodAmount);
    }
}
