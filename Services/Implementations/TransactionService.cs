using FCBlockchain.Extensions.Enums;
using FCBlockchain.Models;
using FCBlockchain.Services.Interfaces;
using Nethereum.Geth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace FCBlockchain.Services.Implementations
{
    public class TransactionService : ITransactionService

    {

        private readonly FCBlockchainContext context;
        private readonly IBlockchainService blockchainService;



        public TransactionService(FCBlockchainContext context, IBlockchainService service)
        {
            this.blockchainService = service;
            this.context = context;
        }

        public TransactionsWithObjectsListDTO GetTransactionsByUserID(int userID, TransactionType transType)
        {
            TransactionsDTO transactions = new TransactionsDTO() { TransactionList = new List<TransactionList>() };
            if (transType == TransactionType.Send)
            {
                transactions.TransactionList = context.TransactionLists.Where(t => t.Sender == userID).ToList();
            }
            else
            {
                transactions.TransactionList = context.TransactionLists.Where(t => t.Receiver == userID).ToList();

            }

            TransactionsWithObjectsListDTO fullTransactionList
            = new TransactionsWithObjectsListDTO()
            {
                TransactionList = new
            List<TransactionsWithObjectsDTO>()
            };
            foreach (TransactionList tran in transactions.TransactionList)
            {

                var fulltransaction = new TransactionsWithObjectsDTO()
                {
                    Data = tran.Date,
                    Sender = tran.Sender,
                    Receiver = tran.Receiver,
                    SendAddress = tran.SendAddress,
                    ReceiveAddress = tran.ReceiveAddress,
                    SendObject = blockchainService.GetSmartContractByAddress(tran.SendAddress),
                    ReceiveObject = blockchainService.GetSmartContractByAddress(tran.ReceiveAddress),
                };
                fullTransactionList.TransactionList.Add(fulltransaction);
            }
            return fullTransactionList;

        }
        public ResponseDTO SendTransaction(int firstUserID, int second, object senObject, TransactionType transactiontype, int transID)
        {
        if(transactiontype == TransactionType.Send)
            {
                var response = blockchainService.SendSmartContract(senObject);
                if(response.Result.Code == "400")
                {
                    return response.Result;
                }
                TransactionList transaction = new TransactionList()
                {
                    Date = DateTime.Now,
                    Sender = firstUserID,
                    Receiver = second,
                    SendAddress = response.Result.ResponseObject
                };
                try
                {
                    context.Add(transaction);
                    context.SaveChanges();
                }
                catch(Exception e)
                {
                    return new ResponseDTO() { Code = "400", Message =$"Failed durning add transation object t odb, error:{e.Message}", Status = "Failed" };
                }
                return new ResponseDTO() { Code = "200", Message = $"added transaciton to object", Status = "Success" };


            }
         else
            {
                var response = blockchainService.SendSmartContract(senObject);
                if (response.Result.Code == "400")
                {
                    return response.Result;
                }
                var transactionToModify = context.TransactionLists.Where(t => t.Id == transID).SingleOrDefault();
                if(transactionToModify == null)
                {
                    return new ResponseDTO() { Code = "400", Message = $"Transaciton with id :{transID} doesnt exist", Status = "Failed" };
                }
                transactionToModify.ReceiveAddress = response.Result.ResponseObject;
                try
                {
                    context.Update(transactionToModify);
                }
                catch(Exception e)
                {
                    return new ResponseDTO() { Code = "400", Message = $"Failed durning add transation object t odb, error:{e.Message}", Status = "Failed" };

                }
                return new ResponseDTO() { Code = "200", Message = $"added transaciton to object", Status = "Success" };

            }

        }
    }
}