using System;
using System.Threading.Tasks;
using AutoFixture;
using MiniBank.Core.Domains.BankAccounts;
using MiniBank.Core.Domains.BankAccounts.Repositories;
using MiniBank.Core.Domains.BankAccounts.Services;
using MiniBank.Core.Domains.CurrencyConverters.Services;
using MiniBank.Core.Domains.RemittanceHistories.Services;
using MiniBank.Core.Domains.Users.Repositories;
using MiniBank.Core.Exception;
using Moq;
using Xunit;

namespace MiniBank.Core.Tests.Tests
{
    public class BankAccountServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IBankAccountRepository> _bankAccountRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly BankAccountService _service;

        public BankAccountServiceTests()
        {
            _fixture = new Fixture();
            _bankAccountRepositoryMock = new Mock<IBankAccountRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userRepositoryMock = new Mock<IUserRepository>();
            Mock<ICurrencyConverter> currencyConverterMock = new();
            Mock<IRemittanceHistoryService> remmitanceHistoryMock = new();

            _service = new BankAccountService(_bankAccountRepositoryMock.Object,
                _userRepositoryMock.Object,currencyConverterMock.Object,
                remmitanceHistoryMock.Object,_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Create_AccountIsNull_ShouldThrowArgumentNullException()
        {
            //ARRANGE
            //ACT
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));
            //ASSERT
            _bankAccountRepositoryMock.Verify(it=>it.Create(It.IsAny<Account>()),Times.Never);
            _unitOfWorkMock.Verify(it=>it.SaveChanges(),Times.Never);
        }
        [Fact]
        public async Task Create_AccountWithOtherCurrency_ShouldThrowValidationException()
        {
            //ARRANGE
            var account = _fixture.Build<Account>()
                .With(it=>it.Currency,"otherCurrency")
                .Create();
            //ACT
            await Assert.ThrowsAsync<ValidationException>(() => _service.Create(account));
            //ASSERT
            _bankAccountRepositoryMock.Verify(it=>it.Create(It.IsAny<Account>()),Times.Never);
            _unitOfWorkMock.Verify(it=>it.SaveChanges(),Times.Never);
        }
        [Theory]
        [InlineData("RUB")]
        [InlineData("USD")]
        [InlineData("EUR")]
        public async Task Create_AccountWithoutUser_ShouldThrowValidationException(string currency)
        {
            //ARRANGE
            var account = _fixture.Build<Account>()
                .With(it=>it.Currency,currency)
                .Create();
            _userRepositoryMock.Setup(it => it.UserExists(account.UserId)).ReturnsAsync(false);
            //ACT
            await Assert.ThrowsAsync<ValidationException>(() => _service.Create(account));
            //ASSERT
            _bankAccountRepositoryMock.Verify(it=>it.Create(It.IsAny<Account>()),Times.Never);
            _unitOfWorkMock.Verify(it=>it.SaveChanges(),Times.Never);
        }
        [Theory]
        [InlineData("RUB")]
        [InlineData("USD")]
        [InlineData("EUR")]
        public async Task Create_AccountIsValid_ShouldReturnNewAccount(string currency)
        {
            //ARRANGE
            var account = _fixture.Build<Account>()
                .With(it=>it.Currency,currency)
                .Create();
            _userRepositoryMock.Setup(it => it.UserExists(account.UserId)).ReturnsAsync(true);
            //ACT
            await _service.Create(account);
            //ASSERT
            _bankAccountRepositoryMock.Verify(it=>it.Create(It.IsAny<Account>()),Times.Once);
            _unitOfWorkMock.Verify(it=>it.SaveChanges(),Times.Once);
        }
        [Fact]
        public void CalculateCommission_WithNegativeSum_ShouldThrowValidationException()
        {
            //ARRANGE
            var sum = -1000;
            var fromAccountId = Guid.NewGuid();
            var toAccountId = Guid.NewGuid();
            //ACT
            Assert.Throws<ValidationException>(() => _service.CalculateComission(sum,fromAccountId,toAccountId));
            //ASSERT
        }
        [Fact]
        public void CalculateCommission_IsValidWithEqualUserId_ShouldThrowValidationException()
        {
            //ARRANGE
            var sum = 1000;
            var fromUserId = Guid.NewGuid();
            var toUserId = fromUserId;
            var actual = 1000;
            //ACT
            var expected=_service.CalculateComission(sum,fromUserId,toUserId);
            //ASSERT
            Assert.Equal(expected,actual);
        }
        [Fact]
        public void CalculateCommission_IsValidWithDifferentUserId_ShouldThrowValidationException()
        {
            //ARRANGE
            var sum = 1000;
            var fromUserId = Guid.NewGuid();
            var toUserId = Guid.NewGuid();;
            var actual = 980;
            //ACT
            var expected=_service.CalculateComission(sum,fromUserId,toUserId);
            //ASSERT
            Assert.Equal(expected,actual);
        }
        [Fact]
        public async Task CloseAccount_IsValid_MustRunOnce()
        {
            //ARRANGE
            var id = Guid.NewGuid();
            //ACT
            await _service.CloseAccount(id);
            //ASSERT
            _bankAccountRepositoryMock.Verify(it=>it.CloseAccount(It.IsAny<Guid>()),Times.Once);
            _unitOfWorkMock.Verify(it=>it.SaveChanges(),Times.Once);
        }
        [Fact]
        public async Task Remittance_WithNegativeSum_ShouldThrowValidationException()
        {
            //ARRANGE
            var sum = -1000;
            var fromAccount = _fixture.Build<Account>()
                .Create();
            var toAccount = _fixture.Build<Account>()
                .Create();
            //ACT
            await Assert.ThrowsAsync<ValidationException>(() => _service.Remittance(sum,fromAccount.Id,toAccount.Id));
            //ASSERT
            _bankAccountRepositoryMock.Verify(it=>it.ChangeAmounts(fromAccount,toAccount),Times.Never);
            _unitOfWorkMock.Verify(it=>it.SaveChanges(),Times.Never);
        }
        [Theory]
        [InlineData(1000,100)]
        public async Task Remittance_WithCloseAccount_ShouldThrowValidationException(decimal accountSum,decimal sum)
        {
            //ARRANGE
            var fromAccount = _fixture.Build<Account>()
                .With(it=>it.IsActive,false)
                .With(it=>it.Sum,accountSum)
                .Create();
            var toAccount = _fixture.Build<Account>()
                .Create();
            _bankAccountRepositoryMock.Setup(it => it.GetAccount(fromAccount.Id)).ReturnsAsync(fromAccount);
            _bankAccountRepositoryMock.Setup(it => it.GetAccount(toAccount.Id)).ReturnsAsync(toAccount);
            //ACT
            await Assert.ThrowsAsync<ValidationException>(() => _service.Remittance(sum,fromAccount.Id,toAccount.Id));
            //ASSERT
            _bankAccountRepositoryMock.Verify(it=>it.ChangeAmounts(fromAccount,toAccount),Times.Never);
            _unitOfWorkMock.Verify(it=>it.SaveChanges(),Times.Never);
        }
        [Theory]
        [InlineData(100,1000)]
        public async Task Remittance_WithSumLessThanAccountSum_ShouldThrowValidationException(decimal accountSum,decimal sum)
        {
            //ARRANGE
            var fromAccount = _fixture.Build<Account>()
                .With(it=>it.Sum,accountSum)
                .Create();
            var toAccount = _fixture.Build<Account>()
                .Create();
            _bankAccountRepositoryMock.Setup(it => it.GetAccount(fromAccount.Id)).ReturnsAsync(fromAccount);
            _bankAccountRepositoryMock.Setup(it => it.GetAccount(toAccount.Id)).ReturnsAsync(toAccount);
            //ACT
            await Assert.ThrowsAsync<ValidationException>(() => _service.Remittance(sum,fromAccount.Id,toAccount.Id));
            //ASSERT
            _bankAccountRepositoryMock.Verify(it=>it.ChangeAmounts(fromAccount,toAccount),Times.Never);
            _unitOfWorkMock.Verify(it=>it.SaveChanges(),Times.Never);
        }
        [Theory]
        [InlineData(1000,100)]
        public async Task Remittance_IsValid(decimal accountSum,decimal sum)
        {
            //ARRANGE
            var fromAccount = _fixture.Build<Account>()
                .With(it=>it.Sum,accountSum)
                .With(it=>it.IsActive,true)
                .With(it=>it.Currency,"RUB")
                .Create();
            var toAccount = _fixture.Build<Account>()
                .With(it=>it.IsActive,true)
                .With(it=>it.Currency,"RUB")
                .Create();
            _bankAccountRepositoryMock.Setup(it => it.GetAccount(fromAccount.Id)).ReturnsAsync(fromAccount);
            _bankAccountRepositoryMock.Setup(it => it.GetAccount(toAccount.Id)).ReturnsAsync(toAccount);
            //ACT
             await _service.Remittance(sum,fromAccount.Id,toAccount.Id);
            //ASSERT
            _bankAccountRepositoryMock.Verify(it=>it.ChangeAmounts(fromAccount,toAccount),Times.Once);
            _unitOfWorkMock.Verify(it=>it.SaveChanges(),Times.Once);
        }
        [Fact]
        public async Task GetAllAccounts_MustRunOnce()
        {
            //ARRANGE
            //ACT
            await _service.GetAllAccounts();
            //ASSERT
            _bankAccountRepositoryMock.Verify(it=>it.GetAllAccounts(),Times.Once);
        }
    }
}