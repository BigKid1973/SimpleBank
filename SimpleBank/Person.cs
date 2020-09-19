using System;

namespace SimpleBank
{
    public interface IPerson
    {
        Guid Id { get; }

        string Name { get; }

        IMoney Cash { get; set; }
    }

    public class Person : IPerson
    {
        public Person(string testPerson1Name, IMoney cash)
        {
            this.Id = Guid.NewGuid();
            this.Name = testPerson1Name;
            this.Cash = cash;
        }

        public Guid Id { get; }

        public string Name { get; }

        public IMoney Cash { get; set; }

    }
}