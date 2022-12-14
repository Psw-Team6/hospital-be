using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Consiliums.Model;
using HospitalLibrary.CustomException;
using HospitalLibrary.Doctors.Model;
using HospitalLibrary.Doctors.Service;
using HospitalLibrary.Rooms.Model;
using HospitalLibrary.SharedModel;

namespace HospitalLibrary.Consiliums.Service
{
    public class ConsiliumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DoctorService _doctorService;

        public ConsiliumService(IUnitOfWork unitOfWork, DoctorService doctorService)
        {
            _unitOfWork = unitOfWork;
            _doctorService = doctorService;
        }

        public async Task<IEnumerable<Consilium>> GetAll()
        {
            return await _unitOfWork.ConsiliumRepository.GetAllAsync();
        }

        public async Task<Consilium> GetById(Guid id)
        {
            return await _unitOfWork.ConsiliumRepository.GetByIdAsync(id);
        }
        public async Task<Consilium> ScheduleConsilium(Consilium consilium)
        {             
            var doctors = await _doctorService.GetDoctorsForConsilium(consilium);
            var meeting = new Consilium(consilium.Theme, doctors, consilium.TimeRange);
            meeting.ValidateConsilium();
            var timeRange = FindTimeRangeForAllDoctors(meeting);
            var room = await FindAvailableMeetingRoom(timeRange);
            var createdConsilium = await CreatedConsilium(consilium, doctors, timeRange, room);
            return createdConsilium;
        }

        private async Task<Consilium> CreatedConsilium(Consilium consilium, List<Doctor> doctors, TimeRange timeRange, Room room)
        {
            var newConsilium = new Consilium(consilium.Theme, doctors, timeRange, room);
            var createdConsilium = await _unitOfWork.ConsiliumRepository.CreateAsync(newConsilium);
            await _unitOfWork.CompleteAsync();
            return createdConsilium;
        }

        private TimeRange FindTimeRangeForAllDoctors(Consilium meeting)
        {
            var timeRange = FindDateWhenDoctorsAreAvailable(meeting);
            if (timeRange == null)
                throw new ConsiliumException
                    ("The system couldn't find a date and time in that time range!");
            return timeRange;
        }

        private async Task<Room> FindAvailableMeetingRoom(TimeRange timeRange)
        {
            var rooms = await _unitOfWork.RoomRepository.GetAllMeetingRooms();
           
           //Treba refaktorisati
            foreach (var room in rooms)
            {
                var availableRoom =  await IsRoomAvailable(timeRange, room);
                if (availableRoom)
                    return room;
            }
            return null;
        }

        private async Task<bool> IsRoomAvailable(TimeRange timeRange, Room room)
        {
            var consiliums = await _unitOfWork.ConsiliumRepository.GetAllAsync();
            foreach (var consilium in consiliums)
            {
                if (consilium.IsConsiliumInRoom(room.Id, timeRange))
                    return false;
            }
            return true;
        }

        private TimeRange FindDateWhenDoctorsAreAvailable(Consilium consilium)
        {
            var startTime = InitializeStartAndEndTime(consilium, out var endTime);
            while (startTime <= endTime)
            {
                startTime = CheckIfTimeIsAfterTen(startTime);
                var possibleConsiliumTime = PossibleConsiliumTime(consilium, startTime);
                if (possibleConsiliumTime)
                {
                    var timeRange = InitializeTimeRange(consilium, startTime);
                    return timeRange;
                }
                startTime = consilium.TimeRange.AddDurationToDateTime(startTime);
            }
            return null;
        }

        private  DateTime CheckIfTimeIsAfterTen(DateTime startTime)
        {
            if (startTime.TimeOfDay.CompareTo(new TimeSpan(22, 0, 0)) >= 0)
            {
                startTime = startTime.Date.AddDays(1);
                startTime = startTime.Date + new TimeSpan(8, 0, 0);
            }
            return startTime;
        }

        private static TimeRange InitializeTimeRange(Consilium consilium, DateTime startTime)
        {
            var timeRange = new TimeRange
            {
                From = startTime,
                To = startTime.AddMinutes(consilium.TimeRange.Duration),
                Duration = consilium.TimeRange.Duration
            };
            return timeRange;
        }

        private  DateTime InitializeStartAndEndTime(Consilium consilium, out DateTime endTime)
        {
            var startTime = consilium.TimeRange.SetStartTimeAndDate();
            endTime = consilium.TimeRange.SetFinishTimeAndDate();
            return startTime;
        }

        private  bool PossibleConsiliumTime(Consilium consilium, DateTime startTime)
        {
            foreach (var doctor in consilium.Doctors)
            {
                if (!doctor.IsAvailable(startTime, consilium.TimeRange.Duration))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<Consilium> ScheduleConsiliumSpecialization(Consilium consiliumRequest,List<Specialization> specializations)
        {
            var doctors = await  _doctorService.GetDoctorsBySpecializations(specializations);
            var consilium = new Consilium(consiliumRequest.Theme, doctors, consiliumRequest.TimeRange);
            consilium.ValidateConsilium();
            var timeRange = FindDateWhenDoctorsAreAvailable(consilium);
            if (timeRange == null)
            {
                timeRange = FindTheBestTimeRange(specializations , consilium);
            }
            var room = await FindAvailableMeetingRoom(timeRange);
            var createConsilium = await CreatedConsilium(consilium, doctors, timeRange, room);
            return createConsilium;
        }

        private TimeRange FindTheBestTimeRange(List<Specialization> specializations , Consilium consilium)
        {
            var dictionary = FindAllAvailableDoctorsAndAppointments(consilium);
            dictionary = RemoveAppointmentsWithNoDoctors(dictionary, specializations);
            var newDictionary = FindTheBestAppointmnet(dictionary);
            var bestTime = newDictionary.Keys.Min();
            var timeRange = new TimeRange
            {
                From = bestTime,
                To = bestTime.AddMinutes(consilium.TimeRange.Duration),
                Duration = consilium.TimeRange.Duration
            };
            return timeRange;
        }

        private Dictionary<DateTime, List<Doctor>> FindTheBestAppointmnet(Dictionary<DateTime, List<Doctor>> dictionary)
        {
            var sortedDictionary = SortDictionaryByValueCountDesc(dictionary);
            return FindAppointmentsWithBiggestValue(dictionary, sortedDictionary);
        }

        private static Dictionary<DateTime, List<Doctor>> FindAppointmentsWithBiggestValue(Dictionary<DateTime, List<Doctor>> dictionary, Dictionary<DateTime, List<Doctor>> sortedDictionary)
        {
            var newDictionary = new Dictionary<DateTime, List<Doctor>>();

            var maxDoctorNumber = 0;

            foreach (var key in sortedDictionary.Keys)
            {
                if (sortedDictionary[key].Count > maxDoctorNumber)
                {
                    maxDoctorNumber = sortedDictionary[key].Count;
                    newDictionary.Add(key, dictionary[key]);
                    continue;
                }
                if (maxDoctorNumber == sortedDictionary[key].Count)
                    newDictionary.Add(key, dictionary[key]);
            }
            return newDictionary;
        }

        private static Dictionary<DateTime, List<Doctor>> SortDictionaryByValueCountDesc(Dictionary<DateTime, List<Doctor>> dictionary)
        {
            List<KeyValuePair<DateTime, List<Doctor>>> list = dictionary.ToList();
            list.Sort((x, y) => y.Value.Count.CompareTo(x.Value.Count));
            var sortedDictionary = list.ToDictionary(x => x.Key, x => x.Value);
            return sortedDictionary;
        }

        private Dictionary<DateTime,List<Doctor>> RemoveAppointmentsWithNoDoctors(Dictionary<DateTime,List<Doctor>> dictionary, List<Specialization> specializations)
        {
            foreach (var key in dictionary.Keys)
            {  
                var doctorsValue = dictionary[key];
                var shouldRemove = FindNotSuitableAppointments(specializations, doctorsValue);
                if (shouldRemove)
                    dictionary.Remove(key);
            }
            return dictionary;
        }

        private static bool FindNotSuitableAppointments(IEnumerable<Specialization> specializations, List<Doctor> doctorsValue)
        {
            var shouldRemove = false;
            foreach (var spec in specializations)
            {
                var doctorsForSpec = doctorsValue.Where(doctor => doctor.Specialization.Id == spec.Id);
                if (!doctorsForSpec.Any())
                {
                    shouldRemove = true;
                    break;
                }
            }

            return shouldRemove;
        }

        private Dictionary<DateTime, List<Doctor>> FindAllAvailableDoctorsAndAppointments(Consilium consilium)
        {
            var startTime = InitializeStartAndEndTime(consilium, out var endTime);
            var dictionary = new Dictionary<DateTime, List<Doctor>>();
            while (startTime <= endTime)
            {
                startTime = CheckIfTimeIsAfterTen(startTime);
                var doctors = new List<Doctor>();
                doctors.AddRange(consilium.Doctors);
                foreach (var doctor in consilium.Doctors)
                {
                    if (!doctor.IsAvailable(startTime,consilium.TimeRange.Duration))
                        doctors.Remove(doctor);
                }
                dictionary.Add(startTime,doctors);
                startTime = consilium.TimeRange.AddDurationToDateTime(startTime);
            }
            return dictionary;
        }

        public async Task<IEnumerable<Consilium>> GetConsiliumsForDoctor(Guid id)
        {
            var consiliums = await _unitOfWork.ConsiliumRepository.GetConsiliumsForDoctor();
            var consiliumsForDoctor = consiliums.ToList();
            var consiliumList =  FindDoctorConsilium(id, consiliumsForDoctor);
            return consiliumList;
        }

        private static List<Consilium> FindDoctorConsilium(Guid id, List<Consilium> consiliumsForDoctor)
        {
            var consiliumList = new List<Consilium>();
            if (consiliumList == null) throw new ArgumentNullException(nameof(consiliumList));
            foreach (var consilium in consiliumsForDoctor)
            {
                var doctorConsilium = consilium.Doctors.FirstOrDefault(doctor => doctor.Id == id);
                if (doctorConsilium != null)
                    consiliumList.Add(consilium);
            }
            return consiliumList;
        }
    }
}