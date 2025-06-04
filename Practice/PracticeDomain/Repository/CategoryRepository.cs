using PracticeModel.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeModel.DBcontext;
using PracticeDomain.DTOs.Response;
using PracticeDomain.IRepository;
using PracticeDomain.DTOs.Category;
using Microsoft.EntityFrameworkCore;

namespace PracticeDomain.Repository
{
    public class CategoryRepository:ICatergoryRepository
    {
        private BaseContext _dbContext;

public CategoryRepository(BaseContext dbContext)
        {
           _dbContext= dbContext ;
        }

        public async Task<Response> AddCategory(CatergoryRequest request)
        {
            if (request is not null)
            {
                var category = new Categories
                {
                    Name = request.Name,

                };
                _dbContext.Categories.Add(category);
                await _dbContext.SaveChangesAsync();

                return new Response
                {
                    Message = "Category successfully added",
                    Success = true 
                };
            }

            return new Response
            {
                Message = "Request is null",
                Success = false
            };
        }

        public async Task<Response> UpdateCategory(CatergoryRequest request)
        {

            var update = await _dbContext.Categories.Where(x => x.CategoryId == request.CategoryId && x.Deleted != true).FirstOrDefaultAsync();

            if (update is null)
            {
                return new Response
                {
                    Message = "NUll",
                    Success = false

                };
            }
                update.Name=request.Name;
                await _dbContext.SaveChangesAsync();
            return new Response
            {
                Message = "category successfully updated.",
                Success = true

            };

        }
        public async Task<Response> GetAllCategory()
        {
            var categories = await _dbContext.Categories.Where(x=>x.Deleted !=true).ToListAsync();
            return new Response
            {
                Message = "Categories retrieved successfully",
                Success = true,
                Data = categories
            };
        }

        public async Task<Response> DeleteCategory(int Id)
        {
            var categories = await _dbContext.Categories.Where(x => x.CategoryId == Id && x.Deleted != true).FirstOrDefaultAsync();
            if (categories != null)
            {
                    categories.Deleted=true;
                await _dbContext.SaveChangesAsync();
                return new Response
                {
                    Message = "Categories deleted successfully",
                    Success = true
                };
            }
            return new Response
            {
                Message = "Error Occur during delete",
                Success = false
            };
        }

    }
}
