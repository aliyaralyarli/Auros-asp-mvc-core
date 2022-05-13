using System;
using System.ComponentModel.DataAnnotations;

namespace Auros.WebUI.Models.Entities.FormModels
{
    public class SignInFormModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu xana bos qoyula bilmez!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Bu xana bos qoyula bilmez!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}