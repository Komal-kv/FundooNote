using BusinessLayer.Interfaces;
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
    public class LabelController : ControllerBase
    {
        FundooContext fundooContext;
        ILabelBL labelBL;

        public LabelController(FundooContext fundooContext, ILabelBL labelBL)
        {
            this.fundooContext = fundooContext;
            this.labelBL = labelBL;

        }
        [Authorize]
        [HttpPost("AddLabel/{NoteId}/{LabelName}")]
        public async Task<ActionResult> AddLabel(int NoteId, string LabelName)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userid = Convert.ToInt32(currentUser.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var Label = fundooContext.Labels.Where(x => x.UserId == userid && x.NoteId == NoteId).FirstOrDefault();

                if (Label != null)
                {
                    return this.BadRequest(new { status = 301, isSuccess = false, Message = "Enter Distinct NoteId" });

                }
                await this.labelBL.AddLabel(userid, NoteId, LabelName);

                return this.Ok(new { status = 200, isSuccess = true, Message = "Label created successfully!" });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetAllLabels")]
        public async Task<IActionResult> GetAllLabels()
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

                var labels = await labelBL.GetAllLabels(userId);
                if (labels != null)
                {
                    return this.Ok(new { status = 200, isSuccess = true, Message = "lables are ready", data = labels });
                }
                else
                {
                    return this.NotFound(new { isSuccess = false, Message = "No label found" });
                }
            }
            catch(Exception e)
            {

                throw e;
            }
        }

        [Authorize]
        [HttpGet("GetLabelbByNoteId/{NoteId}")]
        public async Task<IActionResult> GetLabelByNoteId(int NoteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

                var labels = await labelBL.GetLabelByNoteId(userId, NoteId);
                if (labels.Count != 0)
                {
                    return this.Ok(new { status = 200, isSuccess = true, message = "  label found Successfully", data = labels });
                }
                else
                {
                    return this.NotFound(new { isSuccess = false, message = "label not Found" });
                }
            }
            catch(Exception e)
            {

                throw e;
            }
        }

        [Authorize]
        [HttpPut("UpdateLabel/{NoteId}/{LabelName}")]
        public async Task<IActionResult> UpdateLabel(int NoteId, string LabelName)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

                var result = await labelBL.UpdateLabel(userId, NoteId, LabelName);
                if (result != null)
                {
                    return this.Ok(new { status = 200, isSuccess = true, message = "Label Updated Successfully", data = result });
                }
                else
                {
                    return this.NotFound(new { isSuccess = false, message = "No Label Found" });
                }
            }
            catch(Exception e)
            {

                throw e;
            }
        }

        [Authorize]
        [HttpDelete("DeleteNote/{NoteId}")]
        public async Task<IActionResult> DeleteLabel(int NoteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

                var delete = await labelBL.DeleteLabel(userId, NoteId);
                if (delete != null)
                {
                    return this.Ok(new {status = 200, isSuccess = true , message = "Label Deleted Successfully" });
                }
                else
                {
                    return this.NotFound(new { isSuccess = false, message = "Label not found" });
                }
            }
            catch(Exception e)
            {

                throw e;
            }
        }

    }
}
