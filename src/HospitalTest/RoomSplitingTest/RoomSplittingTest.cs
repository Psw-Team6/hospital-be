using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Rooms.Repository;
using HospitalLibrary.Rooms.Service;
using HospitalLibrary.SharedModel;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace HospitalTest.RoomSplitingTest
{
    public class RoomSplittingTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public RoomSplittingTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        
        
        [Fact]
        public async Task GetMerging_Succesfull()
        {
            var mockSearchRepo = new Mock<IRoomSplitingRepository>();
            var GetMovementByRoomId = new Mock<RoomRenovationService>();
            var GetMovementByRoomId2 = new Mock<IRoomService>();
            var GetMovementByRoomId3 = new Mock<IAppointmentService>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();


            mockUnitOfWork.Setup(uw => uw.RoomSplitingRepository).Returns(mockSearchRepo.Object);
            
            RoomSpliting roomMerge1 = new RoomSpliting()
            {
               
                
                Id = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                
                DatesForSearch= new DateRange()
                {
                    From = new DateTime(2023, 12, 17, 15, 0, 0),
                    To = new DateTime(2023, 12, 18, 15, 30, 0)
                },
                
                // RoomId = room1.Id
            };
            
            var mergedRoom = roomMerge1;
            mergedRoom.RoomId = roomMerge1.RoomId;
            
            
            
         
            
            mockUnitOfWork.Setup(uw => uw.RoomSplitingRepository).Returns(mockSearchRepo.Object);
            
            var equipmentMergingService = new RoomRenovationService(GetMovementByRoomId2.Object,mockUnitOfWork.Object,GetMovementByRoomId3.Object); 
                
            Func<Task> act = () => equipmentMergingService.GetAllMergingByRoomId(mergedRoom.RoomId);
            ;
    
          
            _testOutputHelper.WriteLine(act.ToString());
            Assert.NotNull(act);
            
        }
    }
}