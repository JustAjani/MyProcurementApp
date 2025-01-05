using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCodeBase.Model
{
    public class CostCenterModel
    {
        public int CostCenterId { get; set; }
        public string CostCenterName { get; set; }
        public bool isActive { get; set; }
    }
}
