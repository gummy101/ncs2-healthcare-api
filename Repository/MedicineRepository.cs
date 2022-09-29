using HealthcareApi.Data;
using HealthcareApi.Interface;
using HealthcareApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApi.Repository
{
    public class MedicineRepository : IMedicine
    {
        private readonly EHealthDbContext _dbContext;

        public MedicineRepository(EHealthDbContext context)
        {
            _dbContext = context;
        }
        public List<Medicine> GetAllMedicine()
        {
            try
            {
                return _dbContext.Medicines.ToList();
            }
            catch
            {
                return new List<Medicine>();
            }
        }
        public Medicine? GetMedicineById(int id)
        {
            try
            {
                return _dbContext.Medicines.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public Medicine AddMedicine(Medicine entity)
        {
            try
            {
                _dbContext.Medicines.Add(entity);
                _dbContext.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public Medicine UpdateMedicine(Medicine entity)
        {
            try
            {
                 _dbContext.Medicines.Update(entity);
                _dbContext.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Medicine? DeleteMedicineById(int id)
        {
            try
            {
                Medicine entity = _dbContext.Medicines.Find(id);
                if (entity == null)
                    throw new ArgumentNullException("id param is invalid");
                else
                {
                    _dbContext.Medicines.Remove(entity);
                    _dbContext.SaveChanges();
                    return entity;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }


    }
}
