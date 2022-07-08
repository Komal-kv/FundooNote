using DatabaseLayer.Label;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
        Task AddLabel(int UserId, int NoteId, string Labelname);
        Task UpdateLabel(int UserId, int NoteId, string LabelName);
        Task DeleteLabel(int UserId, int NoteId);
        Task<Label> GetLabelByNoteId(int UserId, int NoteId);
        Task<List<Label>> GetAllLabels(int UserId);
    }
}
