using ApiService.DTO;
using ApiService.Mappers;
using ApiService.ValidationResultFactory;
using Infrastructure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController(UtilitiesDbContext dbContext) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<HomeViewDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int limit = 25, int offset = 0)
    {
        var homes = await dbContext.GetHomesWithDetails(false).Skip(offset).Take(limit).ToListAsync();
        return new OkObjectResult(homes.Select(x => x.ToViewDto()));
    }

    [HttpGet("{id}")]
    [ProducesResponseType<HomeViewDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id, bool withDetails = true)
    {
        var home = await dbContext.GetHomesWithDetails(withDetails).SingleOrDefaultAsync(h => h.Id == id);
        if (home is null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(home.ToViewDto());
    }

    [HttpGet("{id}/edit")]
    [ProducesResponseType<HomeEditDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetForEdit(Guid id)
    {
        var home = await dbContext.GetHomesWithDetails(true).SingleOrDefaultAsync(h => h.Id == id);
        if (home is null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(home.ToEditDto());
    }

    [HttpGet("default")]
    [ProducesResponseType<HomeViewDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDefault(bool withDetails = true)
    {
        var home = await dbContext.GetHomesWithDetails(withDetails).SingleOrDefaultAsync(x => x.IsDefault);
        if (home is null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(home.ToViewDto());
    }

    [HttpPost]
    [ProducesResponseType<HomeViewDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType<ValidationResult>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(HomeEditDto home)
    {
        if (await dbContext.Homes.FindAsync(home.Id) != null)
        {
            return new ConflictResult();
        }

        dbContext.Homes.Add(home.ToEntity());
        await dbContext.SaveChangesAsync();
        return new CreatedResult($"/home/{home.Id}", home);
    }

    [HttpPost("default")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PostSetDefaultAction(Guid id)
    {
        var existing = await dbContext.Homes.FindAsync(id);
        if (existing == null)
        {
            return new NotFoundResult();
        }

        foreach (var house in dbContext.Homes.Where(x => x.Scope == existing.Scope))
        {
            house.IsDefault = false;
        }

        existing.IsDefault = true;
        await dbContext.SaveChangesAsync();
        return new OkResult();
    }

    [HttpPut("{id}")]
    [ProducesResponseType<HomeViewDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<HomeViewDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ValidationResult>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(Guid id, HomeEditDto home)
    {
        if (id != home.Id)
        {
            return new BadRequestResult();
        }

        var existing = await dbContext.Homes.FindAsync(id);
        if (existing == null)
        {
            var entity = home.ToEntity();
            dbContext.Homes.Add(entity);
            await dbContext.SaveChangesAsync();
            return new CreatedResult($"/home/{home.Id}", entity.ToViewDto());
        }

        dbContext.Entry(existing).CurrentValues.SetValues(home);
        await dbContext.SaveChangesAsync();
        return new OkObjectResult(existing.ToViewDto());
    }

    [HttpPatch("{id}")]
    [Consumes("application/json-patch+json")]
    [ProducesResponseType<HomeViewDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ValidationResult>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<HomeEditDto> homePatch)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var home = await dbContext.Homes.FindAsync(id);
        if (home is null)
        {
            return new NotFoundResult();
        }

        dbContext.Remove(home);
        await dbContext.SaveChangesAsync();
        return new OkResult();
    }
}