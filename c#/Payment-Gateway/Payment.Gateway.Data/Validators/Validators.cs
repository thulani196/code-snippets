
namespace Payment.Gateway.Data.Validators
{
    using Models;
    using Payment.Gateway.Data.Models.MtnModels;

    public class Validators : IValidators
    {
        private readonly PaymentValidators _paymentValidators;
        private readonly QueryDRValidators _queryDrValidators;
        private readonly RefundValidators _refundValidators;
        private readonly VoidPaymentValidators _voidPaymentValidators;
        private readonly MTNReferenceValidators _mtnReferenceValidators;
        public readonly MTNTransactionValidators _mtnTransactionValidators;


        public Validators(
            PaymentValidators paymentValidators, 
            QueryDRValidators queryDrValidators, 
            RefundValidators refundValidators, 
            VoidPaymentValidators voidPaymentValidators, 
            MTNReferenceValidators mtnReferenceValidators, 
            MTNTransactionValidators mtnTransactionValidators)
        {
            _paymentValidators = paymentValidators;
            _queryDrValidators = queryDrValidators;
            _refundValidators = refundValidators;
            _voidPaymentValidators = voidPaymentValidators;
            _mtnReferenceValidators = mtnReferenceValidators;
            _mtnTransactionValidators = mtnTransactionValidators;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void ValidateTransactionModel(TransactionModel data)
        {
            _paymentValidators.Validate(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void ValidateQueryDRModel(QueryDRModel data)
        {
            _queryDrValidators.Validate(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void ValidateRefundModel(RefundModel data)
        {
            _refundValidators.Validate(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void ValidateVoidTransactionModel(VoidTransactionModel data)
        {
            _voidPaymentValidators.Validate(data);
        }

        public void ValidateMtnReferenceNumberModel(TransactionReference data)
        {
            _mtnReferenceValidators.Validate(data);
        }

        public void ValidateMtnTransactionModel(MTNTransactionModel data)
        {
            _mtnTransactionValidators.Validate(data);
        }
    }
}
