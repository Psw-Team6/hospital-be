using IntegrationLibrary.Tender.Model;
using IntegrationLibrary.Tender.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntegrationLibrary.BloodStatistic.Model;

namespace IntegrationLibrary.Tender.Service
{
    public class TenderService : ITenderService
    {
        private readonly ITenderRepository tenderRepository;
        private readonly IBloodUnitAmountRepository amountRepository;

        public TenderService(ITenderRepository tenderRepository, IBloodUnitAmountRepository amountRepository)
        {
            this.tenderRepository = tenderRepository;
            this.amountRepository = amountRepository;
        }

        public void Create (Model.Tender tender)
        {
            tenderRepository.Create(tender);
        }

        public void Delete (Model.Tender tender)
        {
            tenderRepository.Delete(tender);
        }
        public async Task<List<Model.Tender>> GetAll()
        {
            return await tenderRepository.GetAll();
        }
        public Model.Tender GetById(Guid id)
        {
            Tender.Model.Tender tender = tenderRepository.GetById(id);
            List<BloodUnitAmount> bloodUnitAmounts = (List<BloodUnitAmount>)amountRepository.GetAllByTenderId(tender.Id);
            tender.BloodUnitAmount = bloodUnitAmounts;
            return tender;
        }
        

        public void Update(Model.Tender tender)
        {
            tenderRepository.Update(tender);
        }

        public async Task<List<Model.Tender>> GetClosedTenders()
        {
            return await tenderRepository.GetClosedTenders();
        }
        public List<Model.Tender> GetAllTenders()
        {
            return tenderRepository.GetAllTenders();
        }

        public List<Model.Tender> getAllInDateRange(DateRange dateRange)
        {
            List<Model.Tender> all = tenderRepository.GetAllTenders();
            List<Model.Tender> inRange = new List<Model.Tender>();

            foreach(Model.Tender tender in all)
            {
                if (dateRange.From <= tender.DeadlineDate && tender.DeadlineDate  <= dateRange.To)
                {
                    if (tender.Winner != null)
                    {
                        inRange.Add(tender);
                    }
                }
            }
            return inRange;
        }

        public List<BloodUnitAmount> GetBloodUnitAmounts(Guid tenderID)
        {
            return (List<BloodUnitAmount>)amountRepository.GetAllByTenderId(tenderID);
        }
        
        public async Task<List<Model.Tender>> GetWinnerTenders()
        {
            return await tenderRepository.GetWinnerTenders();
        }
        
    }
}
