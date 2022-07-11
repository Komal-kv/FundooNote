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
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<LabelResponseModel>> GetAllLabelsByLinqJoins(int UserId)
        {
            try
            {
                var label = fundooContext.Labels.FirstOrDefault(c => c.UserId == UserId);
                if (label == null)
                {
                    return null;
                }

                var res = await(from User in fundooContext.Users
                                join notes in fundooContext.Notes on User.UserId equals UserId
                                join labels in fundooContext.Labels on notes.NoteId equals labels.NoteId
                                where labels.UserId == UserId


                                select new LabelResponseModel
                                {
                                    UserId = UserId,
                                    NoteId = notes.NoteId,
                                    Title = notes.Title,
                                    FirstName = User.FirstName,
                                    LastName = User.LastName,
                                    Email = User.Email,
                                    Description = notes.Description,
                                    LabelName = labels.LabelName,
                                }).ToListAsync();
                return res;
            }
            catch(Exception ex)
            {

                throw ex;
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
