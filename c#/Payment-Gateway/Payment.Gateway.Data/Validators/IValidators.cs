

namespace Payment.Gateway.Data.Validators
{
    using Models;
    using Payment.Gateway.Data.Models.MtnModels;

    public interface IValidators
    {
        /// <summary>
        /// Validates the Transaction Payload for ECOBANK VPC API.
        /// </summary>
        /// <param name="data"></param>
        void ValidateTransactionModel(TransactionModel data);

        /// <summary>
        /// Validates the Transaction Query Payload for the ECOBANK API.
        /// </summary>
        /// <param name="data"></param>
        void ValidateQueryDRModel(QueryDRModel data);

        /// <summary>
        /// Validates the Refund Transaction payload for the ECOBANK API.
        /// </summary>
        /// <param name="data"></param>
        void ValidateRefundModel(RefundModel data);

        /// <summary>
        /// Validates the VoidTransaction payload for the ECOBANK API
        /// </summary>
        /// <param name="data"></param>
        void ValidateVoidTransactionModel(VoidTransactionModel data);

        /// <summary>
        /// Validates the MTN Transaction reference number thats used to retrieve
        /// a previously made transaction.
        /// </summary>
        /// <param name="data"></param>
        void ValidateMtnReferenceNumberModel(TransactionReference data);

        /// <summary>
        /// Validates the MTN Transaction Payload.
        /// </summary>
        /// <param name="data"></param>
        void ValidateMtnTransactionModel(MTNTransactionModel data);

    }
}