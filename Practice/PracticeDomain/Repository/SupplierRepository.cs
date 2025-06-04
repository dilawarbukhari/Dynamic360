using Microsoft.EntityFrameworkCore;
using PracticeDomain.DTOs.Category;
using PracticeDomain.DTOs.Response;
using PracticeDomain.DTOs.Supplier;
using PracticeDomain.IRepository;
using PracticeModel.DataModel;
using PracticeModel.DBcontext;


namespace PracticeDomain.Repository
{
    public class SupplierRepository:ISupplierRepository
    {
        private readonly BaseContext _dbContext;
         public SupplierRepository(BaseContext dbContext) 


           {
           _dbContext= dbContext ;
        }

    public async Task<Response> AddSupplier(SupplierRequest request)
    {
            if (request is not null)
            {
                var supplier = new Supplier
                {
                    Name = request.Name,
                    ContactPerson=request.Contactperson,
                    Phone = request.Phone,
                    Email = request.Email,
                    Address = request.Address

                };
                _dbContext.Suppliers.Add(supplier);
                await _dbContext.SaveChangesAsync();

                return new Response
                {
                    Message = "Supplier successfully added",
                    Success = true
                };
            }
            return new Response
            {
                Message = "Supplier not added",
                Success = false
            };
        }
        public async Task<Response> UpdateSupplier(SupplierRequest request)
        {

            var update = await _dbContext.Suppliers.Where(x => x.SupplierId == request.SupplierId && x.Deleted != true).FirstOrDefaultAsync();

            if (update is null)
            {
                return new Response
                {
                    Message = "NUll",
                    Success = false

                };
            }
            update.Name = request.Name;
            update.ContactPerson = request.Contactperson;
            update.Email= request.Email;
            update.Phone = request.Phone;
            update.Address = request.Address;
            await _dbContext.SaveChangesAsync();
            return new Response
            {
                Message = "supplier successfully updated.",
                Success = true

            };

        }

        public async Task<Response> GetAllSupplier()
        {
            var suppliers = await _dbContext.Suppliers.Where(x => x.Deleted != true).ToListAsync();
            if (!suppliers.Any())
            {
                return new Response
                {
                    Message = "supplier not found",
                    Success = true,
                    Data = suppliers
                };

            }
            return new Response
            {
                Message = "supplier retrieved successfully",
                Success = true,
                Data = suppliers
            };
        }


        public async Task<Response> DeleteSupplier(int Id)
        {
            var supplier = await _dbContext.Suppliers.Where(x => x.SupplierId == Id && x.Deleted != true).FirstOrDefaultAsync();
            if (supplier != null)
            {
                supplier.Deleted = true;
                await _dbContext.SaveChangesAsync();
                return new Response
                {
                    Message = "supplier deleted successfully",
                    Success = true
                };
            }
            return new Response
            {
                Message = "supplier not deleted",
                Success = false
            };
        }
    }
}
