using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Models;

namespace WebAPI.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class ForumController : ControllerBase
    {
        private readonly IForumLogic ForumLogic;

        public ForumController(IForumLogic forumLogic)
        {
            ForumLogic = forumLogic;
        }

        [HttpPost]
        public async Task<ActionResult<Forum>> CreateAsync([FromBody] ForumCreationDTO dto)
        {
            try
            {
                Forum created = await ForumLogic.CreateAsync(dto);
                return Created($"/forum/{created.Id}", created);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Forum>>> GetAsync([FromQuery] string? userName, [FromQuery] int? UserId,
            [FromQuery] string? titleContains)
        {
            try
            {
                SearchArticleDto searchForumDto = new(userName, UserId, titleContains);
                var forums = await ForumLogic.GetAsync(searchForumDto);
                return Ok(forums);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPatch]
        public async Task<ActionResult> UpdateAsync([FromBody] ForumUpdateDto dto)
        {
            try
            {
                await ForumLogic.UpdateAsync(dto);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                await ForumLogic.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
