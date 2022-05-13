using System;
using System.ComponentModel.DataAnnotations;

namespace Auros.WebUI.Models.Entities
{
    public class Subscribe
    {
        public int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public DateTime? EmailConfirmedDate { get; set; }
    }
}