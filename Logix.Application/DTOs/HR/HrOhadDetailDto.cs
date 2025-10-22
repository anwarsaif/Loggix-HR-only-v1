using Castle.MicroKernel.SubSystems.Conversion;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Logix.Application.DTOs.HR
{
    public class HrOhadDetailDto
    {
        public long? OhadDetId { get; set; }
        public long? OhdaId { get; set; }
        public long? ItemId { get; set; }
        public string? OhdaDate { get; set; }
        public string? ItemName { get; set; }
        public string? ItemState { get; set; }
        public string? ItemDetails { get; set; }
        public string? Note { get; set; }

        public decimal? QtyIn { get; set; }
        public decimal? QtyOut { get; set; }
        public decimal? OtyWantsDroping { get; set; }

        public int? ItemStateId { get; set; }
        public long? OrgnalId { get; set; }
        public bool? IsDeleted { get; set; }
		public decimal? ItemQtyIn { get; set; }
		public decimal? ItemQtyOut { get; set; }
		public decimal? RemainingQuantity { get; set; }
	}  
    
    public class HrOhadDetailEditDto
    {
        public long OhadDetId { get; set; }
        public long? OhdaId { get; set; }
        public long? ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? ItemState { get; set; }
        public string? ItemDetails { get; set; }
        public string? Note { get; set; }
        public decimal? QtyIn { get; set; }
        public decimal? QtyOut { get; set; }
        public decimal? OtyWantsDroping { get; set; }
        public int? ItemStateId { get; set; }
        public long? OrgnalId { get; set; }
    }  

}
