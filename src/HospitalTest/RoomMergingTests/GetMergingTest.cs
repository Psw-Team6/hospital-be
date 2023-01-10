using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Common;
using HospitalLibrary.Enums;
using HospitalLibrary.EquipmentMovement.Model;
using HospitalLibrary.EquipmentMovement.Service;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Rooms.Repository;
using HospitalLibrary.Rooms.Service;
using HospitalLibrary.SharedModel;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace HospitalTest.RoomMergingTests
{
    public class GetMergingTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public GetMergingTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        
        
        [Fact]
        public void  GetMerging_Succesfull()
        {
            var mockSearchRepo = new Mock<IRoomMergingRepository>();
            var GetMovementByRoomId = new Mock<RoomRenovationService>();
            var GetMovementByRoomId2 = new Mock<IRoomService>();
            var GetMovementByRoomId3 = new Mock<IAppointmentService>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();


            mockUnitOfWork.Setup(uw => uw.RoomMergingRepository).Returns(mockSearchRepo.Object);
            
            RoomMerging roomMerge1 = new RoomMerging()
            {
               
                
                Id = Guid.NewGuid(),
                Room1Id = Guid.NewGuid(),
                Room2Id = Guid.NewGuid(),
                DateRangeOfMerging= new DateRange()
                {
                    From = new DateTime(2023, 12, 17, 15, 0, 0),
                    To = new DateTime(2023, 12, 18, 15, 30, 0)
                },
                
                // RoomId = room1.Id
            };
            
            var mergedRoom = roomMerge1;
            mergedRoom.Room1Id = roomMerge1.Room1Id;
            
            var roomMerge2 = roomMerge1;
            mergedRoom.Room2Id = roomMerge1.Room2Id;
            
         
            
            mockUnitOfWork.Setup(uw => uw.RoomMergingRepository).Returns(mockSearchRepo.Object);
            
            var equipmentMergingService = new RoomRenovationService(GetMovementByRoomId2.Object,mockUnitOfWork.Object,GetMovementByRoomId3.Object); 
                
            Func<Task> act = () => equipmentMergingService.GetAllMergingByRoomId(mergedRoom.Room1Id);
            Func<Task> act2 = () => equipmentMergingService.GetAllMergingByRoomId(roomMerge2.Room2Id);
    
          
            _testOutputHelper.WriteLine(act.ToString());
            _testOutputHelper.WriteLine(act2.ToString());
            Assert.NotNull(act);
            Assert.NotNull(act2);
        }
        
        
        
    }
    
}