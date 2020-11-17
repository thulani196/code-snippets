
namespace Payment.Gateway.Data.Validators
{
    using FluentValidation;
    using Models;

    public class VoidPaymentValidators : AbstractValidator<VoidTransactionModel>
    {
        public VoidPaymentValidators()
        {
            RuleFor(payment => payment.VpcMerchTxnRef).NotEmpty();
            RuleFor(payment => payment.VpcTransNo).NotEmpty();
        }
    }
}
