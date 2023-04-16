using krediku_be.Models;

namespace krediku_be.Repositories
{
    public interface ITransactionRepo
    {
        Task<List<Transaction>> GetTransactions();
        Task<List<TransactionDetail>> GetTransactionWithDetail();
        Task<Transaction> AddTransaction(Transaction transaction);
        Task<Transaction> GetTransaction(string AgreementNo);
    }
}
