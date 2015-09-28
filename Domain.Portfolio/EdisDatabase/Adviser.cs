using System;
using System.ComponentModel.DataAnnotations;

namespace EdisDatabase
{
    public class Adviser
    {
        [Key]
        public string AdviserId { get; set; }

        [Required]
        public DateTime? CreatedOn { get; set; }

        [Required]
        public string AdviserNumber { get; set; }
    }
}