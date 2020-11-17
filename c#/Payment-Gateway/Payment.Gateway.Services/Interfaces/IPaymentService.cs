namespace Payment.Gateway.Logic.Interfaces
{
    using Data.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IPaymentService" />
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// The MakePaymentAsync
        /// </summary>
        /// <param name="transaction">The transaction<see cref="TransactionModel"/></param>
        /// <returns>The <see cref="Task{string}"/></returns>
        Task<string> MakePaymentAsync(TransactionModel transaction);

        /// <summary>
        /// The RefundPaymentAsync
        /// </summary>
        /// <param name="refundTransaction">The transaction<see cref="RefundModel"/></param>
        /// <returns>The <see cref="Task{ResponseModel}"/></returns>
        Task<ResponseModel> RefundPaymentAsync(RefundModel refundTransaction);

        /// <summary>
        /// The VoidPaymentAsync
        /// </summary>
        /// <param name="voidTransaction">The transaction<see cref="VoidTransactionModel"/></param>
        /// <returns>The <see cref="Task{ResponseModel}"/></returns>
        Task<ResponseModel> VoidPaymentAsync(VoidTransactionModel voidTransaction);

        /// <summary>
        /// The QueryTransaction
        /// </summary>
        /// <param name="queryTransaction">The transaction<see cref="QueryDRModel"/></param>
        /// <returns>The <see cref="Task{ResponseModel}"/></returns>
        Task<ResponseModel> QueryTransaction(QueryDRModel queryTransaction);
    }
}
