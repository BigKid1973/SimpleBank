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
            Person person = new Person(Name);

            // Assert
            Assert.NotNull(person);
            Assert.Equal( Name, person.Name);
        }
    }
}