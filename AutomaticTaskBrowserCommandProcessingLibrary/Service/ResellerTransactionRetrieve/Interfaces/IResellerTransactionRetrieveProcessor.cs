using AutomaticTaskSharedLibrary;
using InvoiceRepositoryTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public interface IResellerTransactionRetrieveProcessor
    {
        Task<ResellerTransactionRetrieveResponseDto> Execute(ResellerTransactionRetrieveRequestDto resellerTransactionRetrieveRequestDto);
    }
}
