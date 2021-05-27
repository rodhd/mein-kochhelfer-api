using System.ComponentModel.DataAnnotations;

namespace Domain.Authors
{
    public class Author
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
    }
}