using PracticeModel.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeModel.DataModel
{
    public class Supplier:IBaseEntity
    {
        [Key]
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string ContactPerson {  get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

    }
}
