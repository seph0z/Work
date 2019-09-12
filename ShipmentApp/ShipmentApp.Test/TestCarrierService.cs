using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShipmentApp.Data.EntityFramework;
using ShipmentApp.Domain.Contracts.ViewModels;
using ShipmentApp.Domain.Services;
using ShipmentApp.Domain.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipmentApp.Test
{
    [TestClass]
    public class TestCarrierService
    {
        private CarrierViewModel carrier = new CarrierViewModel { Id = 10, Name = "Test Name" };

        [TestMethod]
        public void Get_CarrierExist_ShouldReturn()
        {
            using(var context = InitializeContext("Get_CarrierExist_ShouldReturn"))
            {
                var service = new CarrierService(context);
                service.Create(carrier);

                var result = service.Get(carrier.Id);

                Assert.IsNotNull(result);
                Assert.AreEqual(carrier.Name, result.Name);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(StatusCodeException))]
        public void Get_CarrierNotFound_ExpectedException()
        {
            using (var context = InitializeContext("Get_CarrierNotFound_ExpectedException"))
            {
                var service = new CarrierService(context);
                service.Create(carrier);

                var result = service.Get(35);
            }
        }

        [TestMethod]
        public void Create_CreateCarrier_ShouldReturn()
        {
            using (var context = InitializeContext("Create_CreateCarrier_ShouldReturn"))
            {
                var service = new CarrierService(context);
                service.Create(carrier);

                var result = service.Get(carrier.Id);

                Assert.IsNotNull(result);
                Assert.AreEqual(carrier.Name, result.Name);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(StatusCodeException))]
        public void Create_CarrierDoesNotNull_ExpecterdException()
        {
            using (var context = InitializeContext("Create_CarrierDoesNotNull_ExpecterdException"))
            {
                var service = new CarrierService(context);
                service.Create(null);
            }
        }

        [TestMethod]
        public void Update_UpdateCarrier_ShouldBeChanged()
        {
            using (var context = InitializeContext("Update_UpdateCarrier_ShouldBeChanged"))
            {
                var service = new CarrierService(context);
                service.Create(carrier);

                var subject = service.Get(carrier.Id);
                const string name = "New Name";
                subject.Name = name;
                service.Update(subject);
                var result = service.Get(carrier.Id);

                Assert.IsNotNull(result);
                Assert.AreEqual(name, result.Name);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(StatusCodeException))]
        public void Update_CarrierNotFound_ExpectedException()
        {
            using (var context = InitializeContext("Update_CarrierNotFound_ExpectedException"))
            {
                var service = new CarrierService(context);
                service.Create(carrier);

                var subject = service.Get(35);
                subject.Name = "New Name";
                service.Update(subject);
            }
        }

        [TestMethod]
        public void Delete_DeleteCarrier_NoException()
        {
            using (var context = InitializeContext("Delete_DeleteCarrier_NoException"))
            {
                var service = new CarrierService(context);
                service.Create(carrier);

                service.Delete(carrier.Id);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(StatusCodeException))]
        public void Delete_CarriertNotFound_ExpectedException()
        {
            using (var context = InitializeContext("Delete_CarriertNotFound_ExpectedException"))
            {
                var service = new CarrierService(context);
                service.Delete(35);
            }
        }

        [TestMethod]
        public void GetAll_CallGetAll_ShouldReturnList()
        {
            using (var context = InitializeContext("GetAll_CallGetAll_ShouldReturnList"))
            {
                var service = new CarrierService(context);

                service.Create(new CarrierViewModel { Name = "Name1" });
                service.Create(new CarrierViewModel { Name = "Name2" });
                service.Create(new CarrierViewModel { Name = "Name3" });
                service.Create(new CarrierViewModel { Name = "Name4" });

                var result = service.GetAll().ToList();

                Assert.AreEqual(4, result.Count);
            }
        }

        private AppDbContext InitializeContext(string methodName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: methodName)
                .Options;

            return new AppDbContext(options);
        }
    }
}
