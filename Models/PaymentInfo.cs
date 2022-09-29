namespace HealthcareApi.Models
{
    public class PaymentInfo
    {
        public int Id { get; set; }
        /*public string CreditCardNumber { get; set; }
        public string CreditCardType { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CVV { get; set; }*/
        
        public string AccountNumber { get; set; }
        public Double Amount { get; set; }
        public User User { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }   
        public PaymentInfo()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
        
    }
}
