using Xunit;

namespace SimpleBank.Tests
{
    public class MoneyTests
    {
        [Fact]
        public void ShouldCreateMoney()
        {
            // Arrange
            int amount = 1000;

            // Act
            Money money = new Money(amount);

            // Assert
            Assert.NotNull(money);
            Assert.Equal(1000, amount);
        }
    }
}