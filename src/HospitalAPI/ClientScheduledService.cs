using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace HospitalAPI
{
    public class ClientScheduledService : IHostedService
    {
        private System.Timers.Timer timer;
        //private GrpcChannel channel;
       private Channel channel { get; set; }
        private UrgentBloodSupply.UrgentBloodSupplyClient client;

        public ClientScheduledService() { }

        public Task StartAsync(CancellationToken cancellationToken)
        {
   
            //channel = GrpcChannel.ForAddress("https://127.0.0.1:9091");
            channel = new Channel("127.0.0.1:9091", ChannelCredentials.Insecure);
            //channel= new Channel("localhost", 9091, ChannelCredentials.Insecure);

            client = new UrgentBloodSupply.UrgentBloodSupplyClient(channel);
            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(SendMessage);
            timer.Interval = 3300; // number in miliseconds  
            timer.Enabled = true;
            return Task.CompletedTask;
        }


        private async void SendMessage(object source, ElapsedEventArgs e)
        {
            try
            {

              Response response = await client.orderBloodUrgentlyAsync(new Request() { BloodType = "Apos", Quantity = 1});

                Console.WriteLine("ODGOVOR:");
               Console.WriteLine(response.BloodBankName);
               Console.WriteLine(response.BloodType);
               Console.WriteLine(response.Quantity);

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.StackTrace);
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            channel?.ShutdownAsync();
            timer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
