using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SimpleBank
{
    [DebuggerDisplay("Bank: Name={this.Name}")]
    public class Bank
    {
        public Bank(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        // all Acounts
        private Dictionary<Guid, Dictionary<string, IAccount>> accountsByCustomer = new Dictionary<Guid, Dictionary<string, IAccount>>();
        private Dictionary<string, IAccount> accountsById = new Dictionary<string, IAccount>();

        /// <summary>
        ///     Creates a new bank account for the given <see cref="customer" />
        ///     and deposits
        /// </summary>
        public IAccount CreateAccount(IPerson customer, IMoney initialDeposit)
        {
            // Check if negative amount
            if (initialDeposit.Value < 0)
                throw new Exception("Negative amount.");

            // Check if Person has that much cash
            if (initialDeposit.Value > customer.Cash.Value)
                throw new Exception("Insufficient cash.");

            // create new account
            IAccount account = new Account(initialDeposit, customer);

            // if this customer has no accounts yet - create accounts dict
            if (!accountsByCustomer.ContainsKey(customer.Id))
                accountsByCustomer.Add(customer.Id, new Dictionary<string, IAccount>());

            // add this account
            accountsByCustomer[customer.Id].Add(account.Id, account);
            accountsById.Add(account.Id, account);

            return account;

        }

        /*
        /// <summary>
        ///     Returns the account for given <see cref="accountId" />
        ///     <exception cref="Exception">If account cannot be found</exception>
        /// </summary>
        private Account GetAccountById(string accountId)
        {
            return accountsById[accountId];
        }
        */

        /// <summary>
        ///     Returns all accounts for a given <see cref="customer" />.
        /// </summary>
        public IEnumerable<IAccount> GetAccounts(Person customer)
        {
            foreach (IAccount account in this.accountsByCustomer[customer.Id].Values)
                yield return account;

        }

        /// <summary>
        ///     Add the <see cref="amount" /> to given account with <see cref="targetAccountId" />.
        /// </summary>
        /// <param name="targetAccountId"></param>
        /// <param name="amount"></param>
        public void Deposit(string targetAccountId, Money amount)
        {
            IAccount account = accountsById[targetAccountId];
            account.Deposit(amount);
        }

        /// <summary>
        ///     Extracts the <see cref="amount" /> from account with <see cref="sourceAccountId" />.
        /// </summary>
        /// <param name="sourceAccountId"></param>
        /// <param name="amount"></param>
        public void Withdraw(string sourceAccountId, Money amount)
        {
            IAccount account = accountsById[sourceAccountId];
            account.Withdraw(amount);

        }

        /// <summary>
        ///     Transfers the given <see cref="amount" /> from <see cref="sourceAccountId" /> to <see cref="targetAccountId" /> if
        ///     all validations succeed.
        /// </summary>
        public void Transfer(string sourceAccountId, string targetAccountId, Money amount)
        {
            IAccount sourceAcc = accountsById[sourceAccountId];
            IAccount targetAcc = accountsById[targetAccountId];

            if (sourceAcc == null || targetAcc == null)
                throw new Exception("Unkown account");

            sourceAcc.Withdraw(amount);
            targetAcc.Deposit(amount);
        }
    }
}