using HealthcareApi.Data;
using HealthcareApi.DataTransferObjects;
using HealthcareApi.Interface;
using HealthcareApi.Models;

namespace HealthcareApi.Repository
{
    public class PaymentRepository :IPayment
    {
        private readonly EHealthDbContext _dbContext;
        private readonly IUser _userContext;
        public PaymentRepository(EHealthDbContext context, IUser userContext)
        {
            _dbContext = context;
            _userContext = userContext;
        }
        public List<PaymentInfo> GetAllPayments()
        {
            return null;
        }
        public PaymentInfo? GetPaymentById(int id)
        {
            try
            {
                return _dbContext.PaymentInfos.Find(id);
            }
            catch
            {
                return null;
            }
        }
        public PaymentInfo? AddPayment(PaymentInfo entity)
        {
            try
            {
                _dbContext.PaymentInfos.Add(entity);
                _dbContext.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }
        public PaymentInfo? AddPayment(AddPaymentInfoDTO paymentdto)
        {
            try
            {
                var pay = new PaymentInfo();
                pay.AccountNumber = paymentdto.AccountId;
                pay.Amount = paymentdto.Amount;
                pay.User = _userContext.GetUserById(paymentdto.UserId);
                AddPayment(pay);
                return pay;
            }
            catch
            {
                return null;
            }
        }
        public PaymentInfo? UpdatePayment(PaymentInfo entity)
        {
            try
            {
                _dbContext.PaymentInfos.Update(entity);
                _dbContext.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
            
        }
        public PaymentInfo?UpdatePayment(int id, double amount)
        {
            var payment = GetPaymentById(id);
            if (payment != null)
            {
                payment.Amount = amount;
            }
            var updpayment = UpdatePayment(payment);
            return updpayment;
        }
        public PaymentInfo? DeletePaymentById(int id)
        {
            try
            {
                PaymentInfo entity = _dbContext.PaymentInfos.Find(id);
                if (entity == null)
                    throw new ArgumentNullException("id param is invalid");
                else
                {
                    _dbContext.PaymentInfos.Remove(entity);
                    _dbContext.SaveChanges();
                    return entity;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
