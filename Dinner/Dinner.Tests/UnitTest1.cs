using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Dinner.Models;
using Dinner.Controllers;
using System.Web.Mvc;

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

        [TestMethod]
        public void Can_Save_valid_User() {
            // Arrange
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            UserController target = new UserController(mock.Object);
            User usr = new User { Name = "Test" };

            // Act
            ActionResult result = target.Edit(usr);

            // Assert - check that the repository was called
            mock.Verify(m => m.SaveUser(usr));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes() {
            // Arrange - create mock repository
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            // Arrange - create the controller
            UserController target = new UserController(mock.Object);
            // Arrange - create a product
            User usr = new User { Name = "Test" };
            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");

            // Act - try to save the product
            ActionResult result = target.Edit(usr);

            // Assert - check that the repository was not called
            mock.Verify(m => m.SaveUser(It.IsAny<User>()), Times.Never());
            // Assert - check the method result type
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
