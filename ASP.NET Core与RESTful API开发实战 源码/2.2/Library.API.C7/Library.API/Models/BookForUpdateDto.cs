using System.ComponentModel.DataAnnotations;

namespace Library.API.Models
{
    public class BookForUpdateDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "长度不能大于500")]
        public string Title { get; set; }
        public string Description { get; set; }
        public int Pages { get; set; }
    }
}
