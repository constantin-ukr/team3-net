using Comments.Core;
using Comments.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Comment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        CommentsDbContext db;
        public CommentController(CommentsDbContext context)
        {
            db = context;
            if (!db.Comment.Any())
            {
                db.Comment.Add(new CommentEntity { FieldText = "First Comment" });
                db.Comment.Add(new CommentEntity { FieldText = "Second Comment" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentEntity>>> Get()
        {
            return await db.Comment.ToListAsync();
        }

        // GET api/CommentEntities/
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentEntity>> Get(int id)
        {
            CommentEntity comment = await db.Comment.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
                return NotFound();
            return new ObjectResult(comment);
        }

        // POST api/CommentEntities
        [HttpPost]
        public async Task<ActionResult<CommentEntity>> Post(CommentEntity commentEntity)
        {
            if (commentEntity == null)
            {
                return BadRequest();
            }
            db.Comment.Add(commentEntity);
            await db.SaveChangesAsync();
            return Ok(commentEntity);
        }

        // PUT api/CommentEntities/
        [HttpPut]
        public async Task<ActionResult<CommentEntity>> Put(CommentEntity commentEntity)
        {
            if (commentEntity == null)
            {
                return BadRequest();
            }
            if (!db.Comment.Any(x => x.Id == commentEntity.Id))
            {
                return NotFound();
            }
            db.Update(commentEntity);
            await db.SaveChangesAsync();
            return Ok(commentEntity);
        }

        // DELETE api/CommentEntities/
        [HttpDelete("{id}")]
        public async Task<ActionResult<CommentEntity>> Delete(int id)
        {
            CommentEntity commentEntity = db.Comment.FirstOrDefault(x => x.Id == id);
            if (commentEntity == null)
            {
                return NotFound();
            }
            db.Comment.Remove(commentEntity);
            await db.SaveChangesAsync();
            return Ok(commentEntity);
        }
    }

}
