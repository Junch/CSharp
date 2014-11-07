using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Dinner.Models;
using Dinner.Controllers;

namespace Dinner.Tests {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void Can_Edit_User() {
            // Arrange
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            mock.Setup(m => m.Users).Returns(new User[] {
                new User {UserId = 1, Name="N1"},
                new User {UserId = 2, Name="N2"},
                new User {UserId = 3, Name="N3"},
            });
            UserController target = new UserController(mock.Object);

            // Action
            User u1 = target.Edit(1).ViewData.Model as User;
            User u2 = target.Edit(2).ViewData.Model as User;
            User u3 = target.Edit(3).ViewData.Model as User;

            // Assert
            Assert.AreEqual(1, u1.UserId);
            Assert.AreEqual(2, u2.UserId);
            Assert.AreEqual(3, u3.UserId);
        }
    }
}
