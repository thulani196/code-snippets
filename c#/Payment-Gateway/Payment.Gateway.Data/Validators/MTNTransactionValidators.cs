using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using Payment.Gateway.Data.Models.MtnModels;

namespace Payment.Gateway.Data.Validators
{
    public class MTNTransactionValidators : AbstractValidator<MTNTransactionModel>
    {
        public MTNTransactionValidators()
        {
            RuleFor(payment => payment.TransactionRecord.TransactionReferenceId).NotNull().MinimumLength(36).MaximumLength(36).NotEmpty();
            RuleFor(payment => payment.PaymentRecord).NotNull();
        }

        public override ValidationResult Validate(ValidationContext<MTNTransactionModel> context)
        {
            var result = base.Validate(context);
            if (!result.IsValid)
            {
                throw new InvalidDataException(result.Errors.FirstOrDefault()?.ErrorMessage);
            }

            return result;
        }
    }
}
