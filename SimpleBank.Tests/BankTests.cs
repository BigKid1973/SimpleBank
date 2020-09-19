using Xunit;
using System.Linq.Expressions;
using System.Linq;

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
            Person person = new Person( "Janet Jackson" );
            Money money = new Money(1000);
            person.Cash = money;
            Bank bank = new Bank("JustABank");

            // Act
            Account account = bank.CreateAccount(person, money);

            // Assert
            Assert.NotNull(account);
        }

        [Fact]
        public void ShouldCreateAccount_ThrowsExceptionIfNegativeValue()
        {
            // Arrange
            Person person = new Person("Janet Jackson");
            Money money = new Money(-1000);
            Bank bank = new Bank("JustABank");

            // Act & Assert
            Assert.Throws<System.Exception>(() => bank.CreateAccount(person, money)); 

        }

        [Fact]
        public void ShouldCreateAccount_ThrowsExceptionIfInsufficientFunds()
        {
            // Arrange
            Person person = new Person("Janet Jackson");
            Money money = new Money(10000);
            Bank bank = new Bank("JustABank");

            // Act & Assert
            Assert.Throws<System.Exception>(() => bank.CreateAccount(person, money));
        }

        [Fact]
        public void ShouldAccountWithdraw()
        {
            // Arrange
            Person person = new Person("Janet Jackson");
            Money money = new Money(10000);
            person.Cash = money;
            Bank bank = new Bank("JustABank");
            Account account = bank.CreateAccount(person, money);

            // Act
            bank.Withdraw(account.Id, money);

            // Assert
            Assert.Equal(0, account.Amount.Value);

        }

        [Fact]
        public void ShouldAccountDeposit()
        {
            // Arrange
            Person person = new Person("Janet Jackson");
            Money money = new Money(10000);
            person.Cash = money;
            Bank bank = new Bank("JustABank");
            Account account = bank.CreateAccount(person, money);

            // Act
            bank.Deposit(account.Id, money);

            // Assert
        }

        [Fact]
        public void ShouldAccountTransfer()
        {
            // Arrange
            Person person = new Person("Janet Jackson");
            Money money = new Money(10000);
            person.Cash = money;
            Bank bank = new Bank("JustABank");
            Account account = bank.CreateAccount(person, money);
            Account account2 = bank.CreateAccount(person, money);

            // Act
            bank.Transfer(account.Id, account2.Id, money);

            Assert.Equal(0 , account.Amount.Value); // withdrawal
            Assert.Equal(money.Value * 2, account2.Amount.Value); // deposit

        }

        [Fact]
        public void ShouldGetAccounts()
        {
            // Arrange
            Person person = new Person("Janet Jackson");
            Money money = new Money(10000);
            person.Cash = money;
            Bank bank = new Bank("JustABank");
            bank.CreateAccount(person, money);

            // Act
            int count = bank.GetAccounts(person).Count();

            // Assert
            Assert.Equal(1, count);
        }

        public void GetAccountById()
        {
            // Arrange
            Person person = new Person("Janet Jackson");
            Money money = new Money(10000);
            person.Cash = money;
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
            Person person = new Person("Janet Jackson");
            Money money = new Money(10000);
            person.Cash = money;
            Bank bank = new Bank("JustABank");
            Account account = bank.CreateAccount(person, money);
            Account account2 = bank.CreateAccount(person, money);
            Money moaMoney = new Money(200000);

            // Act & Assert
            Assert.Throws<System.Exception>( () => bank.Transfer(account.Id, account2.Id, moaMoney));

            // Assert
            Assert.Equal(money.Value, account.Amount.Value); // no withdrawal
            Assert.Equal(money.Value, account2.Amount.Value); // no deposit
        }
    }
}