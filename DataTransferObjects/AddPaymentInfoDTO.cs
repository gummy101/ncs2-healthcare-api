namespace HealthcareApi.DataTransferObjects
{
    public class AddPaymentInfoDTO
    {
        //public string CreditCardNumber { get; set; }
        //public string CreditCardType { get; set; }
        //public DateTime ExpiryDate { get; set; }
        //public int CVV { get; set; }'
        public int UserId { get; set; }
        public string AccountId { get; set; }
        public double Amount { get; set; }

        
    }
}
