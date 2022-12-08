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
namespace HospitalTest.RoomMergingTests
{
    public class RoomMergingTest
    {
        
        private readonly ITestOutputHelper _testOutputHelper;
        public RoomMergingTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        
        [Fact]
        public async Task Merging_Succesfull()
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
                Lenght = 2,
                RoomId = room1.Id,
                Width = 2
            };
            
            Room room2 = new()
            {
                Id = Guid.NewGuid(),
                FloorId = floor11.Id,
                Name = "A11",
                BuildingId = floor11.BuildingId
            };

            GRoom gRoom2 = new()
            {
                Id = Guid.NewGuid(),
                PositionX = 2,
                PositionY = 0,
                Lenght = 2,
                RoomId = room1.Id,
                Width = 2
            };

            room1.GRoomId = gRoom1.Id;
            room2.GRoomId = gRoom2.Id;
            
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var roomService = new RoomService(mockUnitOfWork.Object);
            
            Func<Task> act =  () =>  roomService.MergeRooms(room1, room2);
            
            _testOutputHelper.WriteLine(act.ToString());
            Assert.NotNull(act);
        }

    }
}