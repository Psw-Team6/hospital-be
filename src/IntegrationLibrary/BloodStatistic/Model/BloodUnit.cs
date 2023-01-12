using IntegrationLibrary.BloodRequests.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.BloodStatistic.Model
{
    public class BloodUnit
    {
        private Guid _id;
        private BloodType _bloodType;
        private int _amount;
        private String _bloodBankName;
        private IEnumerable<String> _consumptions;
        private DateTime _date; //date of acquisition
        private String _source;

        public BloodUnit(IEnumerable<string> consumptions, int amount, Guid id, BloodType bloodType, string bloodBankName, DateTime date, string source)
        {
            Consumptions = consumptions;
            Amount = amount;
            Id = id;
            BloodType = bloodType;
            BloodBankName = bloodBankName;
            Date = date;
            Source = source;
        }

        public void decreseAmount(int consumptionAmount)
        {
            if (isValidToDecrese(consumptionAmount))
                Amount -= consumptionAmount;
        }

        private bool isValidToDecrese(int consumptionAmount)
        {
            return consumptionAmount <= Amount ? true : false;
        }

        public IEnumerable<String> Consumptions
        {
            get => _consumptions;
            set => _consumptions = value;
        }

        public int Amount
        {
            get => _amount;
            private set => _amount = value;
        }

        public Guid Id
        {
            get => _id;
            private set => _id = value;
        }

        public BloodType BloodType
        {
            get => _bloodType;
            private set => _bloodType = value;
        }

        public string BloodBankName
        {
            get => _bloodBankName;
            private set => _bloodBankName = value;
        }

        public DateTime Date
        {
            get => _date;
            private set => _date = value;
        }

        public string Source
        {
            get => _source;
            private set => _source = value;
        }

    }
}
