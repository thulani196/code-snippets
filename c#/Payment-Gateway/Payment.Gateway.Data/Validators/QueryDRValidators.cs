
namespace Payment.Gateway.Data.Validators
{
    using FluentValidation;
    using Models;

    public class QueryDRValidators : AbstractValidator<QueryDRModel>
    {
        public QueryDRValidators()
        {
            RuleFor(payment => payment.VpcMerchTxnRef).NotEmpty();
        }
    }
}
