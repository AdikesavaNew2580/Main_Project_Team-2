
using NUnit.Framework;
using Moq;
using System.Data.Entity;
using System.Web.Mvc;
using System.Linq;
using Employee_Travel_Booking_System_WebApplication.Controllers.AdminMenu;
using Employee_Travel_Booking_System_WebApplication.Models;
using System.Collections.Generic;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        private AdminLoginController _controller;
        private Mock<Employee_Travel_Booking_SystemDB1Entities> _mockContext;
        private Mock<DbSet<admin>> _mockAdmins;

        [SetUp]
        public void Setup()
        {
            // Initialize mocks
            _mockContext = new Mock<Employee_Travel_Booking_SystemDB1Entities>();
            _mockAdmins = new Mock<DbSet<admin>>();

            
            
            _controller = new AdminLoginController();
        }

        // Login Redirecting to its View
        [Test]
        public void Login_Get_ReturnsView()
        {
            var result = _controller.Login() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }

        
    }
}