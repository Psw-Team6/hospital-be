using System;
using System.Threading.Tasks;
using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Common;
using HospitalLibrary.Common.EventSourcing;
using HospitalLibrary.Examinations.DomainEvents;
using HospitalLibrary.Examinations.EventStores;
using HospitalLibrary.Examinations.Model;
using HospitalLibrary.Examinations.Repository.EventStoreRepository;
using HospitalLibrary.Examinations.Service.EventStoreService;
using Moq;
using Xunit;

namespace HospitalTest.ExaminationEventSourcingTests
{
    public class CreateEventExaminationTests
    {
        
        [Fact]
        public async Task CreateEvents_ShouldCreateEvent()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<IEventStoreExaminationRepository>();
            var examination =  new Examination();
            var @event = new SymptomsViewedEvent(new DateTime(2022,12,12),EventStoreExaminationType.SYMPTOMS_VIEWED);
            
            var maxDate = DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc);

            var eventStore = new EventStoreExamination(examination,maxDate,1,1,EventStoreExaminationType.SYMPTOMS_VIEWED,"symptoms");
            mockUnitOfWork.Setup(uw => uw.EventStoreExaminationRepository).Returns(mockRepo.Object);
            var mockService = new EventStoreExaminationService(mockUnitOfWork.Object);
            mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<EventStoreExamination>()))
                .ReturnsAsync(()=>eventStore);
            mockRepo.Setup(repo => repo.GetVersionCount(It.IsAny<Guid>()))
                .ReturnsAsync(()=>1);
            mockRepo.Setup(repo => repo.GetSequenceCount())
                .ReturnsAsync(()=>1);
            var result =await mockService.CreateEvents(@event, examination);
            Assert.NotNull(result);
        }
    }
}