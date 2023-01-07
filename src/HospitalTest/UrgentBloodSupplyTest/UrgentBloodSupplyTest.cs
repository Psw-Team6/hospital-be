using HospitalAPI;
using HospitalAPI.Controllers;
using HospitalAPI.gRPC;
using HospitalTest.Setup;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using HospitalLibrary.BloodUnits.Model;
using Microsoft.AspNetCore.Mvc;

namespace HospitalTest.UrgentBloodSupplyTest
{
    public class UrgentBloodSupplyTest: BaseIntegrationTest
    {
        public  UrgentBloodSupplyTest(TestDatabaseFactory factory) : base(factory) { }

        [Fact]
        public async Task Send_urgent_requets_to_blood_bank_successfullyAsync()
        {

            BloodType bloodType = BloodType.Apos;
            int amount = 2;
            var mockNotify = new Mock<IUrgentBloodSupplyService>();
            UrgentBloodSupplyController urgentBloodSupplyController = new UrgentBloodSupplyController(mockNotify.Object);
            BloodUnitDto bloodUnit = new BloodUnitDto(bloodType, amount);
            await urgentBloodSupplyController.CreateBloodUnit(bloodUnit);
            
            mockNotify.Verify(x => x.OrderBloodUrgentlyAsync(bloodType.ToString(),amount), Times.Once);
        }

        [Fact]
        public async Task Send_urgent_requets_to_blood_bank_with_incorrect_amount()
        {

            BloodType bloodType = BloodType.Bneg;
            int amount = -1;
            var mockNotify = new Mock<IUrgentBloodSupplyService>();
            UrgentBloodSupplyController urgentBloodSupplyController = new UrgentBloodSupplyController(mockNotify.Object);
            BloodUnitDto bloodUnit = new BloodUnitDto(bloodType, amount);

            await urgentBloodSupplyController.CreateBloodUnit(bloodUnit);
            
            mockNotify.Verify(x => x.OrderBloodUrgentlyAsync(bloodType.ToString(), amount), Times.Once);
        }

    }
    
}
