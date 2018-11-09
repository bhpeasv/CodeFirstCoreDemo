using DataModel.BE;
using DataModel.BLL;
using DataModel.DAL;
using System;

namespace CodeFirstCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager mgr = new CustomerManager(new CustumerRepository());

            Customer c = new Customer("Jeppe");
            c = mgr.Add(c);
            Console.WriteLine("Customer saved with Id = " + c.Id);
        }
    }
}
