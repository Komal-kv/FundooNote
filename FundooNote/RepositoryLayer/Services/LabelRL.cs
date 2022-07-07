﻿using Microsoft.EntityFrameworkCore;
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
    }
}
