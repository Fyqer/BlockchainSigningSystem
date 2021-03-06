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
    public class BlockchainService : IBlockchainService
    
    {
        private readonly string senderAddress = "0x12890d2cce10226644c59daE5baed380d84830";
            private readonly string password = "password";
        private readonly string byteCode = "ciag_znakowz.bin";
        private readonly string abi = @"[{'inputs':[{'internalType':'string','name':
            'message','type':'string'}],'stateMutability':'nonpayable',
    'type':'constructor'},{'inputs':[],'name':'getOriginalMessage','outputs':
        [{'internalType':'string','name':'temp','type':'string'}]
    ,'stateMutability':'view','type':'function'},{'inputs':[],'name':'object','outputs':[{'internalType':'
            string','name':'','type':'string'}],'stateMutability':'view','type':'function'}]";

        public async Task<SCResponseDTO> GetSmartContractByAddress(string Address)
        {
            var web3 = new Web3Geth();
            var result = string.Empty;

            var contract = web3.Eth.GetContract(abi,Address);
            var contractBody = contract.GetFunction("getOriginalMessage");
            try
            {
                result = await contractBody.CallAsync<string>();
            }
            catch(Exception e)
            {
                return new SCResponseDTO { Code = "400", Message = "Error", Status = "Failed" };
            }

            return new SCResponseDTO { ResponseObject = result, Code = "200", Message = "OK", Status = "OK" };

        }
        public async Task<SCResponseDTO> SendSmartContract(object sendObject)
        {
            var web3 = new Web3Geth();

            web3.TransactionManager.DefaultGas = BigInteger.Parse("900000");
            web3.TransactionManager.DefaultGasPrice = BigInteger.Parse("1");

            try
            {
                var Unlock = await web3.Personal.UnlockAccount.SendRequestAsync(senderAddress, password, new ulong(), "tEST");
                Thread.Sleep(1000);
                var hash = await web3.Eth.DeployContract.SendRequestAsync(abi, byteCode, senderAddress, $"{sendObject.ToString()}");
                var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(hash);

                while(receipt == null)
                {
                    Thread.Sleep(5000);
                    receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(hash);
                }
                var contractAdres = receipt.ContractAddress;
                return new SCResponseDTO { ResponseObject = contractAdres, Code = "200", Message = "OK", Status = "OK" };

            }
            catch (Exception e)
            {
                return new SCResponseDTO { Code = "400", Message = "Error trying add smart conteract oto blockchain", Status = "Failed" };
            }
        }
    }
}
