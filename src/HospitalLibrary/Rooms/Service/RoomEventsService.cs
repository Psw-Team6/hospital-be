using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Common;
using HospitalLibrary.Rooms.Model;

namespace HospitalLibrary.Rooms.Service
{
    public class RoomEventsService : IRoomEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomEventsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RoomEvent>> GetAll()
        {
            return await _unitOfWork.RoomEventRepository.GetAllAsync();
        }

        public async Task<RoomEvent> Create(RoomEvent roomEvent)
        {
            roomEvent.Id = Guid.NewGuid();
            roomEvent.TimeStamp = DateTime.Now;

            var result = await _unitOfWork.RoomEventRepository.CreateAsync(roomEvent);

            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async  Task<int> SuccesfullMergingCount()
        {
            var events = await _unitOfWork.RoomEventRepository.GetAllAsync();
            int count = events.Count(e => e.EventName == "SessionEnded" && e.Value == "MergingSuccesful");
            return count;
        }
        public async  Task<int> SuccesfullSplitingCount()
        {
            var events = await _unitOfWork.RoomEventRepository.GetAllAsync();
            int count = events.Count(e => e.EventName == "SessionEnded" && e.Value == "SplitingSuccesful");
            return count;
        }

        public async Task<int[]> StepMergingCount()
        {
            int[] result = { 0, 0, 0, 0 };

            var events = await _unitOfWork.RoomEventRepository.GetAllAsync();
            result[0] = events.Count(e => e.EventName.Contains("MergingStep1"));
            result[1] = events.Count(e => e.EventName.Contains("MergingStep2"));
            result[2] = events.Count(e => e.EventName.Contains("MergingStep3"));
            result[3] = events.Count(e => e.EventName.Contains("SessionEnded") && e.Value == ("MergingSuccesful"));
            
            return result;
        }

        public async Task<int[]> StepSplitingCount()
        {
            int[] result = { 0, 0, 0, 0 };

            var events = await _unitOfWork.RoomEventRepository.GetAllAsync();
            result[0] = events.Count(e => e.EventName.Contains("SplitingStep1"));
            result[1] = events.Count(e => e.EventName.Contains("SplitingStep2"));
            result[2] = events.Count(e => e.EventName.Contains("SplitingStep3"));
            result[3] = events.Count(e => e.EventName.Contains("SessionEnded") && e.Value == ("SplitingSuccesful"));

            
            return result;
        }

        public async Task<int> SchedulingCanceledCount()
        {
            var events = await _unitOfWork.RoomEventRepository.GetAllAsync();
            int count = events.Count(e =>  e.Value == "Exit");
            return count;
        }
        public async Task<IEnumerable<RoomEvent>> GetRoomEventsInLastDay()
        {
            IEnumerable<RoomEvent> events = await _unitOfWork.RoomEventRepository.GetAllAsync();

            List<RoomEvent> list = new List<RoomEvent>();
            
            foreach (RoomEvent e in events)
            {
                if (list.Count() > 20)
                {
                    break;
                }
                
                if (e.TimeStamp > DateTime.Now.AddDays(-1))
                {
                    list.Add(e);
                }
            }
            
            IEnumerable<RoomEvent> result = list;
            
            return result;
        }

        public async  Task<int[]> GetAverageMergningSchedulingTimes()
        {
            int[] result = { 0, 0, 0 };
            List<List<RoomEvent>> allSessions = await GetSessions();
            
            foreach (var session in allSessions)
            {
                if (session[0].Value == "Merging" && session[session.Count()-1].Value == "MergingSuccesful")
                {
                    int ukupnoVreme = 0;

                    ukupnoVreme = (int)(session[session.Count-1].TimeStamp - session[0].TimeStamp).TotalSeconds;

                    if (ukupnoVreme <= 30)
                    {
                        result[0] += 1;
                    }
                    else if (ukupnoVreme <= 60)
                    {
                        result[1] += 1;
                    }
                    else
                    {
                        result[2] += 1;
                    }
                }
            }

            return result;
        }

        public async  Task<int[]> GetAverageMergningStepTimes()
        {
            int[] result = { 0, 0, 0, 0 };
            List<List<RoomEvent>> allSessions = await GetSessions();

            int[] vremena = { 0, 0, 0, 0 };
            int[] brojaci = { 0, 0, 0, 0 };
            
            foreach (var session in allSessions)
            {
                int i = 0;
                if (session[0].Value == "Merging")
                {
                    foreach (var e in session)
                    {
                        if (e.EventName.Contains("SessionStarted") && e.Value == "Merging")
                        {
                            vremena[0] += (int)(session[i + 1].TimeStamp - e.TimeStamp).TotalSeconds;
                            brojaci[0]++;
                        }
                        else if (e.EventName.Contains("1"))
                        {
                            vremena[1] += (int)( session[i + 1].TimeStamp -e.TimeStamp).TotalSeconds;
                            brojaci[1]++;
                        }
                        else if (e.EventName.Contains("2"))
                        {
                            vremena[2] += (int)( session[i + 1].TimeStamp -e.TimeStamp).TotalSeconds;
                            brojaci[2]++;
                        }
                        else if (e.EventName.Contains("3"))
                        {
                            vremena[3] += (int)( session[i + 1].TimeStamp -e.TimeStamp).TotalSeconds;
                            brojaci[3]++;
                        }

                        i++;
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (brojaci[i] == 0)
                {
                    result[i] = 0;
                }
                else
                {
                    result[i] = vremena[i] / brojaci[i];
                }
            }
            
            return result;
        }

        public async  Task<int[]> GetAverageSplitingSchedulingTimes()
        {
            int[] result = { 0, 0, 0 };
            List<List<RoomEvent>> allSessions = await GetSessions();
            
            foreach (var session in allSessions)
            {
                if (session[0].Value == "Spliting" && session[session.Count()-1].Value == "SplitingSuccesful")
                {
                    int ukupnoVreme = 0;

                    ukupnoVreme = (int)(session[session.Count-1].TimeStamp - session[0].TimeStamp).TotalSeconds;

                    if (ukupnoVreme <= 30)
                    {
                        result[0] += 1;
                    }
                    else if (ukupnoVreme <= 60)
                    {
                        result[1] += 1;
                    }
                    else
                    {
                        result[2] += 1;
                    }
                }
            }

            return result;
        }

        public async Task<int[]> GetAverageSplitingStepTimes()
        {
            int[] result = { 0, 0, 0, 0 };
            List<List<RoomEvent>> allSessions = await GetSessions();

            int[] vremena = { 0, 0, 0, 0 };
            int[] brojaci = { 0, 0, 0, 0 };
            
            foreach (var session in allSessions)
            {
                int i = 0;
                if (session[0].Value == "Spliting")
                {
                    foreach (var e in session)
                    {
                        if (e.EventName.Contains("SessionStarted") && e.Value == "Merging")
                        {
                            vremena[0] += (int)(session[i + 1].TimeStamp -e.TimeStamp).TotalSeconds;
                            brojaci[0]++;
                        }
                        else if (e.EventName.Contains("1"))
                        {
                            vremena[1] += (int)(session[i + 1].TimeStamp -e.TimeStamp).TotalSeconds;
                            brojaci[1]++;
                        }
                        else if (e.EventName.Contains("2"))
                        {
                            vremena[2] += (int)(session[i + 1].TimeStamp - e.TimeStamp).TotalSeconds;
                            brojaci[2]++;
                        }
                        else if (e.EventName.Contains("3"))
                        {
                            vremena[3] += (int)(session[i + 1].TimeStamp - e.TimeStamp).TotalSeconds;
                            brojaci[3]++;
                        }

                        i++;
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (brojaci[i] == 0)
                {
                    result[i] = 0;
                }
                else
                {
                    result[i] = vremena[i] / brojaci[i];
                }
            }

            return result;
        }

        public async Task<List<List<RoomEvent>>> GetSessions()
        {
            List<List<RoomEvent>> sessions = new List<List<RoomEvent>> ();

            IEnumerable<RoomEvent> events = await _unitOfWork.RoomEventRepository.GetAllAsync();
            List<RoomEvent> listOfEvents = events.ToList();
            
            listOfEvents = new List<RoomEvent>(listOfEvents.OrderBy(x => x.TimeStamp)); 
            
            List<RoomEvent> newSession = new List<RoomEvent>();
            
            foreach (RoomEvent e in listOfEvents)
            {
                if (newSession.Count() > 0)
                {

                    if (e.EventName == "SessionStarted")
                    {
                        newSession = new List<RoomEvent>();
                        newSession.Add(e);
                    }
                    else if(e.EventName == "SessionEnded")
                    {
                        newSession.Add(e);
                        sessions.Add(newSession);
                        newSession = new List<RoomEvent>();
                    }
                    else if ( e.Value == "PageExit" || e.Value == "Exit")
                    {
                        newSession = new List<RoomEvent>();
                    }
                    else
                    {
                        newSession.Add(e);
                    }
                }
                else
                {
                    if (e.EventName == "SessionStarted")
                    {
                        newSession.Add(e);
                    }
                    
                }
            }

            return sessions;
        }
    }
}