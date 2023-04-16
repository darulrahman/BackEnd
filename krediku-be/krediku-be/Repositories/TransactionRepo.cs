using krediku_be.Data;
using krediku_be.Models;
using Microsoft.EntityFrameworkCore;

namespace krediku_be.Repositories
{
    public class TransactionRepo : ITransactionRepo
    {
        private KredikuContext _context;
        public TransactionRepo(KredikuContext context)
        {
            _context = context;
        }
        public async Task<Transaction> AddTransaction(Transaction transaction)
        {
            await _context.transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction> GetTransaction(string AgreementNo)
        {
            return await _context.transactions.FindAsync(AgreementNo);
        }

        public async Task<List<Transaction>> GetTransactions()
        {
            return await _context.transactions.ToListAsync();
        }

        public async Task<List<TransactionDetail>> GetTransactionWithDetail()
        {
            List<TransactionDetail> tranDetail = new List<TransactionDetail>();

            tranDetail = await (from t in _context.transactions
                          join l in _context.locations
                          on t.LocationId equals l.Id
                          select new TransactionDetail
                          {
                              AgreementNumber = t.AgreementNumber,
                              BpkbDate = t.BpkbDate,
                              BpkbDateInput = t.BpkbDateInput,
                              BpkbNumber = t.BpkbNumber,
                              BranchId = t.BranchId,
                              FakturDate = t.FakturDate,
                              FakturNumber = t.FakturNumber,
                              LocationId = t.LocationId,
                              LocationName = l.Name,
                              PoliceNumber = t.PoliceNumber
                          }).ToListAsync();

            return tranDetail;
        }
    }
}
