
namespace Payment.Gateway.UnitTests
{
    using Data.Models;
    using Data.Validators;
    using NUnit.Framework;
    using System.IO;

    public class Tests
    {
        [SetUp]
        public void ValidateExceptionIsThrown()
        {
            var validator = new PaymentValidators();

            var dummyData = new TransactionModel
            {
                VpcAmount = string.Empty
            };

            Assert.Throws(typeof(InvalidDataException), delegate { validator.Validate(dummyData); });
        }
    }
}