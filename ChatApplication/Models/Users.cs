using System.ComponentModel.DataAnnotations;

namespace ChatApplication.Models
{
    public class Users

    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Phone]
        public string ContactNumber { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }
    }
}