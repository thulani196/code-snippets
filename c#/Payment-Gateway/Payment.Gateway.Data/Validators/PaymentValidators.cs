
namespace Payment.Gateway.Data.Validators
{
    using FluentValidation;
    using FluentValidation.Results;
    using Models;
    using System.IO;
    using System.Linq;

    public class PaymentValidators : AbstractValidator<TransactionModel> 
    {
        public PaymentValidators()
        {
            RuleFor(payment => payment.VpcAmount).NotEmpty();
            RuleFor(payment => payment.VpcOrderInfo).NotEmpty();
            RuleFor(payment => payment.VpcCurrency).NotEmpty();
            RuleFor(payment => payment.VpcCard).NotEmpty();
            RuleFor(payment => payment.VpcCardNum).NotEmpty();
            RuleFor(payment => payment.VpcCardExp).NotEmpty();
            RuleFor(payment => payment.VpcCardSecurityCode).NotEmpty();
        }

        public override ValidationResult Validate(ValidationContext<TransactionModel> context)
        {
            var result =  base.Validate(context);
            if (!result.IsValid)
            {
                throw new InvalidDataException(result.Errors.FirstOrDefault()?.ErrorMessage);
            }

            return result;
        }
    }
}
