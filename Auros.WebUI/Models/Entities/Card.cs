using System;
using System.ComponentModel.DataAnnotations;

namespace Auros.WebUI.Models.Entities
{
    public class Card
    {
        public int Id { get; set; }

        [Required]
        public int CardNumber { get; set; }

        [Required]
        public string ExpireDate { get; set; }

        public DateTime? DeleteTime { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}