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

        public TenderService(ITenderRepository tenderRepository)
        {
            this.tenderRepository = tenderRepository;
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
            return tenderRepository.GetById(id);
        }

        public void Update(Model.Tender tender)
        {
            tenderRepository.Update(tender);
        }

       
        
    }
}
