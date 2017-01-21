using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CustomerServiceLibrary
{
    public class CustomerService : ICustomerService, IDisposable
    {

        List<Customer>_customers = null;
        ICustomerServiceCallback _callback;
        Customer _customer = null;

        private const string Constr = "Data Source=.;Initial Catalog=TestDB;Persist Security Info=True;User ID=sa;Password=test123";

        public CustomerService()
        {
            _callback = OperationContext.Current.GetCallbackChannel<ICustomerServiceCallback>();
            SqlDependency.Start(Constr);
        }

        //public void MyMethod()
        //{
        //    _callback = OperationContext.Current.GetCallbackChannel<ICustomerServiceCallback>();
        //    _callback.Callback();
        //}

        public void GetAllCustomer()
        {

            using (var con = new SqlConnection(Constr))
            {
                using (var cmd = new SqlCommand())
                {
                    con.Open();

                    cmd.Connection = con;
                    cmd.CommandText = "GetHero";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Notification = null;

                    // Create the associated SqlDependency
                    var dep = new SqlDependency(cmd);
                    dep.OnChange += new OnChangeEventHandler(dep_OnChange);

                    SqlDataReader dr = cmd.ExecuteReader();
                    _customers = new List<Customer>();
                    while (dr.Read())
                    {
                        _customer = new Customer {Name = dr.GetString(0)};

                        _customers.Add(_customer);

                    }
                }

                _callback.Callback(_customers.ToArray());


            }

        }

        void dep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            try
            {

                if (e.Type == SqlNotificationType.Change)
                {
                    GetAllCustomer();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void Dispose()
        {
            SqlDependency.Stop(Constr);
        }
    }
}
