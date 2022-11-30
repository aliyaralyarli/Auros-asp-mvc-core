using System;

namespace Auros.WebUI.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string BrandName { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public byte[] ImageData { get; set; }
    }
}