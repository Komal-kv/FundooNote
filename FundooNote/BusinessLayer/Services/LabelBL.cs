using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }
        public async Task AddLabel(int UserId, int NoteId, string Labelname)
        {
            try
            {
                await labelRL.AddLabel(UserId, NoteId, Labelname);  
            }
            catch(Exception e)
            {
                throw e; 
            }
        }

        public async Task<string> DeleteLabel(int UserId, int NoteId)
        {
            try
            {
                return await labelRL.DeleteLabel(UserId, NoteId);
            }
            catch(Exception e)
            {

                throw e;
            }
        }

        public async Task<IEnumerable<Label>> GetAllLabels(int UserId)
        {
            try
            {
                return await labelRL.GetAllLabels(UserId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Label>> GetLabelByNoteId(int UserId, int NoteId)
        {
            try
            {
                return await labelRL.GetLabelByNoteId(UserId, NoteId);
            }
            catch(Exception e)
            {

                throw e;
            }
        }

        public async Task<string> UpdateLabel(int UserId, int NoteId, string Labelname)
        {
            try
            {
                return await labelRL.UpdateLabel(UserId, NoteId, Labelname);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
