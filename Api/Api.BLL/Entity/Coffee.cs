using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Api.BLL.Entity
{
    public class Coffee
    {
        [Key]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
