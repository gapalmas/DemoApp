using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities
{
    public class Product : BaseEntity
    {
        /*Validaciones con DataAnnotations*/
        /*Reference: https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/data-annotations*/
        //public int Id { get; set; }
        //[MaxLength(100, ErrorMessage = "Este campo {0} solo contiene {1} como longitud maxima.")]
        //[Required]
        public string Name { get; set; }
        /*Formato Currency 2*/
        /*Reference: https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings */
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        //[Display(Name = "Is Availabe?")]
        //public bool Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double StockMin { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double StockMax { get; set; }

        public User User { get; set; }
    }
}
