using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeModel.BaseEntity
{
    public abstract class IBaseEntity
    {
        public string CreatedBy { get; set; }  // User who created the record
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;  // Creation timestamp
        public string? ModifiedBy { get; set; }  // User who last modified the record
        public DateTime? ModifiedDate { get; set; }  // Last modified timestamp
        public bool  Deleted { get; set; } 
    }
    }
