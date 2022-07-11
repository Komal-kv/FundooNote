using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Services.Entities
{
    [Keyless]
    public class Collaborator
    {
        public string CollaboratorEmail { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Note")]
        public int NoteId { get; set; }
    }
}
