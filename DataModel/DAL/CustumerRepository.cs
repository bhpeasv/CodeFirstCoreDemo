using CodeFirstCoreDemo.DAL;
using DataModel.BE;
using DataModel.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataModel.DAL
{
    public class CustumerRepository : IRepository<int, Customer>
    {
        private CustomerContext db = new CustomerContext();

        public Customer Add(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
            return customer;
        }

        public IList<Customer> GetAll()
        {
            return db.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return db.Customers.FirstOrDefault(c => c.Id == id);
        }

        public void Remove(Customer customer)
        {
            db.Customers.Remove(customer);
            db.SaveChanges();
        }

        public void Update(Customer customer)
        {
            var cust = GetById(customer.Id);
            if (cust != null)
            {
                cust = customer;
                db.SaveChanges();
            }

        }
    }
}
