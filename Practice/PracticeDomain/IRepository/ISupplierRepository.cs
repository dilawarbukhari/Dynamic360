using PracticeDomain.DTOs.Response;
using PracticeDomain.DTOs.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeDomain.IRepository
{
    public interface ISupplierRepository
    {
        Task<Response> AddSupplier(SupplierRequest request);
        Task<Response> UpdateSupplier(SupplierRequest request);
        Task<Response> GetAllSupplier();
        Task<Response> DeleteSupplier(int Id);
    }
}
