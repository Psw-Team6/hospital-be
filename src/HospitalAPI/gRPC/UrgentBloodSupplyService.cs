using Grpc.Core;
using System;
using System.Threading.Tasks;
using HospitalLibrary.BloodUnits.Model;
using HospitalLibrary.BloodUnits.Service;

namespace HospitalAPI.gRPC
{
    public class UrgentBloodSupplyService: IUrgentBloodSupplyService
    {
        private Channel channel { get; set; }
        private UrgentBloodSupply.UrgentBloodSupplyClient client;
        private readonly BloodUnitService _bloodUnitService;

        public UrgentBloodSupplyService(BloodUnitService bloodUnitService)
        {
            _bloodUnitService = bloodUnitService;
        }

        public async Task OrderBloodUrgentlyAsync(string bloodType, int bloodAmount)
        {
            channel = new Channel("127.0.0.1:9091", ChannelCredentials.Insecure);
            client = new UrgentBloodSupply.UrgentBloodSupplyClient(channel);


            Response response = await client.orderBloodUrgentlyAsync(new Request() { BloodType = bloodType, Quantity = bloodAmount });
            if(response.BloodBankName != "")
            {
                BloodType bt = ConvertToBloodType(response.BloodType);
                BloodUnit bloodUnit = new BloodUnit(response.Quantity, bt, response.BloodBankName);

                await _bloodUnitService.Create(bloodUnit);
                Console.WriteLine("ODGOVOR:");
                Console.WriteLine(response.BloodBankName);
                Console.WriteLine(response.BloodType);
                Console.WriteLine(response.Quantity);
                
               
            }

            Console.WriteLine("ODGOVOR:");
            Console.WriteLine(response.BloodBankName);
            Console.WriteLine(response.BloodType);
            Console.WriteLine(response.Quantity);
            
        }

        public BloodType ConvertToBloodType(String bloodType)
        {
            if (bloodType =="Apos")
                return BloodType.Apos;
            else if (bloodType =="Aneg")
                return BloodType.Aneg;
            else if (bloodType=="Bpos")
                return BloodType.Bpos;
            else if (bloodType=="Bneg")
                return BloodType.Bneg;
            else if (bloodType=="ABpos")
                return BloodType.ABpos;
            else if (bloodType=="ABneg")
                return BloodType.ABneg;
            else if (bloodType=="Opos")
                return BloodType.Opos;
            else
                return BloodType.Oneg;
        }
    }
}