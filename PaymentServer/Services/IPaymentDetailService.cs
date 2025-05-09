using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentProject.Models;
using PaymentProject.Models.DTOs;

namespace PaymentProject.Services
{
    public interface IPaymentDetailService
    {
        Task<IEnumerable<PaymentDetail>> GetAllPaymentDetailsAsync();
        Task<PaymentDetail> GetPaymentDetailByIdAsync(int id);
        Task<PaymentDetail> CreatePaymentDetailAsync(PaymentDetailDTO paymentDetailDto);
        Task<PaymentDetail> UpdatePaymentDetailAsync(int id, PaymentDetailDTO paymentDetailDto);
        Task<bool> DeletePaymentDetailAsync(int id);
        Task<bool> PaymentDetailExistsAsync(int id);
    }
} 