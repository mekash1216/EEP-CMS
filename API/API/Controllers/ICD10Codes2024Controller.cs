using API.Data;
using API.Model;
using API.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ICD10Codes2024Controller : ControllerBase
    {
        private readonly ApiContext _context;

        public ICD10Codes2024Controller(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ICD10CodeDto>>> GetDataItems(
            string searchTerm = null,
            string sortOrder = "asc",
            int pageNumber = 1,
            int pageSize = 8000)
        {
            if (pageSize <= 0 || pageSize > 8000)
            {
                return BadRequest("Page size must be between 1 and 8000.");
            }

            var query = _context.ICD10Codes2024.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item =>
                    item.CODE.Contains(searchTerm) ||
                    item.ShortDescription.Contains(searchTerm) 
                   );
            }

            query = sortOrder.ToLower() == "desc" ?
                query.OrderByDescending(item => item.ShortDescription) :
                query.OrderBy(item => item.ShortDescription);

            var totalItems = await query.CountAsync();
            var dataItems = await query
                .Select(item => new ICD10CodeDto { CODE = item.CODE, ShortDescription = item.ShortDescription })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            Response.Headers.Add("X-Total-Count", totalItems.ToString());

            return Ok(dataItems);
        }
    }
}
