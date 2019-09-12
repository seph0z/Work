using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShipmentApp.Data.EntityFramework;
using ShipmentApp.Domain.Contracts.ViewModels;
using ShipmentApp.Domain.Services;
using System;
using Microsoft.EntityFrameworkCore;
using ShipmentApp.Domain.Services.Exceptions;
using System.Linq;
using Mapster;
using System.Collections;
using System.Collections.Generic;
using ShipmentApp.Domain.Contracts;
using Moq;
using ShipmentApp.Domain.Services.Observers;

namespace ShipmentApp.Test
{
    [TestClass]
    public class TestShipmentService
    {
        private readonly Guid testGuid = Guid.Parse("65bd0443-0b34-4318-860d-bfde1723b0d9");

        [TestMethod]
        public void Retrieve_ShipmentExist_ShouldReturn()
        {
            using (var context = InitializeContext("Retrieve_ShipmentExist_ShouldReturn"))
            {
                var mock = new Mock<Tracker<ShipmentViewModel>>();

                var service = new ShipmentService(context, mock.Object);
                service.Create(new ShipmentViewModel { Id = testGuid, Description = "Test Description" });

                var result = service.Retrieve(testGuid);

                Assert.IsNotNull(result);
                Assert.AreEqual("Test Description", result.Description);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(StatusCodeException))]
        public void Retrieve_ShipmentNotFound_ExpectedException()
        {
            using (var context = InitializeContext("Retrieve_ShipmentNotFound_ExpectedException"))
            {
                var mock = new Mock<Tracker<ShipmentViewModel>>();

                var service = new ShipmentService(context, mock.Object);
                service.Create(new ShipmentViewModel { Id = Guid.NewGuid(), Description = "Test Description" });

                var result = service.Retrieve(testGuid);
            }
        }

        [TestMethod]
        public void Create_CreateShipment_ShouldReturn()
        {
            using (var context = InitializeContext("Create_CreateShipment_ShouldReturn"))
            {
                var mock = new Mock<Tracker<ShipmentViewModel>>();

                var service = new ShipmentService(context, mock.Object);
                const string city = "Minsk";
                service.Create(new ShipmentViewModel { Id = testGuid, RecipientAddress = new AddressViewModel { City = city } });

                var result = service.Retrieve(testGuid);

                Assert.IsNotNull(result);
                Assert.AreEqual(city, result.RecipientAddress.City);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(StatusCodeException))]
        public void Create_ShipmentDoesNotNull_ExpecterdException()
        {
            using (var context = InitializeContext("Create_ShipmentDoesNotNull_ExpecterdException"))
            {
                var mock = new Mock<Tracker<ShipmentViewModel>>();

                var service = new ShipmentService(context, mock.Object);
                service.Create(null);
            }
        }

        [TestMethod]
        public void Update_UpdateShipment_ShouldBeChanged()
        {
            using (var context = InitializeContext("Update_UpdateShipment_ShouldBeChanged"))
            {
                var mock = new Mock<Tracker<ShipmentViewModel>>();

                var service = new ShipmentService(context, mock.Object);
                service.Create(new ShipmentViewModel { Id = testGuid, Description = "Test Description" });

                var subject = service.Retrieve(testGuid);
                const string description = "New Description";
                subject.Description = description;
                service.Update(subject);
                var result = service.Retrieve(testGuid);

                Assert.IsNotNull(result);
                Assert.AreEqual(description, result.Description);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(StatusCodeException))]
        public void Update_ShipmentNotFound_ExpectedException()
        {
            using (var context = InitializeContext("Update_ShipmentNotFound_ExpectedException"))
            {
                var mock = new Mock<Tracker<ShipmentViewModel>>();

                var service = new ShipmentService(context, mock.Object);
                service.Create(new ShipmentViewModel { Id = Guid.NewGuid(), Description = "Test Description" });

                var subject = service.Retrieve(testGuid);
                subject.Description = "New Description";
                service.Update(subject);
            }
        }

        [TestMethod]
        public void Delete_DeleteShipment_NoException()
        {
            using (var context = InitializeContext("Delete_DeleteShipment_NoException"))
            {
                var mock = new Mock<Tracker<ShipmentViewModel>>();

                var service = new ShipmentService(context, mock.Object);
                service.Create(new ShipmentViewModel { Id = testGuid, Description = "Test Description" });

                service.Delete(testGuid);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(StatusCodeException))]
        public void Delete_ShipmentNotFound_ExpectedException()
        {
            using (var context = InitializeContext("Delete_ShipmentNotFound_ExpectedException"))
            {
                var mock = new Mock<Tracker<ShipmentViewModel>>();

                var service = new ShipmentService(context, mock.Object);
                service.Delete(Guid.NewGuid());
            }
        }

        [TestMethod]
        public void RetrieveAll_CallRetrieveAll_ShouldReturnList()
        {
            using (var context = InitializeContext("RetrieveAll_CallRetrieveAll_ShouldReturnList"))
            {
                var mock = new Mock<Tracker<ShipmentViewModel>>();

                var service = new ShipmentService(context, mock.Object);

                service.Create(new ShipmentViewModel { Id = Guid.NewGuid(), Description = "Test Description1" });
                service.Create(new ShipmentViewModel { Id = Guid.NewGuid(), Description = "Test Description2" });
                service.Create(new ShipmentViewModel { Id = Guid.NewGuid(), Description = "Test Description3" });
                service.Create(new ShipmentViewModel { Id = Guid.NewGuid(), Description = "Test Description4" });

                var result = context.Shipments;

                Assert.AreEqual(4, result.Count());
            }
        }

        private AppDbContext InitializeContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new AppDbContext(options);
        }
    }
}
