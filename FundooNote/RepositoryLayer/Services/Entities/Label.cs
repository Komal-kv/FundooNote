using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Services.Entities
{
    [Keyless]
    public class Label
    {
        public string LabelName { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Notes")]
        public int NoteId { get; set; }
        public virtual Note Note { get; set; }
    }
}
