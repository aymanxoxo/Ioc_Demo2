using DataEntities;
using DataEntities.Exceptions;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        private IAuthenticationService authService;
        [TestInitialize]
        public void InitializeTest()
        {
            var _mock = new Mock<IRepository<User>>();
            _mock.Setup(e => e.Get(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>())).Returns(new List<User> { new User { Email = "test@service.com"} }.AsQueryable());

            Expression<Func<Expression<Func<User, bool>>, bool>> x = (User u) => u.Email == "";
            var n = "";

            _mock.Setup(e => e.Get(It.Is<Expression<Func<User, bool>>>(x), It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>())).Returns(new List<User> { new User { Email = "test@service.com" } }.AsQueryable());
            authService = new AuthenticationService(_mock.Object);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterwithNullUser_ShouldThroughArgumentNullException()
        {
            authService.Register(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void RegisterWithNullEmail_ShouldThroughValidationException()
        {
            authService.Register(new User { Password = "123" });
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void RegisterWithNullPassword_ShouldThroughValidationException()
        {
            try
            {
                authService.Register(new User { Email = "test2@service.com", Password = "123" });
            }
            catch(ValidationException ex)
            {
                Assert.AreEqual("Password", ex.Element);
                Assert.AreEqual(DataEntities.Resources.ServerLabel.PasswordRequired, ex.ValidationMessage);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void RegisterAlreadyExistsEmail_ShouldThroughValidationException()
        {
            try
            {
                authService.Register(new User { Email = "test2@service.com", Password = "123" });
            }
            catch(ValidationException ex)
            {
                Assert.AreEqual("Email", ex.Element);
                Assert.AreEqual(DataEntities.Resources.ServerLabel.AlreadyRegisteredEmail, ex.ValidationMessage);
            }
        }
    }
}
