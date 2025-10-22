using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrStatisticsVM
    {
        public string? StatusName { get; set; }    
        public string? StatusName2 { get; set; }
        public string? Icon { get; set; }
        public string? Color { get; set; }
        public int Count { get; set; }
        public string? Route { get; set; }

        /// <summary>
        /// 1 mean is the first section of statistics 
        /// 2 mean is the Second section of statistics 
        /// </summary>
        public int StatisticType { get; set; }
    }
}
