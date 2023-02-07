using System.ComponentModel.DataAnnotations;

namespace User.Models
{
    public class UserObj
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        public decimal Wallet { get; set; } = 0;
        // add a wallet with a reducing balance

    }
}
