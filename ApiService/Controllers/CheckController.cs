using ApiService.DTO;
using ApiService.Mappers;
using ApiService.ValidationResultFactory;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Controllers;

[Route("[controller]")]
[ApiController]
public class CheckController(UtilitiesDbContext dbContext, CheckCalculationService calculationService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<CheckViewDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int limit = 25, int offset = 0)
    {
        var checks = await dbContext.GetChecksWithDetails(false).Skip(offset).Take(limit).ToListAsync();
        return new OkObjectResult(checks.Select(x => x.ToViewDto()));
    }

    [HttpGet("{id}")]
    [ProducesResponseType<CheckViewDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id, bool withDetails = true)
    {
        var check = await dbContext.GetChecksWithDetails(withDetails).SingleOrDefaultAsync(h => h.Id == id);
        if (check is null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(check.ToViewDto());
    }

    [HttpGet("{id}/edit")]
    [ProducesResponseType<CheckEditDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetForEdit(Guid id)
    {
        var check = await dbContext.GetChecksWithDetails(true).SingleOrDefaultAsync(h => h.Id == id);
        if (check is null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(check.ToEditDto());
    }

    [HttpGet("previous")]
    [ProducesResponseType<CheckViewDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPrevious(Guid homeId, DateTime date)
    {
        var check = await calculationService.GetPreviousCheck(homeId, date);
        if (check is null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(check.ToViewDto());
    }

    [HttpPost]
    [ProducesResponseType<CheckViewDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType<ValidationResult>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(CheckEditDto check)
    {
        var existing = await dbContext.Checks.FindAsync(check.Id);
        if (existing != null)
        {
            return new ConflictResult();
        }

        var entity = check.ToEntity();
        await calculationService.Calculate(entity);

        dbContext.Checks.Add(entity);
        await dbContext.SaveChangesAsync();
        return new CreatedResult($"/{entity.Id}", entity.ToViewDto());
    }

    [HttpPost("calculate")]
    [ProducesResponseType<decimal>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationResult>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostCalculateAction(CheckEditDto check)
    {
        var entity = check.ToEntity();
        var result = await calculationService.Calculate(entity);
        return new OkObjectResult(result);
    }

    [HttpPost("calculate/record")]
    [ProducesResponseType<RecordEditDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationResult>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostCalculateAction(RecordEditDto record, bool isZero)
    {
        var entity = record.ToEntity();
        await calculationService.Calculate(entity, isZero);
        return new OkObjectResult(entity.ToEditDto());
    }

    [HttpPost("empty")]
    [ProducesResponseType<CheckEditDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostCreateEmptyAction([BindRequired] Guid houseId, [BindRequired] DateTime date)
    {
        var exist = dbContext.Homes.Any(x => x.Id == houseId);
        if (!exist)
        {
            return BadRequest();
        }

        var created = await calculationService.CreateEmpty(houseId, date);
        return new OkObjectResult(created.ToEditDto());
    }

    [HttpPost("refill")]
    [ProducesResponseType<CheckEditDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationResult>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostRefillAction(CheckEditDto check)
    {
        var entity = check.ToEntity();
        await calculationService.Refill(entity);
        return new OkObjectResult(entity.ToEditDto());
    }

    [HttpPut("{id}")]
    [ProducesResponseType<CheckViewDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<CheckViewDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ValidationResult>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(Guid id, CheckEditDto check)
    {
        if (id != check.Id)
        {
            return new BadRequestResult();
        }

        var entity = check.ToEntity();
        await calculationService.Calculate(entity);

        foreach (var item in entity.Records)
        {
            item.Tariff = null;
        }

        var existing = await dbContext.Checks.FindAsync(id);
        if (existing == null)
        {
            dbContext.Checks.Add(entity);
            await dbContext.SaveChangesAsync();
            return new CreatedResult($"/{entity.Id}", entity);
        }

        await dbContext.Entry(existing).Collection(x => x.Records).LoadAsync();
        dbContext.Entry(existing).CurrentValues.SetValues(entity);
        foreach (var item in existing.Records)
        {
            dbContext.Entry(item).CurrentValues.SetValues(entity.Records.First(x => x.Id == item.Id));
        }

        await dbContext.SaveChangesAsync();
        return new OkObjectResult(existing.ToViewDto());
    }

    [HttpPatch("{id}")]
    [Consumes("application/json-patch+json")]
    [ProducesResponseType<CheckViewDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ValidationResult>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<CheckEditDto> homePatch)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var home = await dbContext.Checks.FindAsync(id);
        if (home is null)
        {
            return new NotFoundResult();
        }

        dbContext.Remove(home);
        await dbContext.SaveChangesAsync();
        return new OkResult();
    }
}
