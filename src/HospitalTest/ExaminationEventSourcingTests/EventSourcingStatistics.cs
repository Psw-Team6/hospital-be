using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Examinations.EventStores;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Examinations.Repository;
using HospitalLibrary.Examinations.Repository.EventStoreRepository;
using HospitalLibrary.Examinations.Service.EventStoreService;
using Moq;
using Xunit;

namespace HospitalTest.ExaminationEventSourcingTests
{
    public class EventSourcingStatistics
    {
        [Fact]
        public async Task Get_average_steps()
        {
            var mockUnitOfWork = SeedData(out var mockRepo, out var mockExaminationRepo, out var examinations, out var events);
            mockUnitOfWork.Setup(uw => uw.EventStoreExaminationRepository).Returns(mockRepo.Object);
            mockUnitOfWork.Setup(uw => uw.ExaminationRepository).Returns(mockExaminationRepo.Object);
            var mockService = new EventStoreExaminationService(mockUnitOfWork.Object);
            mockUnitOfWork.Setup(x => x.ExaminationRepository
                    .GetAllAsync())
                .ReturnsAsync(() => examinations);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetAllAsync())
                .ReturnsAsync(() => events);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetEventCountByAggregate(It.IsAny<Guid>()))
                .ReturnsAsync(() => 3);
            var service = new EventStoreExaminationService(mockUnitOfWork.Object);
            var result = await service.GetAverageStepCount();
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Get_average_time()
        {
            var mockUnitOfWork = SeedData2(out var mockRepo, out var mockExaminationRepo, out var examinations, out var events);
            mockUnitOfWork.Setup(uw => uw.EventStoreExaminationRepository).Returns(mockRepo.Object);
            mockUnitOfWork.Setup(uw => uw.ExaminationRepository).Returns(mockExaminationRepo.Object);
            var mockService = new EventStoreExaminationService(mockUnitOfWork.Object);
            mockUnitOfWork.Setup(x => x.ExaminationRepository
                    .GetAllAsync())
                .ReturnsAsync(() => examinations);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetEventsByAggregate(It.IsAny<Guid>()))
                .ReturnsAsync(() => events);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetEventCountByAggregate(It.IsAny<Guid>()))
                .ReturnsAsync(() => 3);
            var service = new EventStoreExaminationService(mockUnitOfWork.Object);
            var result = await service.GetAverageTime();
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Get_average_count_for_every_examination_step()
        {
            var mockUnitOfWork = SeedData2(out var mockRepo, out var mockExaminationRepo, out var examinations, out var events);
            mockUnitOfWork.Setup(uw => uw.EventStoreExaminationRepository).Returns(mockRepo.Object);
            mockUnitOfWork.Setup(uw => uw.ExaminationRepository).Returns(mockExaminationRepo.Object);
            var mockService = new EventStoreExaminationService(mockUnitOfWork.Object);
            mockUnitOfWork.Setup(x => x.ExaminationRepository
                    .GetAllAsync())
                .ReturnsAsync(() => examinations);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetEventsByAggregate(It.IsAny<Guid>()))
                .ReturnsAsync(() => events);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetAverageViewForType(It.IsAny<EventStoreExaminationType>()))
                .ReturnsAsync(() => 3);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetEventCountByAggregate(It.IsAny<Guid>()))
                .ReturnsAsync(() => 3);
            var service = new EventStoreExaminationService(mockUnitOfWork.Object);
            var result = await service.GetAverageCountForEveryStep();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Get_average_time_for_every_examination_step()
        {
            var mockUnitOfWork = SeedData(out var mockRepo, out var mockExaminationRepo, out var examinations, out var events);
            mockUnitOfWork.Setup(uw => uw.EventStoreExaminationRepository).Returns(mockRepo.Object);
            mockUnitOfWork.Setup(uw => uw.ExaminationRepository).Returns(mockExaminationRepo.Object);
            var mockService = new EventStoreExaminationService(mockUnitOfWork.Object);
            mockUnitOfWork.Setup(x => x.ExaminationRepository
                    .GetAllAsync())
                .ReturnsAsync(() => examinations);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetEventsByAggregate(It.IsAny<Guid>()))
                .ReturnsAsync(() => events);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetAverageViewForType(It.IsAny<EventStoreExaminationType>()))
                .ReturnsAsync(() => 3);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetAllAsync())
                .ReturnsAsync(() => events);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetEventCountByAggregate(It.IsAny<Guid>()))
                .ReturnsAsync(() => 3);
            var service = new EventStoreExaminationService(mockUnitOfWork.Object);
            var result = await service.GetAverageTimeForEveryStep();
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Get_average_time_for_every_medical_branch()
        {
            var specialization = new Specialization();
            specialization.Name = "Dermatology";
            var specializations = new List<Specialization>();
            specializations.Add(specialization);
            var specializationRepo = new Mock<ISpecializationsRepository>();
            var mockUnitOfWork = SeedData(out var mockRepo, out var mockExaminationRepo, out var examinations, out var events);
            mockUnitOfWork.Setup(uw => uw.EventStoreExaminationRepository).Returns(mockRepo.Object);
            mockUnitOfWork.Setup(uw => uw.ExaminationRepository).Returns(mockExaminationRepo.Object);
            mockUnitOfWork.Setup(uw => uw.SpecializationsRepository).Returns(specializationRepo.Object);
            mockUnitOfWork.Setup(x => x.ExaminationRepository
                    .GetExaminationsBySpecializations(It.IsAny<Guid>()))
                .ReturnsAsync(() => examinations);
            mockUnitOfWork.Setup(x => x.SpecializationsRepository
                    .GetAllAsync())
                .ReturnsAsync(() =>specializations );
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetEventsByAggregate(It.IsAny<Guid>()))
                .ReturnsAsync(() => events);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetAverageViewForType(It.IsAny<EventStoreExaminationType>()))
                .ReturnsAsync(() => 3);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetAllAsync())
                .ReturnsAsync(() => events);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetEventCountByAggregate(It.IsAny<Guid>()))
                .ReturnsAsync(() => 3);
            var service = new EventStoreExaminationService(mockUnitOfWork.Object);
            var result = await service.GetAverageTimeForMedicalBranch();
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Get_average_step_count_for_every_medical_branch()
        {
            var specialization = new Specialization();
            specialization.Name = "Dermatology";
            var specializations = new List<Specialization>();
            specializations.Add(specialization);
            var specializationRepo = new Mock<ISpecializationsRepository>();
            var mockUnitOfWork = SeedData(out var mockRepo, out var mockExaminationRepo, out var examinations, out var events);
            mockUnitOfWork.Setup(uw => uw.EventStoreExaminationRepository).Returns(mockRepo.Object);
            mockUnitOfWork.Setup(uw => uw.ExaminationRepository).Returns(mockExaminationRepo.Object);
            mockUnitOfWork.Setup(uw => uw.SpecializationsRepository).Returns(specializationRepo.Object);
            mockUnitOfWork.Setup(x => x.ExaminationRepository
                    .GetExaminationsBySpecializations(It.IsAny<Guid>()))
                .ReturnsAsync(() => examinations);
            mockUnitOfWork.Setup(x => x.SpecializationsRepository
                    .GetAllAsync())
                .ReturnsAsync(() =>specializations );
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetEventsByAggregate(It.IsAny<Guid>()))
                .ReturnsAsync(() => events);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetAverageViewForType(It.IsAny<EventStoreExaminationType>()))
                .ReturnsAsync(() => 3);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetEventsBySpecialization(It.IsAny<Guid>()))
                .ReturnsAsync(() => events);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetAllAsync())
                .ReturnsAsync(() => events);
            mockUnitOfWork.Setup(x => x.EventStoreExaminationRepository
                    .GetEventCountByAggregate(It.IsAny<Guid>()))
                .ReturnsAsync(() => 3);
            var service = new EventStoreExaminationService(mockUnitOfWork.Object);
            var result = await service.GetStepsForMedicalBranch();
            Assert.NotNull(result);
        }
        private static Mock<IUnitOfWork> SeedData(out Mock<IEventStoreExaminationRepository> mockRepo, out Mock<IExaminationRepository> mockExaminationRepo, out List<Examination> examinations, out List<EventStoreExamination> events)
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockRepo = new Mock<IEventStoreExaminationRepository>();
            mockExaminationRepo = new Mock<IExaminationRepository>();
            var examination = new Examination();
            var examination2 = new Examination();
            examinations = new List<Examination>();
            var @event = new EventStoreExamination();
            events = new List<EventStoreExamination>();
            events.Add(@event);
            examinations.Add(examination);
            examinations.Add(examination2);
            return mockUnitOfWork;
        } 
        private static Mock<IUnitOfWork> SeedData2(out Mock<IEventStoreExaminationRepository> mockRepo, out Mock<IExaminationRepository> mockExaminationRepo, out List<Examination> examinations, out List<EventStoreExamination> events)
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockRepo = new Mock<IEventStoreExaminationRepository>();
            mockExaminationRepo = new Mock<IExaminationRepository>();
            var examination = new Examination();
            var examination2 = new Examination();
            examinations = new List<Examination>();
            events = new List<EventStoreExamination>();
            examinations.Add(examination);
            examinations.Add(examination2);
            return mockUnitOfWork;
        }
    }
}