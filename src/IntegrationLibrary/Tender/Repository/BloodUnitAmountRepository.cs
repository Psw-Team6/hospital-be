﻿using IntegrationLibrary.Settings;
using IntegrationLibrary.Tender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Tender.Repository
{
    public class BloodUnitAmountRepository :IBloodUnitAmountRepository
    {
        private readonly IntegrationDbContext _context;

        public BloodUnitAmountRepository(IntegrationDbContext context)
        {
            _context = context;
        }
        public void Create(Model.BloodUnitAmount bloodUnitAmount)
        {
            _context.BloodUnitAmounts.Add(bloodUnitAmount);
            _context.SaveChanges();
        }

        public IEnumerable<Model.BloodUnitAmount> GetAll()
        {
            return _context.BloodUnitAmounts.ToList();
        }

        public IEnumerable<BloodUnitAmount> GetAllByTenderId(Guid tenderId)
        {
            List<BloodUnitAmount> allBloodUnitAmounts = _context.BloodUnitAmounts.ToList();
            List<BloodUnitAmount> bloodUnitAmounts = new List<BloodUnitAmount>();
            foreach (BloodUnitAmount bloodUnitAmount in allBloodUnitAmounts)
            {
                if (bloodUnitAmount.TenderId == tenderId)
                {
                    bloodUnitAmounts.Add(bloodUnitAmount);
                }
            }
            return bloodUnitAmounts;
        }

        public Model.BloodUnitAmount GetById(Guid id)
        {
            return _context.BloodUnitAmounts.Find(id);
        }

    }
}
