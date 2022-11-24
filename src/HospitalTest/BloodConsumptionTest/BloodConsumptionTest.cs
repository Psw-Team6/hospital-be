using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.BloodConsumptions.Model;
using HospitalLibrary.BloodConsumptions.Service;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Model;
using Moq;
using Xunit;

namespace HospitalTest.BloodConsumptionTest
{
    public class BloodConsumptionTest
    {
        [Fact]
        public async Task Hospital_doesnt_have_enough_amount_of_units()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var request = BloodConsumptionTest.request;
            mockUnitOfWork.Setup(uw => uw.BloodUnitRepository
                .GetUnitsAmountByType(BloodType.Aneg))
                .ReturnsAsync(2);

            BloodConsumptionService service = new BloodConsumptionService(mockUnitOfWork.Object);

            List<BloodConsumption> res = service.CreateConsumptions(request).Result;
            Assert.Null(res);
        }
        
        [Fact]
        public async Task Consumption_doctor_doesnt_exists()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var request = BloodConsumptionTest.request;
            mockUnitOfWork.Setup(uw => uw.BloodUnitRepository
                    .GetUnitsAmountByType(BloodType.Aneg))
                .ReturnsAsync(7);
            mockUnitOfWork.Setup(uw => uw.DoctorRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            BloodConsumptionService service = new BloodConsumptionService(mockUnitOfWork.Object);

            List<BloodConsumption> res = service.CreateConsumptions(request).Result;
            Assert.Null(res);
        }
        
        [Fact]
        public async Task Get_units_for_consumptions()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var request = BloodConsumptionTest.request;
            var unitsForConsumption = SeedUnitsData();
            mockUnitOfWork.Setup(uw => uw.BloodUnitRepository
                    .GetSortUnitsByType(BloodType.Aneg))
                .ReturnsAsync(unitsForConsumption);
            
            BloodConsumptionService service = new BloodConsumptionService(mockUnitOfWork.Object);

            List<BloodUnit> res = service.BloodUnitsForConsumptions(request);
            Assert.Equal(res,BloodConsumptionTest.SeedGetUnitsForConsumptionsTrueData());
        }
        
        [Fact]
        public async Task Good()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var request = BloodConsumptionTest.request;
            var unitsForConsumption = SeedUnitsData1();
            mockUnitOfWork.Setup(uw => uw.BloodUnitRepository
                    .GetUnitsAmountByType(BloodType.Aneg))
                .ReturnsAsync(17);
            mockUnitOfWork.Setup(uw => uw.DoctorRepository
                    .GetByIdAsync(request.doctorId))
                .ReturnsAsync(doctor1);
            mockUnitOfWork.Setup(uw => uw.BloodUnitRepository
                    .GetSortUnitsByType(BloodType.Aneg))
                .ReturnsAsync(unitsForConsumption);
            mockUnitOfWork.Setup(uw => uw.BloodConsumptionRepository
                    .CreateAsync(SeedConsumptionData()))
                .ReturnsAsync(SeedConsumptionData());
             
            BloodConsumptionService service = new BloodConsumptionService(mockUnitOfWork.Object);

            List<BloodConsumption> res = service.CreateConsumptions(request).Result;
            Assert.NotNull(res); 
        }
        
        static Guid unit1Id = Guid.NewGuid();
        static Guid unit2Id = Guid.NewGuid();
        static Guid unit3Id = Guid.NewGuid();
        static Guid doctorId = Guid.NewGuid();
        static Guid consumptionId = Guid.NewGuid();
        private static List<BloodUnit> SeedUnitsData()
        {
            var list = new List<BloodUnit>();
            
            BloodUnit unit2 = new()
            {
                Id= unit2Id,
                BloodType = BloodType.Aneg,
                Amount = 5,
                BloodBankName = "Moja Banka Krvi"
                    
            };
            BloodUnit unit3 = new()
            {
                Id= unit3Id,
                BloodType = BloodType.Aneg,
                Amount = 5,
                BloodBankName = "Moja Banka Krvi"
                    
            };
            list.Add(unit1);
            list.Add(unit2);
            list.Add(unit3);
            return list;
        }
        
        static BloodUnit unit1 = new()
        {
            Id= unit1Id,
            BloodType = BloodType.Aneg,
            Amount = 7,
            BloodBankName = "Moja Banka Krvi"
                    
        };

        private static List<BloodUnit> SeedUnitsData1()
        {
            var list = new List<BloodUnit>();
            BloodUnit unit1 = new()
            {
                Id= unit1Id,
                BloodType = BloodType.Aneg,
                Amount = 7,
                BloodBankName = "Moja Banka Krvi"
                    
            };
            BloodUnit unit2 = new()
            {
                Id= unit2Id,
                BloodType = BloodType.Aneg,
                Amount = 5,
                BloodBankName = "Moja Banka Krvi"
                    
            };
            BloodUnit unit3 = new()
            {
                Id= unit3Id,
                BloodType = BloodType.Aneg,
                Amount = 5,
                BloodBankName = "Moja Banka Krvi"
                    
            };
            list.Add(unit1);
            list.Add(unit2);
            list.Add(unit3);
            return list;
        }
        
        
        private static List<BloodUnit> SeedGetUnitsForConsumptionsTrueData()
        {
            var list = new List<BloodUnit>();
            list.Add(unit1);
            return list;
        }
        
        static Doctor doctor1 = new()
        {
            Id = doctorId,
            Username = "Ilija",
            Password = "miki123",
            Name = "Ilija",
            Surname = "Maric",
            Email = "Cajons@gmail.com",
            Jmbg = "99999999",
            Phone = "+612222222"
        };
        
        static BloodConsumptionCreateDto request = new()
        {
            BloodType= BloodType.Aneg,
            Amount = 5,
            Purpose = "test",
            doctorId = doctor1.Id
                    
        };

        public static BloodConsumption SeedConsumptionData()
        {
            BloodUnit unit1 = new()
            {
                Id= unit1Id,
                BloodType = BloodType.Aneg,
                Amount = 7,
                BloodBankName = "Moja Banka Krvi"
                    
            };
            BloodConsumption consumption1 = new BloodConsumption()
            {
                Id = consumptionId,
                BloodUnitId = unit1.Id,
                Amount = 5,
                DoctorId =doctor1.Id,
                Date = new DateTime(2022, 10, 27, 15, 0, 0),
                Purpose = "operation"
            };
            return consumption1;
        }
        
    }
}