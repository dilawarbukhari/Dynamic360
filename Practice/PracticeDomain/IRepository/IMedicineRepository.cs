using PracticeDomain.DTOs.Medicine;
using PracticeDomain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PracticeDomain.DTOs.Medicine.Medicine;

namespace PracticeDomain.IRepository
{
    public interface IMedicineRepository
    {
        Task<Response> AddMedicine(MedicineRequest request);
        Task<Response> UpdateMedicine(MedicineRequest request);
        Task<Response> DeleteMedicine(int Id);
        Task<Response> GetMedicineDetail(MedicineRequestList request);
    }
}
