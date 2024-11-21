namespace BankAccount.Core.Models
{
    public class Account
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string OwnerName { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
