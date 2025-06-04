using PracticeDomain.DTOs.Category;
using PracticeDomain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeDomain.IRepository

{
    public interface ICatergoryRepository
    {
        Task<Response> AddCategory(CatergoryRequest request);
        Task<Response> UpdateCategory(CatergoryRequest request);
        Task<Response> DeleteCategory(int Id);
     Task<Response> GetAllCategory();
    }
}
