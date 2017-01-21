using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CustomerServiceLibrary
{
    [DataContract]
    public class Customer
    {
        [DataMember]
        public string Name
        {
            get;
            set;
        }


    }
}
