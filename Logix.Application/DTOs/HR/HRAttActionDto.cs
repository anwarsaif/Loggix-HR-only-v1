using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrAttActionDto
    {
        public long Id { get; set; }
       
      
        public DateTime Time { get; set; }
       
        public int? TypeId { get; set; }
      
        public long? EmpId { get; set; }
     
        public string? Sensorid { get; set; }
    
        public string? Memoinfo { get; set; }
      
        public string? WorkCode { get; set; }
       
        public string? Sn { get; set; }
        public short? UserExtFmt { get; set; }
        public int? IsManual { get; set; }
    }
}
