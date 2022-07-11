using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace RepositoryLayer.Services.Entities
{
    [Keyless]
    public class Label
    {
        public string LabelName { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Note")]
        public int NoteId { get; set; }     
    }
}
