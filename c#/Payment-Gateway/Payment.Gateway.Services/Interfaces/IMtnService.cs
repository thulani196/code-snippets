
namespace Payment.Gateway.Logic.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Payment.Gateway.Data.Models.MtnModels;

    /// <summary>
    /// IMtnService defines methods that are used in for the MTN MOMO API
    /// </summary>
    public interface IMtnService
    {

        /// <summary>
        /// Definition for a function that handles MTN Momo payments, taking a Json Payload as input
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<string> SubmitPaymentAsync(MTNTransactionModel transaction);

        /// <summary>
        /// Definition for a function that handles transaction information retrieval, taking a transaction
        /// reference number as String Input.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        Task<string> RetrievePaymentDetailsAsync(string uuid);
    }
}
