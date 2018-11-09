using DataModel.BE;
using DataModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.BLL
{
    public class CustomerManager
    {
        private IRepository<int, Customer> customers;

        public CustomerManager(IRepository<int, Customer> repo)
        {
            customers = repo ?? throw new ArgumentException("Missing Customer Repository");
        }

        public Customer Add(Customer customer)
        {
            return customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            customers.Update(customer);
        }

        public void Remove(Customer customer)
        {
            customers.Remove(customer);
        }

        public IList<Customer> GetAll()
        {
            return customers.GetAll();
        }

        public Customer GetById(int id)
        {
            return customers.GetById(id);
        }
    }
}
