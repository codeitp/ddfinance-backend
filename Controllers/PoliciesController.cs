using InsurancePolicyAPI.Data;
using InsurancePolicyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PoliciesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/policies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Policy>>> GetPolicies()
        {
            try
            {
                var policies = await _context.Policies.ToListAsync();
                return Ok(policies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET: api/policies/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Policy>> GetPolicy(int id)
        {
            var policy = await _context.Policies.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }

            return policy;
        }

        // POST: api/policies
        [HttpPost]
        public async Task<ActionResult<Policy>> CreatePolicy([FromBody] Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Policies.Add(policy);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPolicy), new { id = policy.Id }, policy);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating policy", error = ex.Message });
            }
        }

        // PUT: api/policies/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePolicy(int id, Policy policy)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid policy ID" });
            }

            // Fetch the existing policy by ID
            var existingPolicy = await _context.Policies.FindAsync(id);
            if (existingPolicy == null)
            {
                return NotFound(new { message = "Policy not found" });
            }

            // Update the properties of the existing policy
            existingPolicy.PolicyName = policy.PolicyName;
            existingPolicy.Premium = policy.Premium;
            existingPolicy.CoverageAmount = policy.CoverageAmount;
            existingPolicy.PolicyHolderName = policy.PolicyHolderName;
            existingPolicy.EffectiveDate = policy.EffectiveDate;

            // Save changes
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the policy", details = ex.Message });
            }

            return NoContent(); // Return 204 No Content on success
        }

        // DELETE: api/policies/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicy(int id)
        {
            var policy = await _context.Policies.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }

            _context.Policies.Remove(policy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PolicyExists(int id)
        {
            return _context.Policies.Any(e => e.Id == id);
        }
    }
}
