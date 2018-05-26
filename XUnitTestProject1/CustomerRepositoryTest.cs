using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TestWebApp.Data.Interfaces;
using TestWebApp.Data.Model;
using Xunit;

namespace XUnitTestProject1
{
    public class CustomerRepositoryTest
    {
        [Fact]
        public void Create_Customer_Calls_CustomerRepositoryCreate()
        {
            var customer = new Customer
            {
                CustomerId = 1,
                Name = "DADA",
                Money = 100
            };

            var mock = new Mock<ICustomerRepository>();
            mock.Setup(foo => foo.Create(customer));
            mock.Object.Create(customer);
            mock.Setup(foo => foo.Create(customer));
            mock.Verify(x => x.Create(customer), Times.Once());

        }

        [Fact]
        public void Get_Customer_By_Id()
        {
            var customer = new Customer
            {
                CustomerId = 1,
                Name = "DADA",
                Money = 100
            };

            var mock = new Mock<ICustomerRepository>();

            mock.Setup(x => x.getById(1))
            .Returns(customer);
            Assert.Equal(mock.Object.getById(1), customer);
            mock.Verify(x => x.getById(customer.CustomerId), Times.Once());
        }

        [Fact]
        public void Delete_Customer_By_Id()
        {
            var customer = new Customer
            {
                CustomerId = 1,
                Name = "DADA",
                Money = 100
            };

            var mock = new Mock<ICustomerRepository>();

            mock.Setup(x => x.Delete(customer));
            mock.Object.Delete(customer);

            mock.Setup(x => x.getById(1));
            Assert.Null(mock.Object.getById(1));
            mock.Verify(x => x.Delete(customer), Times.Once());
        }
    }
}
