using System.ComponentModel.DataAnnotations;

namespace Api.BLL.Entity
{
    public class Coffee
    {
        [Key]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long Price { get; set; }
    }
}
