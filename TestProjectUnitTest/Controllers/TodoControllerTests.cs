using Domain;
using Microsoft.AspNetCore.Identity;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Areas.Identity.Data;
using TestProject.Controllers;
using Xunit;

namespace TestProjectUnitTest.Controllers
{
    public class TodoControllerTests
    {
        private MockRepository mockRepository;
        private Mock<DataAccess.Repository.IRepository<Fruit>> mockRepositoryService;
        private Mock<IFruitService> mockFruitService;
        //private Mock<UserManager<TestProjectUser>> mockUserManager;

        public TodoControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.mockRepositoryService = this.mockRepository.Create<DataAccess.Repository.IRepository<Fruit>>();
            this.mockFruitService = this.mockRepository.Create<IFruitService>();
            //this.mockUserManager = this.mockRepository.Create<UserManager<TestProjectUser>>(Mock.Of<IUserStore<TestProjectUser>>(), null, null, null, null, null, null, null, null);
        }

        //private TodoController CreateTodoController()
        //{
        //    return new TodoController(
        //        this.mockFruitService.Object,
        //        this.mockUserManager.Object);
        //}

        [Fact]
        public async Task GetFruits_StateUnderTest_ExpectedBehavior()
        {
            var store = new Mock<UserManager<IdentityUser>>();
            store.Setup(x => x.FindByIdAsync("123"))
                .ReturnsAsync(new IdentityUser()
                {
                    UserName = "test@email.com",
                    Id = "123"
                });

            //Mock<UserManager<TestProjectUser>> userManager = GetMockUserManager();
            //var mgr = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);
            //var controller = new TodoController(this.mockFruitService.Object,mgr);

            List<TestProjectUser> _users = new List<TestProjectUser>
              {
               new TestProjectUser() { Id = 1 }
              };


            var mgr1=Helper.Helper.MockUserManager(_users);
            mockFruitService.Setup(p => p.GetAll()).ReturnsAsync(new List<Fruit>
            {
                 new Fruit{Id=1,Color="red",CreatedDateTime=DateTime.Now,CreatorUserId=1,Name="test"},
                 new Fruit{Id=2,Color="red",CreatedDateTime=DateTime.Now,CreatorUserId=1,Name="test"}
            });

            var controller = new TodoController(this.mockFruitService.Object,mgr1.Object);
            // Arrange
            //var todoController = this.CreateTodoController(this.mockFruitService.Object,mgr1);
            string searchText = null;
            // Act
            var result = await controller.GetFruits(searchText);
            // Assert
            //Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetFruit_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var store = new Mock<UserManager<IdentityUser>>();
            store.Setup(x => x.FindByIdAsync("123"))
                .ReturnsAsync(new IdentityUser()
                {
                    UserName = "test@email.com",
                    Id = "123"
                });
            List<TestProjectUser> _users = new List<TestProjectUser>
              {
               new TestProjectUser() { Id = 1 }
              };
            var mgr1=Helper.Helper.MockUserManager(_users);
            int id = 1;
            mockFruitService.Setup(p => p.GetById(id)).ReturnsAsync(            
                 new Fruit{Id=1,Color="red",CreatedDateTime=DateTime.Now,CreatorUserId=1,Name="test"}                
            );
            var controller = new TodoController(this.mockFruitService.Object,mgr1.Object);
             
           
            var result = await controller.GetFruit(
            id);
           
            this.mockRepository.VerifyAll();
        }

        //[Fact]
        //public async Task PutFruit_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var todoController = this.CreateTodoController();
        //    int id = 0;
        //    Fruit fruit = null;

        //    // Act
        //    var result = await todoController.PutFruit(
        //    id,
        //    fruit);

        //    // Assert
        //    Assert.True(false);
        //    this.mockRepository.VerifyAll();
        //}

        [Fact]
        public async Task PostFruit_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var store = new Mock<UserManager<IdentityUser>>();
            store.Setup(x => x.FindByIdAsync("123"))
                .ReturnsAsync(new IdentityUser()
                {
                    UserName = "test@email.com",
                    Id = "123"
                });
            List<TestProjectUser> _users = new List<TestProjectUser>
              {
               new TestProjectUser() { Id = 1 }
              };
            var mgr1=Helper.Helper.MockUserManager(_users);
            //int id = 1;
            Fruit fruit = new Fruit
            {
                Id=1,
                Color="red",
                CreatedDateTime=DateTime.Now,
                CreatorUserId=1,
                Name="test"
            };
            mockFruitService.Setup(p => p.CreateReport(fruit)).ReturnsAsync("Success"); 
            var controller = new TodoController(this.mockFruitService.Object,mgr1.Object);            

            // Act
            var result = await controller.PostFruit(
            fruit);
            // Assert
            //Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        //[Fact]
        //public async Task DeleteFruit_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var todoController = this.CreateTodoController();
        //    int id = 0;

        //    // Act
        //    var result = await todoController.DeleteFruit(
        //    id);

        //    // Assert
        //    Assert.True(false);
        //    this.mockRepository.VerifyAll();
        //}
    }
}
