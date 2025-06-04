using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeDomain.DTOs.Category;
using PracticeDomain.IRepository;

namespace Practice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICatergoryRepository _repository;
        public CategoryController (ICatergoryRepository repository)
        {
            _repository= repository;
        }

        [HttpPost]

        public async Task<IActionResult> AddCategory(CatergoryRequest request)
        {
            var response = await _repository.AddCategory(request);
            return Ok(response);
        }

        [HttpPut]

        public async Task<IActionResult> UpdateCategory(CatergoryRequest request)
        {
            var response = await _repository.UpdateCategory(request);
            return Ok(response);
        }


        [HttpDelete]

        public async Task<IActionResult> DeleteCategory(int Id)
        {
            var response = await _repository.DeleteCategory(Id);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var response = await _repository.GetAllCategory();
            return Ok(response);
        }

    }
}
