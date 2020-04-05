using System;
using System.Collections.Generic;

namespace classes
{
	public class BankAccount
	{
		private static int accountNumberSeed = 1234567890;
		private List<Transaction> allTransations = new List<Transaction>();
		public string Number { get; }
		public string Owner { get; set; }
		public decimal Balance { 
			get
			{
				decimal balance = 0;
				foreach (var item in allTransations)
				{
					balance += item.Amount;
				}
				return balance;
			}
				
		}
		public BankAccount(string name, decimal initBalance)
		{
			this.Owner = name;
			this.Number = accountNumberSeed.ToString();
			accountNumberSeed++;
			MakeDeposit(initBalance, DateTime.Now, "Initial balance");
		}

		public void MakeDeposit(decimal amount, DateTime date, string note) 
		{
			if (amount <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
			}
			var deposit = new Transaction(amount, date, note);
			allTransations.Add(deposit);
		}

		public void MakeWithDrawal(decimal amount, DateTime date, string note)
		{
			if (amount <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal  must be positive");
			}

			if (Balance - amount < 0)
			{
				throw new InvalidOperationException("Not sufficient funds for this withdrawal");
			}
			var withdrawal = new Transaction(-amount, date, note);
			allTransations.Add(withdrawal);
		}

		public string GetAccountHistory()
		{
			var report = new System.Text.StringBuilder();
			decimal balance = 0;
			report.AppendLine("Date\t\tAmount\tBalance\tNote");
			foreach(var item in allTransations)
			{
				balance += item.Amount;
				report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
			}
			return report.ToString();
		}

	}


}
