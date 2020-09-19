using System;
using System.Diagnostics;

namespace SimpleBank
{
    public interface IAccount
    {
        void Withdraw(Money amount);
        void Deposit(Money amount);
        void Init(IMoney initialAmount, IPerson owner);
        string Id { get; }

        IMoney Amount { get; }

        IPerson Owner { get; }
    }

    [DebuggerDisplay("Account: Id={this.Id}, Person={this.Owner.Name}, Money={this.Amount.Value}")]
    public class Account : IAccount
    {
        public void Init(IMoney initialAmount, IPerson owner)
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

        public string Id { get; internal set; }

        public IMoney Amount { get; internal set; }

        public IPerson Owner { get; internal set; }
    }
}