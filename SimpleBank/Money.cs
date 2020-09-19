using System.Diagnostics;

namespace SimpleBank
{
    public interface IMoney
    {
        decimal Value { get; }
    }


    [DebuggerDisplay("Money: Value={this.Value}")]
    public class Money : IMoney
    {
        public Money(decimal amount)
        {
            this.Value = amount;
        }

        public decimal Value { get; }
    }
}