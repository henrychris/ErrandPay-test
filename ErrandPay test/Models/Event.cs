using System.ComponentModel.DataAnnotations;

namespace ErrandPay_test.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public bool IsAttending { get; set; } = false;
    }
}
