using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    class Program
    {
        internal static ServiceHost MyServiceHost = null;

        static void Main(string[] args)
        {
            StartService();
        }

       

        internal static void StartService()
        {
            //Uri httpbaseAddress = new Uri("http://localhost:8087/CustomerService/");

            //Uri[] baseAdresses = { httpbaseAddress };

            //MyServiceHost = new ServiceHost(typeof(CustomerServiceLibrary.CustomerService));
            //MyServiceHost.AddServiceEndpoint(typeof(CustomerServiceLibrary.ICustomerService), new WSDualHttpBinding(), httpbaseAddress);
            MyServiceHost = new ServiceHost(typeof(CustomerServiceLibrary.CustomerService));

            ServiceDescription serviceDesciption = MyServiceHost.Description;

            foreach (ServiceEndpoint endpoint in serviceDesciption.Endpoints)
            {

                Console.WriteLine("Endpoint - address:  {0}", endpoint.Address);
                Console.WriteLine("         - binding name:\t\t{0}", endpoint.Binding.Name);
                Console.WriteLine("         - contract name:\t\t{0}", endpoint.Contract.Name);
                Console.WriteLine();

            }

            MyServiceHost.Open();
            Console.WriteLine("Press enter to stop.");
            Console.ReadKey();

            if (MyServiceHost.State != CommunicationState.Closed)
                MyServiceHost.Close();

        }
    }
}
