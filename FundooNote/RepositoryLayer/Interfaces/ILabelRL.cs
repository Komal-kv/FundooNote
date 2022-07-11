using DatabaseLayer.Label;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        Task AddLabel(int UserId, int NoteId, string Labelname);
        Task UpdateLabel(int UserId, int NoteId, string LabelName);
        Task DeleteLabel(int UserId, int NoteId);
        Task<Label> GetLabelByNoteId(int UserId, int NoteId);
        Task<List<Label>> GetAllLabels(int UserId);
        Task<List<LabelResponseModel>> GetAllLabelsByLinqJoins(int UserId);
    }
}
