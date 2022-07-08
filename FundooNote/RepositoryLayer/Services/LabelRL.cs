using DatabaseLayer.Label;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        FundooContext fundooContext; //context class is used to query or save data to the database.
        IConfiguration configuration;
        public LabelRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
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

        public async Task DeleteLabel(int UserId, int NoteId)
        {
            try
            {
                var label = fundooContext.Labels.Where(X => X.UserId == UserId && X.NoteId == NoteId).FirstOrDefault();
                if (label != null)
                {
                    fundooContext.Labels.Remove(label);
                    await fundooContext.SaveChangesAsync();                   
                }
            }
            catch(Exception e)
            {

                throw e;
            }
        }

        public async Task<List<Label>> GetAllLabels(int UserId)
        {
            try
            {
                var label = fundooContext.Labels.FirstOrDefault(u => u.UserId == UserId);
                if (label == null)
                {
                    return null;
                }
                return await fundooContext.Labels.ToListAsync();

                // Get All Label By Linq Join query

                //return await fundooContext.Labels
                //    .Where(l => l.UserId == UserId)
                //    .Join(fundooContext.Notes
                //    .Where(n => n.NoteId == label.NoteId),
                //    l => l.NoteId,
                //    n => n.NoteId,
                //    (l, n) => new LabelResponseModel
                //    {
                //        UserId = l.UserId,
                //        NoteId = n.NoteId,
                //        Title = n.Title,
                //        Description = n.Description,
                //        LabelName = l.LabelName,

                //    }).ToListAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Label> GetLabelByNoteId(int UserId, int NoteId)
        {
            try
            {
                return await fundooContext.Labels.FirstOrDefaultAsync(u => u.UserId == UserId && u.NoteId == NoteId);
            }
            catch(Exception e)
            {

                throw e;
            }
        }

        public async Task UpdateLabel(int UserId, int NoteId, string LabelName)
        {
            try
            {
                var update = fundooContext.Labels.Where(X => X.UserId == UserId && X.NoteId == NoteId).FirstOrDefault();
                if (update != null)
                {
                    update.LabelName = LabelName;
                    await fundooContext.SaveChangesAsync();                  
                }              
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
