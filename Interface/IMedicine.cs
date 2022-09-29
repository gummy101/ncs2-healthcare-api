using HealthcareApi.Models;

namespace HealthcareApi.Interface
{
    public interface IMedicine
    {
        public List<Medicine> GetAllMedicine();
        public Medicine? GetMedicineById(int id);
        public Medicine? AddMedicine(Medicine medicine);
        public Medicine? UpdateMedicine(Medicine medicine);
        public Medicine? DeleteMedicineById(int id);

    }
}
