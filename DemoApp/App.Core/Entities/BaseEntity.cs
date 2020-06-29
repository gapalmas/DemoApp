using System;
using System.ComponentModel.DataAnnotations;

namespace App.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool Status { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Register Date")]
        public DateTime Date { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime DateUpdate { get; set; }
    }
}