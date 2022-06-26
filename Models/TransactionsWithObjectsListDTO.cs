using System;
using System.Collections.Generic;

#nullable disable

namespace FCBlockchain.Models
{
    public class TransactionsWithObjectsListDTO
    {
       public IList<TransactionsWithObjectsDTO> TransactionList { get; set; }
    }
}
