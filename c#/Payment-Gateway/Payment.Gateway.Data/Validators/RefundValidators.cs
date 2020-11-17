
namespace Payment.Gateway.Data.Validators
{
    using FluentValidation;
    using Models;

    public class RefundValidators : AbstractValidator<RefundModel>
    {
        public RefundValidators()
        {
            RuleFor(refund => refund.VpcAmount).NotEmpty();
            RuleFor(refund => refund.VpcMerchTxnRef).NotEmpty();
            RuleFor(refund => refund.VpcTransNo).NotEmpty();
        }
    }
}
