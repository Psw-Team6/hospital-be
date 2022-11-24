using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Common;
using HospitalLibrary.Enums;
using HospitalLibrary.EquipmentMovement.Model;
using HospitalLibrary.EquipmentMovement.Repository;
using HospitalLibrary.EquipmentMovement.Service;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.Rooms.Repository;
using HospitalLibrary.sharedModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SendGrid.Helpers.Errors.Model;
using Xunit;
using Xunit.Abstractions;

namespace HospitalTest.EquipmentMovementTest
{
    public class EquipmentMovementTest
    {
        [Fact]
        public async Task Schedule_equipment_movement_destination_room_doesnt_exist()
        {
            var mockEquipmentRepo = new Mock<IIEquipmentRepository>();
            var mockRoomRepo = new Mock<IRoomRepository>();
            var mockAppointmentService = new Mock<IAppointmentService>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var room1 = SeedDataRoom(out EquipmentMovementAppointment equipmentMovementAppointment);
            mockUnitOfWork.Setup(uw => uw.RoomRepository).Returns(mockRoomRepo.Object);
            RoomEquipment roomEquipment1 = new RoomEquipment()
            {
                EquipmentName = Equipment.BANDAGE.ToString(),
                Amount = 10,
                RoomEquipmentId = Guid.NewGuid(),
                RoomId = room1.Id
            };
            equipmentMovementAppointment.EquipmentName = roomEquipment1.EquipmentName;
            equipmentMovementAppointment.EquipmentId = roomEquipment1.RoomEquipmentId;
            mockUnitOfWork.Setup(uw => uw.EquipmentRepository).Returns(mockEquipmentRepo.Object);
            mockUnitOfWork.Setup(uw => uw.EquipmentRepository
                    .GetEquipmentById(It.IsAny<Guid>()))
                .ReturnsAsync(() => roomEquipment1);
            mockUnitOfWork.Setup(uw => uw.RoomRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);
            var equipmentMovementAppointmentService = new EquipmentMovementAppointmentService(mockUnitOfWork.Object, mockAppointmentService.Object);
            
            var res =  await  equipmentMovementAppointmentService.Create(equipmentMovementAppointment);
      
            Assert.Null(res);
        }
        
        [Fact]
        public async Task Schedule_equipment_movement_equipment_doesnt_exist()
        {
            var mockEquipmentRepo = new Mock<IIEquipmentRepository>();
            var mockRoomRepo = new Mock<IRoomRepository>();
            var mockAppointmentService = new Mock<IAppointmentService>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var room1 = SeedDataRoom(out EquipmentMovementAppointment equipmentMovementAppointment);
            mockUnitOfWork.Setup(uw => uw.RoomRepository).Returns(mockRoomRepo.Object);
            mockUnitOfWork.Setup(uw => uw.EquipmentRepository).Returns(mockEquipmentRepo.Object);
            mockUnitOfWork.Setup(uw => uw.EquipmentRepository
                    .GetEquipmentById(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);
            mockUnitOfWork.Setup(uw => uw.RoomRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);
            var equipmentMovementAppointmentService = new EquipmentMovementAppointmentService(mockUnitOfWork.Object, mockAppointmentService.Object);
            
            var res =  await equipmentMovementAppointmentService.Create(equipmentMovementAppointment);
            
            Assert.Null(res);
        }
        [Fact]
        public async Task Schedule_equipment_movement_amount_negative_exist()
        {
            var mockEquipmentRepo = new Mock<IIEquipmentRepository>();
            var mockRoomRepo = new Mock<IRoomRepository>();
            var mockAppointmentService = new Mock<IAppointmentService>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            
            var room1 = SeedDataRoom(out EquipmentMovementAppointment equipmentMovementAppointment);
            mockUnitOfWork.Setup(uw => uw.RoomRepository).Returns(mockRoomRepo.Object);
            RoomEquipment roomEquipment1 = new RoomEquipment()
            {
                EquipmentName = Equipment.BANDAGE.ToString(),
                Amount = 10,
                RoomEquipmentId = Guid.NewGuid(),
                RoomId = room1.Id
            };
            equipmentMovementAppointment.Amount = -5;
            equipmentMovementAppointment.EquipmentName = roomEquipment1.EquipmentName;
            equipmentMovementAppointment.EquipmentId = roomEquipment1.RoomEquipmentId;
            mockUnitOfWork.Setup(uw => uw.EquipmentRepository).Returns(mockEquipmentRepo.Object);
            mockUnitOfWork.Setup(uw => uw.EquipmentRepository
                    .GetEquipmentById(It.IsAny<Guid>()))
                .ReturnsAsync(() => roomEquipment1);
            mockUnitOfWork.Setup(uw => uw.RoomRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);
            var equipmentMovementAppointmentService = new EquipmentMovementAppointmentService(mockUnitOfWork.Object, mockAppointmentService.Object);
            
            var res =  await equipmentMovementAppointmentService.Create(equipmentMovementAppointment);
          
            Assert.Null(res);
        }
        
        private readonly ITestOutputHelper _testOutputHelper;
        public EquipmentMovementTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        
        [Fact]
        public async Task Schedule_Succesfull()
        {
            var mockEquipmentMovementRepo = new Mock<IEquipmentMovementAppointmentRepository>();
            var mockEquipmentRepo = new Mock<IIEquipmentRepository>();
            var mockRoomRepo = new Mock<IRoomRepository>();
            var mockAppointmentService = new Mock<IAppointmentService>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var room1 = SeedDataRoom(out EquipmentMovementAppointment equipmentMovementAppointment);
            mockUnitOfWork.Setup(uw => uw.RoomRepository).Returns(mockRoomRepo.Object);
            RoomEquipment roomEquipment1 = new RoomEquipment()
            {
                EquipmentName = Equipment.BANDAGE.ToString(),
                Amount = 10,
                RoomEquipmentId = Guid.NewGuid(),
                RoomId = room1.Id
            };
            equipmentMovementAppointment.EquipmentName = roomEquipment1.EquipmentName;
            equipmentMovementAppointment.EquipmentId = roomEquipment1.RoomEquipmentId;
            mockUnitOfWork.Setup(uw => uw.EquipmentRepository).Returns(mockEquipmentRepo.Object);
            mockUnitOfWork.Setup(uw => uw.EquipmentMovementAppointmentRepository).Returns(mockEquipmentMovementRepo.Object);

            mockUnitOfWork.Setup(uw => uw.EquipmentRepository
                    .GetEquipmentById(It.IsAny<Guid>()))
                .ReturnsAsync(() => roomEquipment1);
            mockUnitOfWork.Setup(uw => uw.RoomRepository
                    .GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => room1);
            var equipmentMovementAppointmentService = new EquipmentMovementAppointmentService(mockUnitOfWork.Object, mockAppointmentService.Object);
            
            Func<Task> act =  () =>  equipmentMovementAppointmentService.Create(equipmentMovementAppointment);
            
            _testOutputHelper.WriteLine(act.ToString());
            Assert.NotNull(act);
        }

        private static Room SeedDataRoom(out EquipmentMovementAppointment equipmentMovementAppointment)
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


            equipmentMovementAppointment = new()
            {
                Id = Guid.NewGuid(),
                Amount = 5,
                DestinationRoom = null,
                OriginalRoom = room1,
                DestinationRoomId = Guid.NewGuid(),
                Duration = new DateRange()
                {
                    From = new DateTime(2023, 10, 27, 15, 0, 0),
                    To = new DateTime(2023, 10, 29, 15, 30, 0)
                },
                OriginalRoomId = room1.Id
            };
            
            return room1;
        }
    }
}