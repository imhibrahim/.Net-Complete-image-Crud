using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class Student_data
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public string? Standard { get; set; }
        public string? Email { get; set; }
        public string? pic { get; set; }

    }
}
