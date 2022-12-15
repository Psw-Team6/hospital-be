using IntegrationLibrary.Tender.Model;
using IntegrationLibrary.Tender.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

       
        
    }
}
