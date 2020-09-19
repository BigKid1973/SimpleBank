using Xunit;

namespace SimpleBank.Tests
{
    public class PersonTests
    {
        [Fact]
        public void ShouldCreatePerson()
        {
            // Arrange
            string Name = "Jane Doe";

            // Act
            Money money = new Money(0);
            Person person = new Person(Name, money);

            // Assert
            Assert.NotNull(person);
            Assert.Equal( Name, person.Name);
        }
    }
}