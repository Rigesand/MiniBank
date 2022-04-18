using System;
using System.Threading.Tasks;
using AutoFixture;
using MiniBank.Core.Domains.Users;
using MiniBank.Core.Domains.Users.Repositories;
using MiniBank.Core.Domains.Users.Services;
using Moq;
using Xunit;

namespace MiniBank.Core.Tests.Tests
{
    public class UserServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly UserService _service;

        public UserServiceTests()
        {
            _fixture = new Fixture();
            _userRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _service = new UserService(_userRepositoryMock.Object,_unitOfWorkMock.Object);
        }
        [Fact]
        public async Task Create_UserIsNull_ShouldThrowArgumentNullException()
        {
            //ARRANGE
            //ACT
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));
            //ASSERT
            _userRepositoryMock.Verify(it=>it.Create(It.IsAny<User>()),Times.Never);
            _unitOfWorkMock.Verify(it=>it.SaveChanges(),Times.Never);
        }
        [Fact]
        public async Task Create_LoginAndEmailIsNull_ShouldThrowArgumentNullException()
        {
            //ARRANGE
            var user = _fixture.Build<User>()
                .Without(it => it.Login)
                .Without(it=>it.Email)
                .Create();
            //ACT
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(user));
            //ASSERT
            _userRepositoryMock.Verify(it=>it.Create(It.IsAny<User>()),Times.Never);
            _unitOfWorkMock.Verify(it=>it.SaveChanges(),Times.Never);
        }
        [Fact]
        public async Task Create_ValidUser_MustRunOnce()
        {
            //ARRANGE
            var user = _fixture.Build<User>()
                .Create();
            _userRepositoryMock.Setup(it => it.Create(user));
            _unitOfWorkMock.Setup(it => it.SaveChanges());
            //ACT
            await _service.Create(user);
            //ASSERT
            _userRepositoryMock.Verify(it=>it.Create(It.IsAny<User>()),Times.Once);
            _unitOfWorkMock.Verify(it=>it.SaveChanges(),Times.Once);
        }
        [Fact]
        public async Task Update_UserIsNull_ShouldThrowArgumentNullException()
        {
            //ARRANGE
            //ACT
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Update(null));
            //ASSERT
            _userRepositoryMock.Verify(it=>it.Update(It.IsAny<User>()),Times.Never);
            _unitOfWorkMock.Verify(it=>it.SaveChanges(),Times.Never);
        }
        [Fact]
        public async Task Update_LoginAndEmailIsNull_ShouldThrowArgumentNullException()
        {
            //ARRANGE
            var user = _fixture.Build<User>()
                .Without(it => it.Id)
                .Without(it=>it.Login)
                .Without(it=>it.Email)
                .Create();
            //ACT
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Update(user));
            //ASSERT
            _userRepositoryMock.Verify(it=>it.Update(It.IsAny<User>()),Times.Never);
            _unitOfWorkMock.Verify(it=>it.SaveChanges(),Times.Never);
        }
        [Fact]
        public async Task Update_ValidUser_MustRunOnce()
        {
            //ARRANGE
            var user = _fixture.Build<User>()
                .Create();
            _userRepositoryMock.Setup(it => it.Update(user));
            _unitOfWorkMock.Setup(it => it.SaveChanges());
            //ACT
            await _service.Update(user);
            //ASSERT
            _userRepositoryMock.Verify(it=>it.Update(It.IsAny<User>()),Times.Once);
            _unitOfWorkMock.Verify(it=>it.SaveChanges(),Times.Once);
        }
        [Fact]
        public async Task GetAllUsers_MustRunOnce()
        {
            //ARRANGE
            _userRepositoryMock.Setup(it => it.GetAllUsers());
            //ACT
            await _service.GetAllUsers();
            //ASSERT
            _userRepositoryMock.Verify(it=>it.GetAllUsers(),Times.Once);
        }
        [Fact]
        public async Task IsExist_LoginIsNull_ShouldThrowArgumentNullException()
        {
            //ARRANGE
            //ACT
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.IsExist(null));
            //ASSERT
            _userRepositoryMock.Verify(it=>it.IsExist(It.IsAny<string>()),Times.Never);
        }
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task IsExist_LoginIsValid_ShouldReturnValueBasedOnRepositoryOutput(bool repositoryResult)
        {
            //ARRANGE
            var login = _fixture.Build<string>().Create();
            _userRepositoryMock.Setup(it => it.IsExist(login)).ReturnsAsync(repositoryResult);
            //ACT
            var isExist = await _service.IsExist(login);
            //ASSERT
            Assert.Equal(repositoryResult,isExist);
            _userRepositoryMock.Verify(it=>it.IsExist(It.IsAny<string>()),Times.Once);
        }
        [Fact]
        public async Task Delete_IsValid_MustRunOnce()
        {
            //ARRANGE
            var id = Guid.NewGuid();
            //ACT
            await _service.Delete(id);
            //ASSERT
            _userRepositoryMock.Verify(it=>it.Delete(It.IsAny<Guid>()),Times.Once);
            _unitOfWorkMock.Verify(it=>it.SaveChanges(),Times.Once);
        }
    }
}