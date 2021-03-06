using DataModel.BE;
using DataModel.BLL;
using DataModel.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSUnitTestProject
{
    [TestClass]
    public class MSCustomerManagerTest
    {
        private Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
        private Mock<IRepository<int, Customer>> customerRepoMock = new Mock<IRepository<int, Customer>>();

        private static int nextId = 1;

        public MSCustomerManagerTest()
        {
            customerRepoMock.Setup(x => x.Add(It.IsAny<Customer>())).Returns<Customer>((c) => { c.Id = nextId++; customers.Add(c.Id, c); return c; });
            customerRepoMock.Setup(x => x.Update(It.IsAny<Customer>())).Callback<Customer>(c => customers[c.Id] = c);
            customerRepoMock.Setup(x => x.Remove(It.IsAny<Customer>())).Callback<Customer>(c => customers.Remove(c.Id));
            customerRepoMock.Setup(x => x.GetAll()).Returns(() => customers.Values.ToList());
            customerRepoMock.Setup(x => x.GetById(It.IsAny<int>())).Returns<int>(id => customers.ContainsKey(id) ? customers[id] : null);
        }


        [TestMethod]
        public void CreateCustomerManagerValid()
        {
            IRepository<int, Customer> repo = customerRepoMock.Object;
            CustomerManager cm = new CustomerManager(repo);
            Assert.IsTrue(customers.Count == 0);
        }

        [TestMethod]
        public void CreateCustomerManagerRepositoryIsNullExpectArgumentException()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => { CustomerManager cm = new CustomerManager(null); });
            Assert.AreEqual("Missing Customer Repository", ex.Message);
        }

        [TestMethod]
        public void AddCustomer()
        {
            IRepository<int, Customer> repo = customerRepoMock.Object;
            CustomerManager cm = new CustomerManager(repo);
            Customer c = new Customer("Name");
            int localId = nextId;
            c = cm.Add(c);
            Assert.IsTrue(customers.Count == 1);
            Assert.AreEqual(localId, c.Id);
            Assert.IsTrue(IsIdenticalCustomer(c, customers[c.Id]));
        }

        [TestMethod]
        public void UpdateCustomer()
        {
            IRepository<int, Customer> repo = customerRepoMock.Object;
            CustomerManager cm = new CustomerManager(repo);
            Customer c = new Customer("Name");
            c = cm.Add(c);

            string newName = "NewName";
            string newEmail = "NewEmail";
            string newPhone = "NewPhonw";

            c.Name = newName;
            c.Email = newEmail;
            c.Phone = newPhone;
            cm.Update(c);
            Assert.IsTrue(IsIdenticalCustomer(c, customers[c.Id]));
        }

        [TestMethod]
        public void RemoveCustomer()
        {
            IRepository<int, Customer> repo = customerRepoMock.Object;
            CustomerManager cm = new CustomerManager(repo);
            Customer c1 = new Customer("Name1");
            Customer c2 = new Customer("Name2");

            c1 = cm.Add(c1);
            c2 = cm.Add(c2);

            cm.Remove(c1);
            Assert.IsTrue(customers.Count == 1);
            Assert.IsTrue(IsIdenticalCustomer(c2, customers[c2.Id]));
        }

        [TestMethod]
        public void GetAllCustomersEmptyList()
        {
            IRepository<int, Customer> repo = customerRepoMock.Object;
            CustomerManager cm = new CustomerManager(repo);
            var result = cm.GetAll();
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void GetAllCustomersNonEmptyList()
        {
            IRepository<int, Customer> repo = customerRepoMock.Object;
            CustomerManager cm = new CustomerManager(repo);
            Customer c1 = new Customer("Name1");
            Customer c2 = new Customer("Name2");

            c1 = cm.Add(c1);
            c2 = cm.Add(c2);

            var result = cm.GetAll();

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(c1));
            Assert.IsTrue(result.Contains(c2));
        }

        [TestMethod]
        public void getByIDExistingCustomer()
        {
            IRepository<int, Customer> repo = customerRepoMock.Object;
            CustomerManager cm = new CustomerManager(repo);
            Customer c1 = new Customer("Name1");
            Customer c2 = new Customer("Name2");

            c1 = cm.Add(c1);
            c2 = cm.Add(c2);

            Customer result = cm.GetById(c2.Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(IsIdenticalCustomer(c2, result));
        }

        [TestMethod]
        public void getByIDNonExistingCustomerExpectNull()
        {
            IRepository<int, Customer> repo = customerRepoMock.Object;
            CustomerManager cm = new CustomerManager(repo);
            Customer c1 = new Customer("Name1");
            Customer c2 = new Customer("Name2");

            c1 = cm.Add(c1);
            c2 = cm.Add(c2);
            cm.Remove(c2);

            Customer result = cm.GetById(c2.Id);

            Assert.IsNull(result);
        }



        private bool IsIdenticalCustomer(Customer c1, Customer c2)
        {
            return (c1.Id == c2.Id && c1.Name == c2.Name && c1.Email == c2.Email && c1.Phone == c2.Phone);
        }
    }
}
