using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CustomerServiceLibrary
{
    [ServiceContract(Name = "ICustomerService",
        CallbackContract = typeof(CustomerServiceLibrary.ICustomerServiceCallback),
        SessionMode = SessionMode.Required)]
    public interface ICustomerService
    {
        [OperationContract(IsOneWay = true)]
        void GetAllCustomer();
    }
}
