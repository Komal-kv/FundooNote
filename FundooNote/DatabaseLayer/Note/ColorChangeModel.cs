using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseLayer.Note
{
    public class ColorChangeModel
    {
        [Required]
        public string Colour { get; set; }
    }
}
