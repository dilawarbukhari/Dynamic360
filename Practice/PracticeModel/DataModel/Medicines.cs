

using PracticeModel.BaseEntity;
using System.ComponentModel.DataAnnotations;

namespace PracticeModel.DataModel
{
    public class Medicines:IBaseEntity
    {
        [Key]
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public  int CategoryId {  get; set; }
        public int SupplierId {  get; set; }
        public string BatchNumber {  get; set; }
        public DateTime ExpiryDate {  get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; } = 5;

    }
}
