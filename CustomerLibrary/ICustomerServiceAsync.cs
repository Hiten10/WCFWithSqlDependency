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
    public interface ICustomerServiceAsync
    {
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginGetAllCustomer(AsyncCallback callback, object asyncState);
        void EndGetAllCustomer(IAsyncResult result);
    }
}
