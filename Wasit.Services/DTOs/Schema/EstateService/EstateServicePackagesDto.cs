using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasit.Services.DTOs.Schema.EstateService
{
    public class EstateServicePackagesDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Period { get; set; }
        public int MaxUsageCount { get; set; }
        public double Price { get; set; }
        public string Details { get; set; }
    }
}
