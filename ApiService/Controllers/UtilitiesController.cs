using ApiService.DTO;
using ApiService.Mappers;
using ApiService.ValidationResultFactory;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Controllers;

[Route("[controller]")]
[ApiController]
public class UtilitiesController(UtilitiesDbContext dbContext) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<UtilityGroupViewModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var result = await dbContext.UtilityGroups.OrderBy(x => x.Priority).ToListAsync();
        return new OkObjectResult(result.Select(x => x.ToViewDto()));
    }

    [HttpGet("{id}")]
    [ProducesResponseType<UtilityGroupViewModel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var utility = await dbContext.UtilityGroups.FindAsync(id);

        if (utility is null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(utility);
    }

    [HttpGet("{id}/edit")]
    [ProducesResponseType<UtilityGroupEditModel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetForEdit(Guid id)
    {
        var utility = await dbContext.UtilityGroups.SingleOrDefaultAsync(h => h.Id == id);
        if (utility is null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(utility.ToEditDto());
    }

    [HttpPost]
    [ProducesResponseType<UtilityGroupViewModel>(StatusCodes.Status201Created)]
    [ProducesResponseType<UtilityGroupViewModel>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<ValidationResult>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(UtilityGroupEditModel utility)
    {
        if (await dbContext.UtilityGroups.FindAsync(utility.Id) != null)
        {
            return new ConflictObjectResult(utility);
        }

        return await AddUtility(dbContext, utility);
    }

    [HttpPut("{id}")]
    [ProducesResponseType<UtilityGroupViewModel>(StatusCodes.Status200OK)]
    [ProducesResponseType<UtilityGroupViewModel>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ValidationResult>(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Put(Guid id, UtilityGroupEditModel utility)
    {
        if (id != utility.Id)
        {
            return new BadRequestResult();
        }

        var existing = await dbContext.UtilityGroups.FindAsync(id);
        if (existing == null)
        {
            return await AddUtility(dbContext, utility);
        }

        dbContext.Entry(existing).CurrentValues.SetValues(utility);
        await dbContext.SaveChangesAsync();
        return new OkObjectResult(existing);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<UtilityGroupViewModel>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var utility = await dbContext.UtilityGroups.FindAsync(id);
        if (utility is null)
        {
            return new NotFoundResult();
        }

        utility.IsArchived = true;

        var itemsToChange = dbContext.UtilityGroups.Where(x => x.Priority > utility.Priority);
        foreach (var item in itemsToChange)
        {
            item.Priority -= 1;
        }

        await dbContext.SaveChangesAsync();
        return new OkResult();
    }

    private static async Task<IActionResult> AddUtility(UtilitiesDbContext dbContext, UtilityGroupEditModel utility)
    {
        var entity = utility.ToEntity();
        entity.Priority = dbContext.UtilityGroups.Max(x => x.Priority) + 1;

        dbContext.UtilityGroups.Add(entity);
        await dbContext.SaveChangesAsync();
        return new CreatedResult($"/utilities/{utility.Id}", utility);
    }
}
