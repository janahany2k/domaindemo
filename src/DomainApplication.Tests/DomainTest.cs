using System;
using System.Collections.Generic;
using System.Linq;
using DomainApplication.Controllers;
using DomainApplication.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainApplication.Tests
{
    [TestClass]
    public class DomainTest
    {
        [TestMethod]
        public void AccessTest()
        {
            var controller = new DomainController();
            Assert.IsInstanceOfType(controller, typeof(DomainController));
        }

        [TestMethod]
        public void GenerateSubDomainMethodTest()
        {
            var controller = new DomainController();
            var subdomains = controller.Get(10, "yahoo.com");

            Assert.IsNotNull(subdomains);

            Assert.AreEqual(subdomains.Count(), 10);
        }

        [TestMethod]
        public void CheckIpAddressMethodTest()
        {
            var controller = new DomainController();
            var subdomains = controller.Get(10, "yahoo.com");

            var ipAddresses = controller.FindIpAddresses(subdomains).Result;

            Assert.IsNotNull(ipAddresses);

            Assert.AreEqual(ipAddresses.Count, 10);
        }

        [TestMethod]
        public void TestValidDomainIpAddressTest()
        {
            var controller = new DomainController();
            var subdomains = new List<SubdomainModel>
            {
                new SubdomainModel {Url = "au.yahoo.com", IpAddress = string.Empty}
            };

            subdomains = controller.FindIpAddresses(subdomains).Result;

            Assert.IsNotNull(subdomains);

            Assert.AreNotEqual(subdomains.First().IpAddress, "-");
        }
    }
}
