using FCBlockchain.Extensions.Enums;
using FCBlockchain.Models;
using FCBlockchain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCBlockchain.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService   transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }
        [Route("api/transaction/getTransactionById")]
        [HttpGet]
        public TransactionsWithObjectsListDTO  GetTransactionsByUserID(int userID, TransactionType transactionType = TransactionType.Send)
        {
            return transactionService.GetTransactionsByUserID(userID, transactionType);
        }

        [Route("api/transactions/sendTransaction")]
        [HttpGet]
        public ResponseDTO SendTransaction(int firstUserID, int second, object senObject, TransactionType transactiontype, int transID)
        {
            return transactionService.SendTransaction(firstUserID, second, senObject, transactiontype, transID);
        }
    }
}
