
using Microsoft.AspNetCore.Mvc;
using PracticeDomain.DTOs.Medicine;

using PracticeDomain.IRepository;
using static PracticeDomain.DTOs.Medicine.Medicine;

namespace Practice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {

        private readonly IMedicineRepository _repository;
        public MedicineController(IMedicineRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]

        public async Task<IActionResult> AddMedicine(MedicineRequest request)
        {
            var response = await _repository.AddMedicine(request);
            return Ok(response);
        }

        [HttpPut]

        public async Task<IActionResult> UpdateMedicine(MedicineRequest request)
        {
            var response = await _repository.UpdateMedicine(request);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMedicine(int Id)
        {
            var response = await _repository.DeleteMedicine(Id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetMedicineDetail(MedicineRequestList request)
        {
            var response = await _repository.GetMedicineDetail(request);
            return Ok(response);
        }
    }
}
