using BusinessLayer.Interfaces;
using DatabaseLayer.Label;
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

                var label = fundooContext.Labels.FirstOrDefault(u => u.UserId == userId);
                if (label == null)
                {
                    this.BadRequest(new { success = false, Message = "Label doesn't exist" });
                }

                List<Label> labelList = new List<Label>();
                labelList = await this.labelBL.GetAllLabels(userId);
                return Ok(new { success = true, Message = $"Note Obtained successfully ", data = labelList });
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
                if (labels == null)
                {
                    return this.BadRequest(new { Success = false, message = "label not Found" });
                }
                var label1 = await this.labelBL.GetLabelByNoteId(userId, NoteId);
                return this.Ok(new { Success = true, message = "  label found Successfully", data = label1 });

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
                    return this.Ok(new { Success = true, message = "Label Updated Successfully", data = result });
                }
                await this.labelBL.UpdateLabel(userId, NoteId, LabelName);
                return this.BadRequest(new { Success = false, message = "No Label Found" });
            }
            catch(Exception e)
            {

                throw e;
            }
        }

        [Authorize]
        [HttpDelete("DeleteLabel/{NoteId}")]
        public async Task<IActionResult> DeleteLabel(int NoteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

                var delete = await labelBL.DeleteLabel(userId, NoteId);
                if (delete != null)
                {
                    return this.Ok(new {Success = true , message = "Label Deleted Successfully" });
                }
                await this.labelBL.DeleteLabel(userId, NoteId);
                return this.BadRequest(new { Success = false, message = "Label not found" });
            }
            catch(Exception e)
            {

                throw e;
            }
        }

    }
}
