using BusinessLayer.Interfaces;
using DatabaseLayer.Collaborator;
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
    public class CollabController : ControllerBase
    {
        FundooContext fundooContext;
        ICollaboratorBL collabBL;
        public CollabController(FundooContext fundooContext, ICollaboratorBL collabBL)
        {
            this.fundooContext = fundooContext;
            this.collabBL = collabBL;
        }

        [Authorize]
        [HttpPost("AddCollaborator/{NoteId}")]
        public async Task<ActionResult> AddCollab(int NoteId, EmailValidationCollab emailValidationCollab)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userid = Convert.ToInt32(currentUser.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var collab = fundooContext.Collabs.Where(x => x.UserId == userid && x.NoteId == NoteId).FirstOrDefault();
                if (collab != null)
                {
                    return this.BadRequest(new { status = 301, isSuccess = false, Message = "Enter Distinct NoteId" });
                }
                await this.collabBL.AddCollab(userid, NoteId, emailValidationCollab);

                return this.Ok(new { status = 200, isSuccess = true, Message = "Collaborator added successfully!" });
            }
            catch(Exception e)
            {

                throw e;
            }
        }

        [Authorize]
        [HttpDelete("DeleteCollab/{NoteId}")]
        public async Task<ActionResult> DeleteCollab(int NoteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userid = Convert.ToInt32(currentUser.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var collab = fundooContext.Collabs.Where(x => x.UserId == userid && x.NoteId == NoteId).FirstOrDefault();
                if (collab == null)
                {
                    return this.BadRequest(new { status = 301, isSuccess = false, Message = "Enter Distinct NoteId" });
                }
                await this.collabBL.DeleteCollab(userid, NoteId);

                return this.Ok(new { status = 200, isSuccess = true, message = "Collaborator removed successfully" });
            }
            catch(Exception e)
            {

                throw e;
            }
        }

        [Authorize]
        [HttpGet("GetAllCollaborator")]
        public async Task<ActionResult> GetAllCollab()
        {
            try
            {
                var currentUser = HttpContext.User;
                int userid = Convert.ToInt32(currentUser.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var collab = fundooContext.Collabs.Where(x => x.UserId == userid).FirstOrDefault();
                if (collab == null)
                {
                    this.BadRequest(new { success = false, Message = "Collab doesn't exist" });
                }
                List<Collaborator> collabr = new List<Collaborator>();
                collabr = await this.collabBL.GetAllCollab(userid);
                return this.Ok(new { success = true, message = " List of all Collaborators :", data = collabr });
            }
            catch(Exception e)
            {

                throw e;
            }
        }
    }
}
