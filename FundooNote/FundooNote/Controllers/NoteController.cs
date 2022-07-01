﻿using BusinessLayer.Interfaces;
using DatabaseLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        INoteBL noteBL;
        FundooContext fundooContext;

        public NoteController(INoteBL noteBL, FundooContext fundooContext)
        {
            this.noteBL = noteBL;
            this.fundooContext = fundooContext;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddNote(NotePostModel notePostModel)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                await this.noteBL.AddNote(userId, notePostModel);
                return this.Ok(new { success = true, message = "Note Added Sucessfully" });
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAllNote()
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

                var note = fundooContext.Notes.FirstOrDefault(u => u.UserId == userId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Your Note doesn't exist" });
                }
                List<Note> list = new List<Note>();
                list = await this.noteBL.GetAllNote(userId);

                return this.Ok(new {sucsess = true, message = "Getting your all note successfully", data = list});

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpDelete("{NoteId}")]
        public async Task<ActionResult> DeleteNote(int NoteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

                var note = fundooContext.Notes.FirstOrDefault(u => u.UserId == userId && u.NoteId == NoteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry..!,Your Note doesn't exist create one" });
                }
                await this.noteBL.DeleteNote(userId, NoteId);

                return this.Ok(new { success = true, message = $"Note Deleted Successfully for the note, {note.Title} " });
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
