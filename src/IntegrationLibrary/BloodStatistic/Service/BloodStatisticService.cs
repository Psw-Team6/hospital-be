using IntegrationLibrary.BloodBank.Repository;
using IntegrationLibrary.BloodStatistic.Model;
using IntegrationLibrary.Enums;
using IntegrationLibrary.Tender.Model;
using IntegrationLibrary.Tender.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodStatistic.Service
{
    public class BloodStatisticService : IBloodStatisticService
    {
        private readonly ITenderService tenderService;
        private readonly IBloodBankRepository bloodBankRepository;

        public BloodStatisticService(ITenderService tenderService, IBloodBankRepository bloodBankRepository)
        {
            this.tenderService = tenderService;
            this.bloodBankRepository = bloodBankRepository;
        }

        public List<BloodStatisticResponse> getTenderStatistic(DateRange range)
        {
            List<BloodStatisticResponse> response = new List<BloodStatisticResponse>();
            List<Tender.Model.Tender> allInRange = tenderService.GetAllTenders();


            foreach (Tender.Model.Tender tender in allInRange)
            {

                tender.BloodUnitAmount = tenderService.GetBloodUnitAmounts(tender.Id);
                if (tender.DeadlineDate.Ticks >= range.From.Ticks && tender.DeadlineDate.Ticks <= range.To.Ticks)
                {
                    if (tender.Status == StatusTender.Close)
                    {
                        var matches = response.Where(p => p.BloodBankID == bloodBankRepository.GetByName(tender.Winner.BloodBankName).Id).ToList();
                        if (matches.Count == 0)
                        {
                            BloodStatisticResponse res = new BloodStatisticResponse();
                            res.BloodBankID = bloodBankRepository.GetByName(tender.Winner.BloodBankName).Id;
                            res.DateRange = range;
                            foreach (BloodUnitAmount bu in tender.BloodUnitAmount)
                            {
                                switch (bu.BloodType)
                                {
                                    case BloodRequests.Model.BloodType.ABneg:
                                        res.ABneg += bu.Amount;
                                        break;
                                    case BloodRequests.Model.BloodType.ABpos:
                                        res.ABpos += bu.Amount;
                                        break;
                                    case BloodRequests.Model.BloodType.Apos:
                                        res.Apos += bu.Amount;
                                        break;
                                    case BloodRequests.Model.BloodType.Aneg:
                                        res.Aneg += bu.Amount;
                                        break;
                                    case BloodRequests.Model.BloodType.Bpos:
                                        res.Bpos += bu.Amount;
                                        break;
                                    case BloodRequests.Model.BloodType.Bneg:
                                        res.Bneg += bu.Amount;
                                        break;
                                    case BloodRequests.Model.BloodType.Opos:
                                        res.Opos += bu.Amount;
                                        break;
                                    case BloodRequests.Model.BloodType.Oneg:
                                        res.Oneg += bu.Amount;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            response.Add(res);
                        }
                        else
                        {
                            foreach (BloodStatisticResponse res in response.ToArray())
                            {
                                if (res.BloodBankID == bloodBankRepository.GetByName(tender.Winner.BloodBankName).Id)
                                {
                                    foreach (BloodUnitAmount bu in tender.BloodUnitAmount)
                                    {
                                        switch (bu.BloodType)
                                        {
                                            case BloodRequests.Model.BloodType.ABneg:
                                                res.ABneg += bu.Amount;
                                                break;
                                            case BloodRequests.Model.BloodType.ABpos:
                                                res.ABpos += bu.Amount;
                                                break;
                                            case BloodRequests.Model.BloodType.Apos:
                                                res.Apos += bu.Amount;
                                                break;
                                            case BloodRequests.Model.BloodType.Aneg:
                                                res.Aneg += bu.Amount;
                                                break;
                                            case BloodRequests.Model.BloodType.Bpos:
                                                res.Bpos += bu.Amount;
                                                break;
                                            case BloodRequests.Model.BloodType.Bneg:
                                                res.Bneg += bu.Amount;
                                                break;
                                            case BloodRequests.Model.BloodType.Opos:
                                                res.Opos += bu.Amount;
                                                break;
                                            case BloodRequests.Model.BloodType.Oneg:
                                                res.Oneg += bu.Amount;
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    continue;
                }
            }
            return response;
        }

        public List<BloodStatisticResponse> getUrgentStatistic(DateRange range, List<BloodUnit> units)
        {
            List<BloodStatisticResponse> response = new List<BloodStatisticResponse>();

            foreach (BloodUnit unit in units)
            {
                if (unit.Date.Ticks >= range.From.Ticks && unit.Date.Ticks <= range.To.Ticks)
                {
                    var matches = response.Where(p => p.BloodBankID == bloodBankRepository.GetByName(unit.BloodBankName).Id).ToList();
                    if (matches.Count == 0)
                    {
                        BloodStatisticResponse res = new BloodStatisticResponse();
                        res.BloodBankID = bloodBankRepository.GetByName(unit.BloodBankName).Id;
                        res.DateRange = range;
                        switch (unit.BloodType)
                        {
                            case BloodRequests.Model.BloodType.ABneg:
                                res.ABneg += unit.Amount;
                                break;
                            case BloodRequests.Model.BloodType.ABpos:
                                res.ABpos += unit.Amount;
                                break;
                            case BloodRequests.Model.BloodType.Apos:
                                res.Apos += unit.Amount;
                                break;
                            case BloodRequests.Model.BloodType.Aneg:
                                res.Aneg += unit.Amount;
                                break;
                            case BloodRequests.Model.BloodType.Bpos:
                                res.Bpos += unit.Amount;
                                break;
                            case BloodRequests.Model.BloodType.Bneg:
                                res.Bneg += unit.Amount;
                                break;
                            case BloodRequests.Model.BloodType.Opos:
                                res.Opos += unit.Amount;
                                break;
                            case BloodRequests.Model.BloodType.Oneg:
                                res.Oneg += unit.Amount;
                                break;
                            default:
                                break;
                        }
                        response.Add(res);
                    }
                    else
                    {
                        foreach (BloodStatisticResponse res in response.ToArray())
                        {
                            if (res.BloodBankID == bloodBankRepository.GetByName(unit.BloodBankName).Id)
                            {
                                switch (unit.BloodType)
                                {
                                    case BloodRequests.Model.BloodType.ABneg:
                                        res.ABneg += unit.Amount;
                                        break;
                                    case BloodRequests.Model.BloodType.ABpos:
                                        res.ABpos += unit.Amount;
                                        break;
                                    case BloodRequests.Model.BloodType.Apos:
                                        res.Apos += unit.Amount;
                                        break;
                                    case BloodRequests.Model.BloodType.Aneg:
                                        res.Aneg += unit.Amount;
                                        break;
                                    case BloodRequests.Model.BloodType.Bpos:
                                        res.Bpos += unit.Amount;
                                        break;
                                    case BloodRequests.Model.BloodType.Bneg:
                                        res.Bneg += unit.Amount;
                                        break;
                                    case BloodRequests.Model.BloodType.Opos:
                                        res.Opos += unit.Amount;
                                        break;
                                    case BloodRequests.Model.BloodType.Oneg:
                                        res.Oneg += unit.Amount;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            return response;
        }
    }
}
