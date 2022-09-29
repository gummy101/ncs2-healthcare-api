namespace HealthcareApi.DataTransferObjects
{
    public class AddNewCartItemDTO
    {
        public int CartId { get; set; }
        public int MedicineId { get; set; }
        public DateTime CreatedDate { get; set; }   
        public DateTime UpdatedDate { get; set; }

        public AddNewCartItemDTO()
         {
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
    }
}
