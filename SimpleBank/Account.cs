using System;
using System.Diagnostics;

namespace SimpleBank
{
    [DebuggerDisplay("Account: Id={this.Id}, Person={this.Owner.Name}, Money={this.Amount.Value}")]
    public class Account
    {
        public Account(Money initialAmount, Person owner)
        {
            this.Id = $"CH00-{Guid.NewGuid().ToString("D").ToUpperInvariant()}";
            this.Amount = initialAmount;
            this.Owner = owner;
        }

        public void Withdraw(Money amount)
        {
            if (amount.Value < 0)
                throw new Exception("Negative withdrawal not allowed.");

            if (amount.Value > Amount.Value)
                throw new Exception("Insufficient funds.");

            Amount = new Money(Amount.Value - amount.Value);
        }

        public void Deposit(Money amount)
        {
            if (amount.Value < 0)
                throw new Exception("Negative deposit not allowed.");

            Amount = new Money(Amount.Value + amount.Value);
        }

        public string Id { get; }

        public Money Amount { get; internal set; }

        public Person Owner { get; }
    }
}