using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Badminton.Contract.DTO.Field
{
    public class FieldUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Code { get; set; }
        [Required]
        public required string Description { get; set; }
    }
}
