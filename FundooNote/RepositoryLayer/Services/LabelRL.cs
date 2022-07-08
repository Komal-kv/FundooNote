using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        public readonly FundooContext fundooContext; //context class is used to query or save data to the database.
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public async Task AddLabel(int UserId, int NoteId, string Labelname)
        {
            try
            {
                var label = fundooContext.Labels.Where(c => c.UserId == UserId && c.NoteId == NoteId).FirstOrDefaultAsync();
                if (label != null)
                {
                    Label labels = new Label(); 

                    labels.UserId = UserId;
                    labels.NoteId = NoteId;
                    labels.LabelName = Labelname;

                    await fundooContext.Labels.AddAsync(labels);
                    await fundooContext.SaveChangesAsync();
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> DeleteLabel(int UserId, int NoteId)
        {
            try
            {
                var deleteLabel = fundooContext.Labels.Where(X => X.UserId == UserId && X.NoteId == NoteId).FirstOrDefault();
                if (deleteLabel != null)
                {
                    fundooContext.Labels.Remove(deleteLabel);
                    await fundooContext.SaveChangesAsync();
                    return "Label Deleted Successfully";
                }
                else
                {
                    return null;
                }
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
                return await fundooContext.Labels.Where(x => x.UserId == UserId).ToListAsync();
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
                return await fundooContext.Labels.Where(u => u.UserId == UserId && u.NoteId == NoteId).ToListAsync();
            }
            catch(Exception e)
            {

                throw e;
            }
        }

        public async Task<string> UpdateLabel(int UserId, int NoteId, string LabelName)
        {
            try
            {
                var update = fundooContext.Labels.Where(X => X.UserId == UserId && X.NoteId == NoteId).FirstOrDefault();
                if (update != null)
                {
                    update.LabelName = LabelName;
                    update.NoteId = NoteId;
                    await fundooContext.SaveChangesAsync();
                    return "Label is modified";
                }
                else
                {
                    return "Label is not modified";
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
