﻿using IntegrationLibrary.Tender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Tender.Service
{
    public interface ITenderService
    {
        public void Create(Model.Tender tender);
        public void Update(Model.Tender tender);
        public void Delete(Model.Tender tender);
        public Task<List<Model.Tender>> GetAll();
        public Model.Tender GetById(Guid id);
        public List<Model.Tender> GetAllTenders();
        public List<BloodUnitAmount> GetBloodUnitAmounts(Guid tenderID);
        public Task<List<Model.Tender>> GetClosedTenders();

        public Task<List<Model.Tender>> GetWinnerTenders();
    }
}
