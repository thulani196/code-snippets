namespace Payment.Gateway.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Payment.Gateway.Data.Models.MtnModels;
    using Payment.Gateway.Data.Validators;
    using Payment.Gateway.Logic.Interfaces;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="MtnPaymentController" />
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MtnPaymentController : ControllerBase
    {
        /// <summary>
        /// Defines the _mtnService
        /// </summary>
        private readonly IMtnService _mtnService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MtnPaymentController"/> class.
        /// </summary>
        /// <param name="mtnLogger">The mtnLogger<see cref="ILogger{MtnPaymentController}"/></param>
        /// <param name="validators">The validators<see cref="IValidators"/></param>
        /// <param name="mtnService">The mtnService<see cref="IMtnService"/></param>
        public MtnPaymentController(
            ILogger<MtnPaymentController> mtnLogger,
            IValidators validators,
            IMtnService mtnService) : base(mtnLogger, validators)
        {
            _mtnService = mtnService;
        }
       
        /// <summary>
        /// The Pay endpoint ends a request to make a new payment to the MTN API
        /// </summary>
        /// <param name="transaction">The transaction<see cref="MTNTransactionModel"/></param>
        /// <returns>Returns a JSON response of the transaction Status (Failed or succeeded), all the details as well</returns>
        [HttpPost("pay")]
        public async Task<IActionResult> MakePayment([FromBody] MTNTransactionModel transaction)
        {
            _validators.ValidateMtnTransactionModel(transaction);
            var response = await _mtnService.SubmitPaymentAsync(transaction);
            return new OkObjectResult(response);
        } 

        /// <summary>
        /// Retrieves a transaction given the transaction ID
        /// </summary>
        /// <param name="transactionReference"></param>
        /// <returns>Returns JSON body of a transaction (Status and other details) of a transaction made using the transactions's unique ID</returns>
        [HttpGet("retrieve")]
        public async Task<IActionResult> GetPayment([FromBody] TransactionReference transactionReference) {
            _validators.ValidateMtnReferenceNumberModel(transactionReference);
            var response = await _mtnService.RetrievePaymentDetailsAsync(transactionReference.TransactionReferenceID);
            return new OkObjectResult(response);
        }

    }
}
