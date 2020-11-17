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
    public class MTNReferenceValidators : AbstractValidator<TransactionReference>
    {
        public MTNReferenceValidators()
        {
            RuleFor(reference => reference.TransactionReferenceID).NotEmpty().MinimumLength(36).MaximumLength(36);
        }

        public override ValidationResult Validate(ValidationContext<TransactionReference> context)
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
