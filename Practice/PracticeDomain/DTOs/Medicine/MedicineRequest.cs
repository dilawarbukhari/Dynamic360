
namespace PracticeDomain.DTOs.Medicine
{
    public class Medicine
    {
        public class MedicineRequest
        {
            public int MedicineId { get; set; }
            public string MedicineName { get; set; }
            public int CategoryId { get; set; }
            public int SupplierId { get; set; }
            public string BatchNumber { get; set; }
            public DateTime ExpiryDate { get; set; }
            public decimal Price { get; set; }
            public int StockQuantity { get; set; }

        }

        public class MedicineRequestList
        {
            public string Name { get; set; }
            public int CategoryId { get; set; }
            public int SupplierId { get; set;}
        }


        public class MedicineResponseList
        {
            public int MedicineId { get; set; }
            public string MedicineName { get; set; }
            public int CategoryId { get; set; }
            public string CategoryName {  get; set; }

            public int SupplierId { get; set; }
            public string SupplierName { get; set; }
            public string BatchNumber { get; set; }
            public int StockQuantity { get; set; }
            public decimal Price { get; set; }
            public DateTime ExpiryDate {  get; set; }

            public int ReorderLevel {  get; set; }
            public bool Deleted {  get; set; }
        }

    }
}