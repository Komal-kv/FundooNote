using BusinessLayer.Interfaces;
using DatabaseLayer.Collaborator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
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
        [HttpPost("AddCollab/{NoteId}")]
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
    }
}
