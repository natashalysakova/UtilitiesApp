using ApiService.DTO;
using ApiService.Mappers;
using ApiService.ValidationResultFactory;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Controllers;

[Route("[controller]")]
[ApiController]
public class TariffsController(UtilitiesDbContext dbContext) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<TariffViewDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(Guid? houseId = null, TariffType? tariffType = null)
    {
        var tariffs = dbContext.Tariffs.Include(x => x.Home).Include(x => x.Limits).Include(x => x.UtilityGroup).AsQueryable();
        if (houseId != null)
        {
            tariffs = tariffs.Where(x => x.HomeId == houseId);
        }

        if (tariffType != null)
        {
            tariffs = tariffs.Where(x => x.TariffType == tariffType);
        }

        var result = await tariffs.ToListAsync();

        return new OkObjectResult(result.Select(x => x.ToViewDto()));
    }

    [HttpGet("{id}")]
    [ProducesResponseType<TariffViewDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var tariff = await dbContext.Tariffs.FindAsync(id);

        if (tariff is null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(tariff);
    }

    [HttpGet("{id}/edit")]
    [ProducesResponseType<TariffEditDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetForEdit(Guid id)
    {
        var tariff = await dbContext.Tariffs.Include(x => x.Limits).Include(x => x.UtilityGroup).SingleOrDefaultAsync(h => h.Id == id);
        if (tariff is null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(tariff.ToEditDto());
    }

    [HttpPost]
    [ProducesResponseType<TariffViewDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<TariffViewDto>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<ValidationResult>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(TariffEditDto tariff)
    {
        if (await dbContext.Tariffs.FindAsync(tariff.Id) != null)
        {
            return new ConflictObjectResult(tariff);
        }

        var entity = tariff.ToEntity();
        dbContext.Tariffs.Add(entity);
        await dbContext.SaveChangesAsync();
        return new CreatedResult($"/tariffs/{tariff.Id}", tariff);
    }

    [HttpPut("{id}")]
    [ProducesResponseType<TariffViewDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<TariffViewDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ValidationResult>(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Put(Guid id, TariffEditDto tariff)
    {
        if (id != tariff.Id)
        {
            return new BadRequestResult();
        }

        var existing = await dbContext.Tariffs.FindAsync(id);
        if (existing == null)
        {
            var entity = tariff.ToEntity();
            dbContext.Tariffs.Add(entity);
            await dbContext.SaveChangesAsync();
            return new CreatedResult($"/tariffs/{entity.Id}", entity);
        }

        dbContext.Entry(existing).CurrentValues.SetValues(tariff);
        await dbContext.SaveChangesAsync();
        return new OkObjectResult(existing);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<TariffViewDto>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var tariff = await dbContext.Tariffs.FindAsync(id);
        if (tariff is null)
        {
            return new NotFoundResult();
        }

        tariff.IsArchived = true;
        await dbContext.SaveChangesAsync();
        return new OkResult();
    }
}
