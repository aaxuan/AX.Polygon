using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AX.Polygon.Admin.DataModel.DTO
{
    public class MenuTreeResult
    { 
        public SystemMenu Menu { get; set; }

        public List<MenuTreeResult> Child { get; set; }
    }
}
