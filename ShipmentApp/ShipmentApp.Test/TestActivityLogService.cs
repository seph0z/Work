using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShipmentApp.Data.EntityFramework;
using ShipmentApp.Domain.Contracts.ViewModels;
using ShipmentApp.Domain.Services;
using System.Linq;
using ShipmentApp.Domain.Services.Exceptions;

namespace ShipmentApp.Test
{
    [TestClass]
    public class TestActivityLogService
    {
        private static Guid guid = Guid.Parse("7a27f7da-63b6-40b6-aa0e-5983a186ff13");

        private static ActivityLogViewModel activityLog = new ActivityLogViewModel
        {
            Action = "Create",
            ShipmentId = guid,
            CarrierId = 10
        };

        [TestMethod]
        public void Create_CreateActivityLog_ShouldReturn()
        {
            using(var context = InitializeContext("Create_CreateActivityLog_ShouldReturn"))
            {
                var service = new ActivityLogService(context);

                ActivityLogViewModel activityLog = new ActivityLogViewModel
                {
                    Action = "Create",
                    DateTime = DateTime.Now,
                    ShipmentId = guid,
                    CarrierId = 10
                };
                service.Create(activityLog);

                var subject = service.GetAll(guid);
                var result = subject.FirstOrDefault(x => x.CarrierId == activityLog.CarrierId);

                Assert.IsNotNull(result);
                Assert.AreEqual(activityLog.Action, result.Action);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(StatusCodeException))]
        public void Create_ActivityLogDoesNotNull_ExpecterdException()
        {
            using (var context = InitializeContext("Create_ActivityLogDoesNotNull_ExpecterdException"))
            {
                var service = new ActivityLogService(context);
                service.Create(null);
            }
        }

        [TestMethod]
        public void GetAll_CallGetAll_ShouldReturnList()
        {
            using (var context = InitializeContext("GetAll_CallGetAll_ShouldReturnList"))
            {
                var service = new ActivityLogService(context);

                service.Create(new ActivityLogViewModel { CarrierName = "Name1", ShipmentId = guid });
                service.Create(new ActivityLogViewModel { CarrierName = "Name2", ShipmentId = guid });
                service.Create(new ActivityLogViewModel { CarrierName = "Name3", ShipmentId = guid });
                service.Create(new ActivityLogViewModel { CarrierName = "Name4", ShipmentId = guid });

                var result = service.GetAll(guid).ToList();

                Assert.AreEqual(4, result.Count);
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
