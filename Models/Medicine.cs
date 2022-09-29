using System.ComponentModel.DataAnnotations;

namespace HealthcareApi.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        public int Price { get; set; }  
        public DateTime ExpiryDate { get; set; }
        public Medicine()
        {
            CreatedDate = DateTime.Now;
        }

        //public DrugClass DrugClass { get; set; }

        //public DrugType DrugType { get; set; }  
        
        public enum DrugType
        {
            Prescription, NonPrescription
        }

        public enum DrugClass
        {
            ScheduleH, Restricted, FreeUse
        }
       
    }
}
