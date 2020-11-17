namespace Payment.Gateway.Api.Controllers
{
    using Data.Validators;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Payment.Gateway.Data.Models;
    using Payment.Gateway.Logic.Interfaces;

    /// <summary>
    /// Defines the <see cref="ControllerBase" />
    /// </summary>
    public class ControllerBase : Controller
    {
        /// <summary>
        /// Defines the _logger
        /// </summary>
        internal readonly ILogger<PaymentController> _logger;

        /// <summary>
        /// Defines the _mtnLogger
        /// </summary>
        internal readonly ILogger<MtnPaymentController> _mtnLogger;

        /// <summary>
        /// Defines the _validators
        /// </summary>
        internal readonly IValidators _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerBase"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger{PaymentController}"/></param>
        /// <param name="validators">The validators<see cref="IValidators"/></param>
        public ControllerBase(ILogger<PaymentController> logger, IValidators validators)
        {
            _logger = logger;
            _validators = validators;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerBase"/> class.
        /// </summary>
        /// <param name="mtnLogger">The mtnLogger<see cref="ILogger{MtnPaymentController}"/></param>
        /// <param name="validators">The validators<see cref="IValidators"/></param>
        public ControllerBase(ILogger<MtnPaymentController> mtnLogger, IValidators validators)
        {
            _mtnLogger = mtnLogger;
            _validators = validators;
        }

        /// <summary>
        /// The ProcessResult
        /// </summary>
        /// <param name="response">The response<see cref="ResponseModel"/></param>
        /// <returns>The <see cref="IActionResult"/></returns>
        public IActionResult ProcessResult(ResponseModel response)
        {
            if (response == null)
            {
                return new EmptyResult();
            }
            return StatusCode(200, response);
        }

    
    }
}
