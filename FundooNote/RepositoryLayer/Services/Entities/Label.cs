using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace RepositoryLayer.Services.Entities
{
    [Keyless]
    public class Label
    {
        public string LabelName { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        [ForeignKey("Notes")]
        public int NoteId { get; set; }     
    }
}
