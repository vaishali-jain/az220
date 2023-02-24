using System;
using Microsoft.Azure.Devices.Client;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace device_messaging
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var certificate = new X509Certificate2(@"primary.pfx", "1234");
            var authentication = new DeviceAuthenticationWithX509Certificate("iotdevice4", certificate);

            DeviceClient Client = DeviceClient.Create("az220iothub1.azure-devices.net", 
                                                        authentication, 
                                                        Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            
            while (true)
            {
                Console.WriteLine("Enter Message to Send (Empty Message to exit)");
                var messageToSend = Console.ReadLine();

                Message message = new Message(Encoding.ASCII.GetBytes(messageToSend));
                Console.WriteLine("Sending Message {0}", messageToSend);
                await Client.SendEventAsync(message);
            }

        }
    }
}
