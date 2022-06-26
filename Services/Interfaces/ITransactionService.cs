using FCBlockchain.Extensions.Enums;
using FCBlockchain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCBlockchain.Services.Interfaces
{
    public interface ITransactionService
    {
        ResponseDTO SendTransaction(int firstUserID, int second, object senObject, TransactionType transactiontype, int transID);
        TransactionsWithObjectsListDTO GetTransactionsByUserID(int userID, TransactionType transType);

    }
}
