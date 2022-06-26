using FCBlockchain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCBlockchain.Services.Interfaces
{
    public interface IBlockchainService
    {
        Task<SCResponseDTO> SendSmartContract(Object sendObject);
        Task<SCResponseDTO> GetSmartContractByAddress(string Address);
    }
}
