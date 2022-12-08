using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Common;
using HospitalLibrary.Enums;
using HospitalLibrary.EquipmentMovement.Model;
using HospitalLibrary.EquipmentMovement.Repository;
using HospitalLibrary.EquipmentMovement.Service;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Rooms.Service;
using HospitalLibrary.Settings;
using HospitalLibrary.sharedModel;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace HospitalTest.EquipmentMovementTest
{
    public class GetMovementByRoomId
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public GetMovementByRoomId(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        
        
        
        [Fact]
        public async Task GetMovementByRoomId_Succesfull()
        {
            var mockSearchRepo = new Mock<IEquipmentMovementAppointmentRepository>();
            var GetMovementByRoomId = new Mock<EquipmentMovementAppointmentService>();
            var GetMovementByRoomId2 = new Mock<IAppointmentService>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();


            mockUnitOfWork.Setup(uw => uw.EquipmentMovementAppointmentRepository).Returns(mockSearchRepo.Object);
            
            EquipmentMovementAppointment movedEquipment1 = new EquipmentMovementAppointment()
            {
               
                
                Id = Guid.NewGuid(),
                Amount = 1,
                OriginalRoomId = Guid.NewGuid(),
                DestinationRoomId = Guid.NewGuid(),
                Duration = new DateRange(),
                EquipmentName = Equipment.ANESTHESIA.ToString(),
                // RoomId = room1.Id
            };
            
            var movedEquipment = movedEquipment1;
            movedEquipment.OriginalRoomId = movedEquipment1.OriginalRoomId;
            
            var movedEquipment2 = movedEquipment1;
            movedEquipment.DestinationRoomId = movedEquipment1.DestinationRoomId;
            
           // var roomEquipment = roomEquipment1;  //skloni ovo posle 
          //  roomEquipment.EquipmentName = roomEquipment1.EquipmentName;
            
            mockUnitOfWork.Setup(uw => uw.EquipmentMovementAppointmentRepository).Returns(mockSearchRepo.Object);
            //var equipmentService = new EquipmentService(mockUnitOfWork.Object);
            var equipmentMovementService = new EquipmentMovementAppointmentService(mockUnitOfWork.Object,GetMovementByRoomId2.Object); 
                
            Func<Task> act = () => equipmentMovementService.GetAllMovementAppointmentByRoomId(movedEquipment.OriginalRoomId);
    
          
            _testOutputHelper.WriteLine(act.ToString());
            Assert.NotNull(act);
        }
        
        
        
        
        
        
    }
}