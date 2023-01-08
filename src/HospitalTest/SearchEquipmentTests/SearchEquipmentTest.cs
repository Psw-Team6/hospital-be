using System;

using System.Threading.Tasks;

using HospitalLibrary.Common;
using HospitalLibrary.CustomException;
using HospitalLibrary.Enums;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Rooms.Repository;
using HospitalLibrary.Rooms.Service;

using Moq;
using Xunit;
using Xunit.Abstractions;



namespace HospitalTest.SearchEquipmentTests
{
    public class SearchEquipmentTest
    {
        
        
        private readonly ITestOutputHelper _testOutputHelper;
        public SearchEquipmentTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        
        [Fact]
        public void  Search_Succesfull()
        {
            var mockSearchRepo = new Mock<IIEquipmentRepository>();
              var mockSearchService = new Mock<EquipmentService>();
              var mockUnitOfWork = new Mock<IUnitOfWork>();
            
            
            //var room1 = SeedDataRoomEquipment(out RoomEquipment roomEquipment);
            
            mockUnitOfWork.Setup(uw => uw.EquipmentRepository).Returns(mockSearchRepo.Object);
            RoomEquipment roomEquipment1 = new RoomEquipment()
            {
                EquipmentName = Equipment.BANDAGE.ToString(),
                Amount = 10,
                RoomEquipmentId = Guid.NewGuid(),
               // RoomId = room1.Id
            };
            var roomEquipment = roomEquipment1;  //skloni ovo posle 
            roomEquipment.EquipmentName = roomEquipment1.EquipmentName;
            
            mockUnitOfWork.Setup(uw => uw.EquipmentRepository).Returns(mockSearchRepo.Object);
            var equipmentService = new EquipmentService(mockUnitOfWork.Object);
            
              Func<Task> act = () => equipmentService.SearchEquipmentByName(roomEquipment.EquipmentName);
            
            _testOutputHelper.WriteLine(act.ToString());
             Assert.NotNull(act);
        }
        
        
        
        
        /*
        [Fact]
        public async Task Search_equipment_doesnt_exist()
        {
              var mockSearchRepo = new Mock<IIEquipmentRepository>();
              var mockSearchService = new Mock<EquipmentService>();
              var mockUnitOfWork = new Mock<IUnitOfWork>();
              
             // var room1 = SeedDataRoomEquipment(out RoomEquipment roomEquipment);
              mockUnitOfWork.Setup(uw => uw.EquipmentRepository).Returns(mockSearchRepo.Object);
              
              RoomEquipment roomEquipment1 = new RoomEquipment()
              {
                  EquipmentName = Equipment.BANDAGE.ToString(),
                  Amount = 10,
                  RoomEquipmentId = Guid.NewGuid(),
                //  RoomId = room1.Id
              };
              
              RoomEquipment roomEquipment2 = new RoomEquipment()
              {
                  EquipmentName = Equipment.SURGICAL_TABLES.ToString(),
                  Amount = 10,
                  RoomEquipmentId = Guid.NewGuid(),
                  //  RoomId = room1.Id
              };
              
              var roomEquipment = roomEquipment2; //skloni ovo posle
              
              
              
              roomEquipment.EquipmentName = roomEquipment1.EquipmentName;
             // roomEquipment.RoomEquipmentId = roomEquipment1.RoomEquipmentId;
              mockUnitOfWork.Setup(uw => uw.EquipmentRepository).Returns(mockSearchRepo.Object);
            
              mockUnitOfWork.Setup(uw => uw.EquipmentRepository
                  
                      .SearchEquipmentByName(It.IsAny<String>()))
                        .ReturnsAsync(() => null);
                    var equipmentService = new EquipmentService(mockUnitOfWork.Object);
            
              Func<Task> act = () => equipmentService.SearchEquipmentByName(roomEquipment.EquipmentName);
              var ex = await Assert.ThrowsAsync<DoctorNotExist>(act);
              Assert.Equal("Equipment does not exist.", ex.Message);
              
        }
*/
        
        
        
        
        
        
        /*
        
      
       private static Room SeedDataRoomEquipment(out RoomEquipment roomEquipment)
        {
            
            Building building1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Stara bolnica"
            };
            Floor floor11 = new()
            {
                Id = Guid.NewGuid(),
                Name = "F0",
                FloorNumber = 0,
                BuildingId = building1.Id
            };

            Room room1 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor11.Id,
                Name = "A11",
                BuildingId = floor11.BuildingId
            };
            
            RoomEquipment roomEquipment1 = new()
            {
              //  RoomId = room1.Id,   
                RoomEquipmentId =Guid.NewGuid(),
                Amount = 15,
                EquipmentName = Equipment.SURGICAL_TABLES.ToString()
            };
            
            
            return roomEquipment1;
          

        }
    
*/








    }
}