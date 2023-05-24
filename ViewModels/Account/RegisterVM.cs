using System.ComponentModel.DataAnnotations;


namespace StudioTemplate.ViewModels.Account
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(25)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Surname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password),Compare(nameof(Password))]
        [MinLength(8)]
        public string ConfirmPasword { get; set; }

    }
}
