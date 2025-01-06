using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCodeBase.Model
{
    public class SupplierModel
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public bool IsActive { get; set; }
    }
}
