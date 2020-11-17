namespace Payment.Gateway.Api.Controllers
{
    using Data.Models;
    using Data.Validators;
    using Logic.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="PaymentController" />
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        /// <summary>
        /// Defines the _paymentService
        /// </summary>
        private readonly IPaymentService _paymentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentController"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger{PaymentController}"/></param>
        /// <param name="validators">The validators<see cref="IValidators"/></param>
        /// <param name="paymentService">The paymentService<see cref="IPaymentService"/></param>
        public PaymentController(
            ILogger<PaymentController> logger,
            IValidators validators,
            IPaymentService paymentService) :
            base(logger, validators)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// The ProcessPayment
        /// </summary>
        /// <param name="paymentTransaction">The paymentTransaction<see cref="TransactionModel"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpPost()]
        public async Task<IActionResult> ProcessPayment([FromBody] TransactionModel paymentTransaction)
        {
            _validators.ValidateTransactionModel(paymentTransaction);

            var response = await _paymentService.MakePaymentAsync(paymentTransaction);
            if (string.IsNullOrEmpty(response))
            {
                _logger.LogError("The response from Payment Service is an Empty string.");
                return new EmptyResult();
            }
            return new OkObjectResult(response);
        }

        /// <summary>
        /// The ProcessRefund
        /// </summary>
        /// <param name="refundTransaction">The refundTransaction<see cref="RefundModel"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpPost("refund")]
        public async Task<IActionResult> ProcessRefund([FromBody] RefundModel refundTransaction)
        {
            _validators.ValidateRefundModel(refundTransaction);
            var response = await _paymentService.RefundPaymentAsync(refundTransaction);
            return ProcessResult(response);
        }

        /// <summary>
        /// The VoidPayment
        /// </summary>
        /// <param name="voidTransaction">The voidTransaction<see cref="VoidTransactionModel"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpPost("void")]
        public async Task<IActionResult> VoidPayment([FromBody] VoidTransactionModel voidTransaction)
        {
            _validators.ValidateVoidTransactionModel(voidTransaction);
            var response = await _paymentService.VoidPaymentAsync(voidTransaction);
            return ProcessResult(response);
        }

        /// <summary>
        /// The GetTransactionResponse
        /// </summary>
        /// <param name="paymentResponseData">The paymentResponseData<see cref="PaymentResponseBindModel"/></param>
        /// <returns>The <see cref="object"/></returns>
        [HttpGet("status")]
        [AllowAnonymous]
        public object GetTransactionResponse([FromQuery] PaymentResponseBindModel paymentResponseData)
        {
            return new OkObjectResult(paymentResponseData);
        }

        /// <summary>
        /// The GetTransaction
        /// </summary>
        /// <param name="queryTransaction">The queryTransaction<see cref="QueryDRModel"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [HttpGet("query")]
        public async Task<IActionResult> GetTransaction([FromBody] QueryDRModel queryTransaction)
        {
            _validators.ValidateQueryDRModel(queryTransaction);
            var response = await _paymentService.QueryTransaction(queryTransaction);
            return ProcessResult(response);
        }
    }
}
