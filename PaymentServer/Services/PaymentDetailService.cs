using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentProject.Data;
using PaymentProject.Models;
using PaymentProject.Models.DTOs;

namespace PaymentProject.Services
{
    public class PaymentDetailService : IPaymentDetailService
    {
        private readonly PaymentDbContext _context;
        private readonly ILogger<PaymentDetailService> _logger;

        public PaymentDetailService(PaymentDbContext context, ILogger<PaymentDetailService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<PaymentDetail>> GetAllPaymentDetailsAsync()
        {
            _logger.LogInformation("Getting all payment details");
            return await _context.PaymentDetails.ToListAsync();
        }

        public async Task<PaymentDetail> GetPaymentDetailByIdAsync(int id)
        {
            _logger.LogInformation($"Getting payment detail with ID: {id}");
            return await _context.PaymentDetails.FindAsync(id);
        }

        public async Task<PaymentDetail> CreatePaymentDetailAsync(PaymentDetailDTO paymentDetailDto)
        {
            _logger.LogInformation("Creating new payment detail");
            
            var paymentDetail = new PaymentDetail
            {
                CardOwnerName = paymentDetailDto.CardOwnerName,
                CardNumber = MaskCardNumber(paymentDetailDto.CardNumber),
                ExpirationDate = paymentDetailDto.ExpirationDate,
                CVC = paymentDetailDto.CVC
            };

            _context.PaymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();
            
            return paymentDetail;
        }

        public async Task<PaymentDetail> UpdatePaymentDetailAsync(int id, PaymentDetailDTO paymentDetailDto)
        {
            _logger.LogInformation($"Updating payment detail with ID: {id}");
            
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return null;
            }

            paymentDetail.CardOwnerName = paymentDetailDto.CardOwnerName;
            paymentDetail.CardNumber = MaskCardNumber(paymentDetailDto.CardNumber);
            paymentDetail.ExpirationDate = paymentDetailDto.ExpirationDate;
            paymentDetail.CVC = paymentDetailDto.CVC;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, $"Concurrency error while updating payment detail with ID: {id}");
                throw;
            }

            return paymentDetail;
        }

        public async Task<bool> DeletePaymentDetailAsync(int id)
        {
            _logger.LogInformation($"Deleting payment detail with ID: {id}");
            
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return false;
            }

            _context.PaymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PaymentDetailExistsAsync(int id)
        {
            return await _context.PaymentDetails.AnyAsync(e => e.PaymentDetailsID == id);
        }

        private string MaskCardNumber(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 4)
                return cardNumber;

            return new string('*', cardNumber.Length - 4) + cardNumber.Substring(cardNumber.Length - 4);
        }
    }
} 