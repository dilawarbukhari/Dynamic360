using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeDomain.DTOs.Category;
using PracticeDomain.DTOs.Supplier;
using PracticeDomain.IRepository;

namespace Practice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {

        private readonly ISupplierRepository _repository;
        public SupplierController(ISupplierRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]

        public async Task<IActionResult> AddSupplier(SupplierRequest request)
        {
            var response = await _repository.AddSupplier(request);
            return Ok(response);
        }


        [HttpPut]

        public async Task<IActionResult> UpdateSupplier(SupplierRequest request)
        {
            var response = await _repository.UpdateSupplier(request);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSupplier()
        {
            var response = await _repository.GetAllSupplier();
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSupplier(int Id)
        {
            var response = await _repository.DeleteSupplier(Id);
            return Ok(response);
        }

    }
}
