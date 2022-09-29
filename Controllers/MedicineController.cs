using HealthcareApi.Data;
using HealthcareApi.Interface;
using HealthcareApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApi.Controllers
{
    [Route("admin")]
    [ApiController]
    [Authorize]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicine _medicineRepo;
        public MedicineController(IMedicine medicineRepo)
        {
            _medicineRepo = medicineRepo;
        }

        [HttpGet("getAllMedicine")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetAllMedicine()
        {
            return await Task.FromResult(_medicineRepo.GetAllMedicine());
        }
        [HttpGet("getMedicineById/{id}")]
        [ProducesResponseType(typeof(Medicine),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Medicine),StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Medicine>> GetMedicineById(int id)
        {
            var medicine = await Task.FromResult(_medicineRepo.GetMedicineById(id));
            if (medicine == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(medicine);
            }
        }
        [HttpPost("addMedicine")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateMedicine(Medicine medicine)
        {
            _medicineRepo.AddMedicine(medicine);
            return CreatedAtAction(nameof(GetMedicineById), new { id = medicine.Id }, medicine);
        }

        [HttpPut("updateMedicine")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMedicine(int id, Medicine medicine)
        {
            if (id != medicine.Id)
                return BadRequest();
            _medicineRepo.UpdateMedicine(medicine);
            return Ok(medicine);            

        }
        [HttpDelete("deleteMedicineById/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var medicine = _medicineRepo.GetMedicineById(id);
            if (medicine == null) 
                return NotFound();
            else
            {
                _medicineRepo.DeleteMedicineById(id);
            }
            return NoContent();
        }
    }
}
