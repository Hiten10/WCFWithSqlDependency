using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CustomerServiceLibrary
{
    public interface ICustomerServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void Callback(Customer[] customers);
    }
   
}
