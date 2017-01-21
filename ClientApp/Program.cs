using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ClientApp.ServiceReference1;

namespace ClientApp
{
    class Program
    {
        delegate void Del();
        private delegate int Calculator(int x, int y, string opt);
        private delegate void GenericDel<in T>(T t); 
        static void Main(string[] args)
        {
            //var address = new EndpointAddress(new Uri("http://localhost:12016/CustomerService"));
            //var wsDualBinding = new WSDualHttpBinding();
            //var instanceContext = new InstanceContext(new MyCallBack());
            //var client = new DuplexChannelFactory<ICustomerService>(instanceContext, wsDualBinding, address).CreateChannel();
            
            //using (client as IDisposable)
            //{
            //    client.GetAllCustomer();
            //}

            //string str = "";
            //str.ToString("");
            //var list = new MyList<Person> {new Person {Name = "Hitendra"}};
            //IQueryable<Person> querableList = list.AsQueryable();

            //Person p = new Person();
            //p.FullName("");

            Method(new Concrete());

            Del d = new Del(SomeMethod);
            d += SomeMethod;
            d.Invoke();
            d();
            Action x1 = () => SomeMethod();
            x1.Invoke();
            x1();

            Calculator calc = new Calculator(Calculate);
            calc += Divide;
            calc += Calculate;
            calc += Calculate;
            calc(4, 5, "Multiply");
            //calc.Invoke(5, 5, "Add");
            //calc(10, 3, "Sub");

            Func<int, int, string, int> result = (int x, int y, string opt) => Calculate(5, 5, "Add");

            Console.WriteLine(result(5, 5, "Add"));
            Func<int, int> func7 = delegate(int x) { return x + 1; };
            func7 += (p) => p + 5;
            Func<int, int, int> func5 = (x, y) => x * y;
            Console.WriteLine(func7(5));
            Console.WriteLine(func5.Invoke(5,6));
            //***************************************
            // CoVariance - compatibility is preserved. 
            // func is object return type which is base class of all reference type
            // func6 return type is string which is derived from object
            // hence it will work
            //***************************************
            Func<string> func6 = delegate { return "Hello"; };
            Func<object> func = func6;
            //Func<string> func1 = func; //not possible

            //***************************************
            // ContraVariance - compatibility is reversed. 
            // action1 have input parameter as object which is base class of all reference type
            // action2 have input parameter as string which is derived from object
            // hence it will work. Since Contravariant is based on input type while covariance worked on return type.
            //***************************************
            Action<object> action1 = delegate(object s) { Console.WriteLine(s); };
            Action<string> action2 = action1;

            var gDel = new GenericDel<string>(SomeMethod);
            gDel("Overloaded");
            gDel += new Concrete().Method;


        }

        static int Calculate(int x, int y, string opt)
        {
            int result = 0;
            switch (opt)
            {
                case "Add":
                    result= x + y;
                    break;
                case "Sub":
                    result = x - y;
                    break;
                case "Multiply":
                    result = x*y;
                    break;
                default:
                    result=0;
                    break;
            }
            return result;

        }

        private static int Divide(int x, int y,string s)
        {
            return x/y;
        }

        static void SomeMethod()
        {
            Console.WriteLine("In Method");
           // return a;
        }

        static void SomeMethod(string s)
        {
            Console.WriteLine("In Method" + s);
        }

        static void Method(Base b)
        {
            Console.WriteLine(b.ToString());
        }
    }

    public class MyCallBack : ICustomerServiceCallback
    {

        public void Callback(Customer[] customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine(customer.Name);
            }
        }
        
    }

    public class MyList<T> : IEnumerable<T> where T:class
    {
        private readonly LinkedList<T> _list = new LinkedList<T>();
        public void Add(T item)
        {
            _list.AddLast(item);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in _list)
            {
                yield return item;
            }
        }


        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (var item in _list)
            {
                yield return item;
            }
        }
    }

    public static class MyExtension
    {
        public static string ToString(this string obj, string param)
        {
            return param;
        }

        public static string UppercaseFirstLetter(this string value)
        {
            //
            // Uppercase the first letter in the string.
            //
            if (value.Length > 0)
            {
                char[] array = value.ToCharArray();
                array[0] = char.ToUpper(array[0]);
                return new string(array);
            }
            return value;
        }

        public static string FullName(this Person obj, string name)
        {
            obj.Name = name;
            return obj.Name;
        }
    }

    public class Person
    {
        public string Name { get; set; }

        public string FullName()
        {
            return Name;
        }
    }

    public class Base
    {
        public void Method(){}
        public void Method(string s) { }
    }

    public class Concrete : Base
    {

    }
}
