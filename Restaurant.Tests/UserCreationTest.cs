using Restaurant.Model;
using Restaurant.Pages.Account;

namespace Restaurant.Tests
{
    [TestClass]
    public sealed class UserCreationTest
    {
        private User testUser;
        private User actualUser;

        [TestInitialize]
        public void Setup()
        {
            testUser = RegisterModel.CreateNewUser("TestName", "TestAddress", "TestCity", "test@gmail.com", "TestPassword");
            actualUser = new User
            {
                Name = "TestName",
                Address = "TestAddress",
                City = "TestCity",
                Email = "test@gmail.com",
                Password = "TestPassword",
                RegistrationDate = DateTime.Now,
                Role = 0
            };
        }

        [TestMethod]
        public void TestUserName()
        {
            Assert.AreEqual(actualUser.Name, testUser.Name);
        }

        [TestMethod]
        public void TestUserAddress()
        {
            Assert.AreEqual(actualUser.Address, testUser.Address);
        }

        [TestMethod]
        public void TestUserCity()
        {
            Assert.AreEqual(actualUser.City, testUser.City);
        }

        [TestMethod]
        public void TestUserEmail()
        {
            Assert.AreEqual(actualUser.Email, testUser.Email);
        }

        [TestMethod]
        public void TestUserPassword()
        {
            Assert.AreEqual(actualUser.Password, testUser.Password);
        }

        [TestMethod]
        public void TestUserRegistrationDate()
        {
            Assert.AreEqual(actualUser.RegistrationDate, testUser.RegistrationDate);
        }

        [TestMethod]
        public void TestUserRole()
        {
            Assert.AreEqual(actualUser.Role, testUser.Role);
        }
    }
}

