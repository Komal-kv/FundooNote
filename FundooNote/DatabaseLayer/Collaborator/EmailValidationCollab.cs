using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseLayer.Collaborator
{
    public class EmailValidationCollab
    {
        [Required]
        //[RegularExpression("^[0-9a-zA-Z]+([+#%&_.-][a-zA-Z0-9]+)*[@][0-9]?[a-zA-Z]{2,}[.][a-zA-Z]{2,3}([.][a-zA-Z]{2,3})?$", ErrorMessage = "Please Enter Your Correct Email ")]
        public string CollaboratorEmail { get; set; }
    }
}
