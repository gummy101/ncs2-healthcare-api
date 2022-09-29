using HealthcareApi.DataTransferObjects;
using HealthcareApi.Models;
namespace HealthcareApi.Interface
{
    public interface IPayment
    {
        public List<PaymentInfo> GetAllPayments();
        public PaymentInfo? GetPaymentById(int id);
        public PaymentInfo? AddPayment(PaymentInfo entity);
        public PaymentInfo? AddPayment(AddPaymentInfoDTO paymentdto);
        public PaymentInfo? UpdatePayment(PaymentInfo entity);
        public PaymentInfo? UpdatePayment(int id, double amount);
        public PaymentInfo? DeletePaymentById(int id);
    }
}
