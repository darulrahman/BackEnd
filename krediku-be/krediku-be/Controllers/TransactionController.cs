using krediku_be.Data;
using krediku_be.Models;
using krediku_be.Repositories;
using krediku_be.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace krediku_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private ITransactionRepo _transactionRepo;
        private ILocationRepo   _locationRepo;
        public TransactionController(ITransactionRepo transactionRepo, ILocationRepo locationRepo)
        {
            _transactionRepo = transactionRepo;
            _locationRepo = locationRepo;

        }

        [HttpGet("[Action]")]
        public async Task<ActionResult<ApiMessage<List<TransactionDetail>>>> GetTransactions()
        {
            ApiMessage<List<TransactionDetail>> apiRes = new ApiMessage<List<TransactionDetail>>();
            try
            {
                List<TransactionDetail> trans = await _transactionRepo.GetTransactionWithDetail();
                if (trans == null || trans.Count == 0)
                    throw new Exception("Transaction Empty");

                apiRes.isSuccess = true;
                apiRes.data = trans;
            }
            catch (Exception ex)
            {
                apiRes.isSuccess = false;
                apiRes.message = ex.Message;
                if(ex.InnerException != null )
                    apiRes.message += ". InnerExc: "+ex.InnerException.Message;
            }
            finally
            {
                apiRes.messageTime = DateTime.Now;
            }
            return apiRes;
        }

        [HttpGet("[Action]/{AggrNo}")]
        public async Task<ActionResult<ApiMessage<Transaction>>> GetTransaction(string AggrNo)
        {
            ApiMessage<Transaction> apiRes = new ApiMessage<Transaction>();
            try
            {
                Transaction tran = await _transactionRepo.GetTransaction(AggrNo);

                if (tran == null)
                    throw new Exception("Transaction Not Found");

                apiRes.isSuccess = true;
                apiRes.data = tran;
            }
            catch(Exception ex)
            {
                apiRes.isSuccess = false;
                apiRes.message = ex.Message;
                if (ex.InnerException != null)
                    apiRes.message += ". InnerExc:" + ex.InnerException.Message;
            }
            finally
            {
                apiRes.messageTime = DateTime.Now;
            }

            return apiRes;
        }

        [HttpPost("[Action]")]
        public async Task<ActionResult<ApiMessage<Transaction>>> AddTransaction([FromBody]Transaction transaction)
        {
            ApiMessage<Transaction> apiRes = new ApiMessage<Transaction>();
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Input data not valid");

                string strValidationMsg;
                if (!clsHelper.isDataTransactionValid(transaction, out strValidationMsg))
                    throw new Exception(strValidationMsg);

                //Validasi LocationID
                Location loc = await _locationRepo.GetLocationById(transaction.LocationId);
                if(loc == null)
                    throw new Exception("Location ID Not Found");

                Transaction existingTran = await _transactionRepo.GetTransaction(transaction.AgreementNumber);
                if (existingTran != null)
                    throw new Exception("Agreement Number has been used. Please input different Agreement Number");

                Transaction newTran = await _transactionRepo.AddTransaction(transaction);

                apiRes.isSuccess = true;
                apiRes.data = newTran;
            }
            catch(Exception ex)
            {
                apiRes.isSuccess = false;
                apiRes.message = ex.Message;
                if (ex.InnerException != null)
                    apiRes.message += ". InnerExc: " + ex.InnerException.Message;
            }
            finally
            {
                apiRes.messageTime = DateTime.Now;
            }
            return apiRes;
        }
    }
}
