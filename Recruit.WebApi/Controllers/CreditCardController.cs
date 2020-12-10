using Microsoft.AspNetCore.Mvc;
using Recruit.WebApi.Attributes;
using Recruit.WebApi.Services.Interfaces;
using Recruit.WebApi.Services.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace Recruit.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuth]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, description:"Unknown API key")]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardService creditCardService;

        public CreditCardController(ICreditCardService creditCardService)
        {
            this.creditCardService = creditCardService ?? throw new System.ArgumentNullException(nameof(creditCardService));
        }

        /// <summary>
        /// Returns all the credit cards.
        /// </summary>
        /// <returns>Collection of <see cref="CreditCardDto"/></returns>
        [HttpGet]
        [SwaggerOperation(description: "Gets all the credit cards saved in the system.")]
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Successfully completed operation")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, description: "Error occurred while completing the operation")]
        public async Task<ActionResult> GetAllAsync()
        {
            return Ok(await creditCardService.GetAllAsync());
        }

        /// <summary>
        /// Returns all the credit cards.
        /// </summary>
        /// <returns>Collection of <see cref="CreditCardDto"/></returns>
        [HttpGet("{creditCardNumber}")]
        [SwaggerOperation(description: "Gets all the credit cards saved in the system.")]
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Successfully completed operation")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, description: "Error occurred while completing the operation")]
        public async Task<ActionResult> GetByCCNumberAsync(string creditCardNumber)
        {
            return Ok(await creditCardService.GetAsync(creditCardNumber));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult> SaveAsync(CreditCardDto creditCard)
        {
            await creditCardService.CreateAsync(creditCard);
            return Ok();
        }
    }
}