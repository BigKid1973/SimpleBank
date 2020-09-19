using Xunit;
using System.Linq.Expressions;
using System.Linq;
using Moq;
using System;

namespace SimpleBank.Tests
{
    public class BankTests
    {
        [Fact]
        public void ShouldCreateBank()
        {
            // Arrange
            string bankName = "Gold";
            // Act
            Bank bank = new Bank(bankName);

            // Assert
            Assert.NotNull(bank);
            Assert.Equal(bankName, bank.Name);
        }

        [Fact]
        public void ShouldCreateAccount()
        {
            // Arrange
            
            // IMoney money = new Money(1000);
            var money = new Mock<IMoney>();
            money.SetupGet(m => m.Value).Returns(1000);

            // IPerson person = new Person( "Janet Jackson", money.Object);
            var person = new Mock<IPerson>();
            Guid guid = Guid.NewGuid();
            person.SetupGet(p => p.Cash).Returns(money.Object);
            person.SetupGet(p => p.Name).Returns("Janet Jackson");
            person.SetupGet(p => p.Id).Returns(guid);

            Bank bank = new Bank("JustABank");

            // Act
            IAccount account = bank.CreateAccount(person.Object, money.Object);

            // Assert
            Assert.NotNull(account);
        }

        [Fact]
        public void ShouldCreateAccount_ThrowsExceptionIfNegativeValue()
        {
            // Arrange
            Money money = new Money(1000);
            Person person = new Person("Janet Jackson", money);
            Money lessthannomoney = new Money(-1000);
            Bank bank = new Bank("JustABank");

            // Act & Assert
            Assert.Throws<System.Exception>(() => bank.CreateAccount(person, lessthannomoney)); 

        }

        [Fact]
        public void ShouldCreateAccount_ThrowsExceptionIfInsufficientFunds()
        {
            // Arrange
            Money money = new Money(10000);
            Money noMoney = new Money(0);
            Person person = new Person("Janet Jackson", noMoney);
            Bank bank = new Bank("JustABank");

            // Act & Assert
            Assert.Throws<System.Exception>(() => bank.CreateAccount(person, money));
        }

        [Fact]
        public void ShouldAccountWithdraw()
        {
            // Arrange
            Money money = new Money(10000);
            Person person = new Person("Janet Jackson", money);
            Bank bank = new Bank("JustABank");
            IAccount account = bank.CreateAccount(person, money);

            // Act
            bank.Withdraw(account.Id, money);

            // Assert
            Assert.Equal(0, account.Amount.Value);

        }

        [Fact]
        public void ShouldAccountDeposit()
        {
            // Arrange
            Money money = new Money(10000);
            Person person = new Person("Janet Jackson", money);
            Bank bank = new Bank("JustABank");
            IAccount account = bank.CreateAccount(person, money);

            // Act
            bank.Deposit(account.Id, money);

            // Assert
        }

        [Fact]
        public void ShouldAccountTransfer()
        {
            // Arrange
            Money money = new Money(10000);
            Person person = new Person("Janet Jackson", money);
            Bank bank = new Bank("JustABank");
            IAccount account = bank.CreateAccount(person, money);
            IAccount account2 = bank.CreateAccount(person, money);

            // Act
            bank.Transfer(account.Id, account2.Id, money);

            Assert.Equal(0 , account.Amount.Value); // withdrawal
            Assert.Equal(money.Value * 2, account2.Amount.Value); // deposit

        }

        [Fact]
        public void ShouldGetAccounts()
        {
            // Arrange
            Money money = new Money(10000);
            Person person = new Person("Janet Jackson", money);
            Bank bank = new Bank("JustABank");
            bank.CreateAccount(person, money);

            // Act
            int count = bank.GetAccounts(person).Count();

            // Assert
            Assert.Equal(1, count);
        }

        [Fact]
        public void GetAccountById()
        {
            // Arrange
            Money money = new Money(10000);
            Person person = new Person("Janet Jackson", money);
            Bank bank = new Bank("JustABank");
            bank.CreateAccount(person, money);

            // Act
            int count = bank.GetAccounts(person).Count();

            // Assert
            Assert.Equal(1, count);
        }


        [Fact]
        public void ShouldAccountTransfer_ThrowsExceptionIfInsufficientFunds()
        {
            // Arrange
            Money money = new Money(10000);
            Person person = new Person("Janet Jackson", money);
            Bank bank = new Bank("JustABank");
            IAccount account = bank.CreateAccount(person, money);
            IAccount account2 = bank.CreateAccount(person, money);
            Money moaMoney = new Money(200000);

            // Act & Assert
            Assert.Throws<System.Exception>( () => bank.Transfer(account.Id, account2.Id, moaMoney));

            // Assert
            Assert.Equal(money.Value, account.Amount.Value); // no withdrawal
            Assert.Equal(money.Value, account2.Amount.Value); // no deposit
        }
    }
}