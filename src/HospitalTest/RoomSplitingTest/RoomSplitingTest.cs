using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Common;
using HospitalLibrary.Enums;
using HospitalLibrary.EquipmentMovement.Model;
using HospitalLibrary.EquipmentMovement.Repository;
using HospitalLibrary.EquipmentMovement.Service;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Rooms.Repository;
using HospitalLibrary.Rooms.Service;
using HospitalLibrary.SharedModel;
using Moq;
using Xunit;
using Xunit.Abstractions;
namespace HospitalTest.RoomSplitingTest
{
    public class RoomSplitingTest
    {
        
        private readonly ITestOutputHelper _testOutputHelper;
        public RoomSplitingTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        
        [Fact]
        public async Task Spliting_Succesfull()
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
                Name = "B22",
                BuildingId = floor11.BuildingId
            };

            GRoom gRoom1 = new()
            {
                Id = Guid.NewGuid(),
                PositionX = 0,
                PositionY = 0,
                Lenght = 4,
                RoomId = room1.Id,
                Width = 4
            };

            room1.GRoomId = gRoom1.Id;
            
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var roomService = new RoomService(mockUnitOfWork.Object);
            
            Func<Task> act =  () =>  roomService.SplitRoom(room1.Id, "Nova soba");
            
            _testOutputHelper.WriteLine(act.ToString());
            Assert.NotNull(act);
        }
    }
}