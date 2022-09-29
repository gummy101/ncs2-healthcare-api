namespace HealthcareApi.DataTransferObjects
{
    public class CreateOrderDTO
    {
        public string OrderStatus { get; set; }
        public int UserId { get; set; }
        public int CartId { get; set; }
        //paymentinfo id
        public int PaymentInfoId { get; set; }
        public int BillAddrId { get; set; } 
        public int ShipAddrId { get; set; }
        
    }
}
