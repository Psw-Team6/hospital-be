using IntegrationLibrary.BloodBank.Service;
using IntegrationLibrary.BloodRequests.Model;
using IntegrationLibrary.BloodRequests.Repository;
using IntegrationLibrary.HTTP;
using System;
using System.Collections.Generic;
using IntegrationLibrary.BloodBank;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodRequests.Service
{
    public class BloodRequestService : IBloodRequestService
    {
        private readonly IBloodRequestRepository _bloodRequestRepository;
        private readonly IHttpService _httpService;
        private readonly IBloodBankService _bloodBankService;

        public BloodRequestService(IBloodRequestRepository bloodRequestRepository, IHttpService httpService, IBloodBankService bloodBankService)
        {
            _bloodRequestRepository = bloodRequestRepository;
            _httpService = httpService;
            _bloodBankService = bloodBankService;
        }
        public void Create(BloodRequest request)
        {
            _bloodRequestRepository.Create(request);
        }

        public void Delete(BloodRequest request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BloodRequest> GetAll()
        {
            return _bloodRequestRepository.GetAll();
        }

        public BloodRequest GetFirst()
        {
            return _bloodRequestRepository.GetFirst();
        }

        public BloodRequest GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(BloodRequest request)
        {
            _bloodRequestRepository.Update(request);
        }

        public IEnumerable<BloodRequest> GetAllOnPending()
        {
            return _bloodRequestRepository.GetAllOnPending();
        }

        public IEnumerable<BloodRequest> GetAllReturned(string doctorUsername)
        {
            return _bloodRequestRepository.GetAllReturned(doctorUsername);
        }

        public bool IfOnDemandRequest(BloodRequest request)
        {
            /*if (DateTime.Now.AddDays(3) < request.Date)
            {
                //pozovi funkciju schedule
                return false;
            }
            else
            {
                //pozovi funkciju on demand
                return true;
            }*/
            if (DateTime.Compare(DateTime.Now, request.Date) > 0)
            {
                //pozovi funkciju schedule
                return true;
            }
            else
            {
                //pozovi funkciju on demand
                return false;
            }
        }

        //METODA KOJA TREBA DA SE POZOVE SVAKI DAN (POZIVA SE VISE PUTA SVE DOK NE VRATI NULL)
        /*public async Task<BloodSupplyResponse> sendScheduledRequest()
        {
            List<BloodRequest> requestTodayList = scheduledRequestsForToday();
            if(requestTodayList.Count > 0)
            {
                requestTodayList[0].Status = Status.SENT;
                Update(requestTodayList[0]);
                return await _httpService.GetProductAsync(requestTodayList[0].BloodBank.ServerAddress + "blood/" + requestTodayList[0].BloodBank.Name + "/" + requestTodayList[0].Type + '/' + requestTodayList[0].Amount);
            }

            return null;
        }*/

        public void sendScheduledRequest()
        {
            List<BloodRequest> requestTodayList = scheduledRequestsForToday();
            if (requestTodayList == null)
            {
                return;
            }
            if(requestTodayList.Count > 0)
            {
                requestTodayList[0].Status = Status.SENT;
                Update(requestTodayList[0]);
                IntegrationLibrary.BloodBank.BloodBank bloodBank = _bloodBankService.GetById(requestTodayList[0].BloodBankId);
                _httpService.GetProductAsync(bloodBank.ServerAddress + "blood/" + bloodBank.Name + "/" + requestTodayList[0].Type + '/' + requestTodayList[0].Amount);
            }
        }

        public List<BloodRequest> scheduledRequestsForToday()
        {
            List<BloodRequest> requestList = (List<BloodRequest>)_bloodRequestRepository.GetAll();
            List<BloodRequest> requestTodayList = new List<BloodRequest>();
            foreach (BloodRequest request in requestList)
            {
                if (request.Status == Status.APPPROVED)
                {
                    if (IfOnDemandRequest(request))
                    {
                        requestTodayList.Add(request);
                    }
                }
            }

            return requestTodayList;
        }

    }
}
