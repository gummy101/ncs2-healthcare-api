namespace HealthcareApi.DataTransferObjects
{
    public class CartItemDTO
    {
        public int Cartitemid { get; set; }
        public string Medicinename { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }

        public int Quantity { get; set; }
        public int Price { get; set; }

        public CartItemDTO(int id, string mname, DateTime created, DateTime updated, int quantity, int price )
        {
            Cartitemid = id;
            Medicinename = mname;
            Updated = updated;
            Created = created;
            Quantity = quantity;
            Price = price;
        }
    }
}
