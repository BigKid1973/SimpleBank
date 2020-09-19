using System;
using Xunit;

namespace SimpleBank.Tests
{
    public class AccountTests
    {
        [Fact]
        public void ShouldCreateAccount()
        {
            // Arrange
            Money money = new Money(1000);
            Person owner = new Person("Jane Doe");

            // Act
            Account account = new Account(money, owner);

            // Assert
            Assert.NotNull(account);
            Assert.NotNull(account.Amount);
            Assert.Equal(money.Value, account.Amount.Value);
        }

        [Fact]
        public void ShouldWithdraw()
        {
            // Arrange
            Money money = new Money(1000);
            Person owner = new Person("Jane Doe");
            Account account = new Account(money, owner);

            // Act
            account.Withdraw(money);

            // Assert
            Assert.NotNull(account);
            Assert.NotNull(account.Amount);
            Assert.Equal(0, account.Amount.Value);

        }

        [Fact]
        public void ShouldWithdraw_ThrowsExceptionIfNegativeAmount()
        {
            // Arrange
            Money money = new Money(1000);
            Person owner = new Person("Jane Doe");
            Money negAmount = new Money(-1000);
            Account account = new Account(money, owner);

            // Act & Assert
            Assert.Throws<System.Exception>(() => account.Withdraw(negAmount));
        }

        [Fact]
        public void ShouldWithdraw_ThrowsExceptionIfInsufficientFunds()
        {
            // Arrange
            Money money = new Money(1000);
            Person owner = new Person("Jane Doe");
            Money moreMoney = new Money(2000);
            Account account = new Account(money, owner);

            // Act & Assert
            Assert.Throws<System.Exception>(() => account.Withdraw(moreMoney));
        }


        [Fact]
        public void ShouldDeposit()
        {
            // Arrange
            Money money = new Money(1000);
            Person owner = new Person("Jane Doe");

            // Act
            Account account = new Account(money, owner);
            account.Deposit(money);

            // Assert
            Assert.NotNull(account);
            Assert.NotNull(account.Amount);
            Assert.Equal(2 * money.Value, account.Amount.Value);

        }

        [Fact]
        public void ShouldDeposit_ThrowsExceptionIfNegativeAmount()
        {
            // Arrange
            Money money = new Money(1000);
            Person owner = new Person("Jane Doe");
            Money negAmount = new Money(-1000);
            Account account = new Account(money, owner);

            // Act & Assert
            Assert.Throws<System.Exception>(() => account.Deposit(negAmount));
        }

    }
}