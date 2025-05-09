using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentProject.Models;
using PaymentProject.Models.DTOs;
using PaymentProject.Services;

namespace PaymentProject.Controllers
{
    /// <summary>
    /// Controller for managing payment details
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly IPaymentDetailService _paymentDetailService;
        private readonly ILogger<PaymentDetailController> _logger;

        public PaymentDetailController(
            IPaymentDetailService paymentDetailService,
            ILogger<PaymentDetailController> logger)
        {
            _paymentDetailService = paymentDetailService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all payment details
        /// </summary>
        /// <returns>A list of payment details</returns>
        /// <response code="200">Returns the list of payment details</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetPaymentDetails()
        {
            try
            {
                var paymentDetails = await _paymentDetailService.GetAllPaymentDetailsAsync();
                return Ok(paymentDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting payment details");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving payment details");
            }
        }

        /// <summary>
        /// Gets a specific payment detail by ID
        /// </summary>
        /// <param name="id">The ID of the payment detail</param>
        /// <returns>The payment detail</returns>
        /// <response code="200">Returns the payment detail</response>
        /// <response code="404">If the payment detail is not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
        {
            try
            {
                var paymentDetail = await _paymentDetailService.GetPaymentDetailByIdAsync(id);
                if (paymentDetail == null)
                {
                    return NotFound();
                }
                return Ok(paymentDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting payment detail with ID: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the payment detail");
            }
        }

        /// <summary>
        /// Updates a payment detail
        /// </summary>
        /// <param name="id">The ID of the payment detail to update</param>
        /// <param name="paymentDetailDto">The updated payment detail data</param>
        /// <returns>The updated payment detail</returns>
        /// <response code="200">Returns the updated payment detail</response>
        /// <response code="400">If the payment detail data is invalid</response>
        /// <response code="404">If the payment detail is not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutPaymentDetail(int id, PaymentDetailDTO paymentDetailDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedPaymentDetail = await _paymentDetailService.UpdatePaymentDetailAsync(id, paymentDetailDto);
                if (updatedPaymentDetail == null)
                {
                    return NotFound();
                }
                return Ok(updatedPaymentDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating payment detail with ID: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the payment detail");
            }
        }

        /// <summary>
        /// Creates a new payment detail
        /// </summary>
        /// <param name="paymentDetailDto">The payment detail data</param>
        /// <returns>The created payment detail</returns>
        /// <response code="201">Returns the newly created payment detail</response>
        /// <response code="400">If the payment detail data is invalid</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetailDTO paymentDetailDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdPaymentDetail = await _paymentDetailService.CreatePaymentDetailAsync(paymentDetailDto);
                return CreatedAtAction(nameof(GetPaymentDetail), new { id = createdPaymentDetail.PaymentDetailsID }, createdPaymentDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating payment detail");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the payment detail");
            }
        }

        /// <summary>
        /// Deletes a payment detail
        /// </summary>
        /// <param name="id">The ID of the payment detail to delete</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the payment detail was successfully deleted</response>
        /// <response code="404">If the payment detail is not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePaymentDetail(int id)
        {
            try
            {
                var result = await _paymentDetailService.DeletePaymentDetailAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting payment detail with ID: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the payment detail");
            }
        }
    }
}
