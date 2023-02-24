using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace DeviceTwinBackend
{
    ///// we want to change location of device
    class Program
    {
        static RegistryManager registryManager;

        // CHANGE THE CONNECTION STRING TO THE ACTUAL CONNETION STRING OF THE IOT HUB (SERVICE POLICY) 
        // changes device twin of a device
        static string connectionString="HostName=vaishali-iot.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=0D7s1MdJgDmdAZp3vxK6XdAhQsHUtfvJnvqPf/HIcBE=";        
        
        //changes device twin of a specific device
        public static async Task SetDeviceTags()  {
            var twin=await registryManager.GetTwinAsync("iotdevice1"); //returns devicetwin of a specific device
            var patch=
                @"{
                        tags: { 
                            location:  {
                                country: 'France',
                                city: 'Paris'
                            }
                        },
                        properties: {
                            desired:  {
                                FPS: 45
                            }
                        }
                    }";
            await registryManager.UpdateTwinAsync(twin.DeviceId, patch, twin.ETag); // update the device twin using above method

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Starting Device Twin backend...");
            registryManager=RegistryManager.CreateFromConnectionString("connectionString");
            SetDeviceTags().Wait();
            Console.WriteLine("Hit Enter to exit...");
            Console.ReadLine(); 
        }
    }
}
