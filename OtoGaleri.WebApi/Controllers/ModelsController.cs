using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtoGaleri.Application.DTOs.ModelDtos;
using OtoGaleri.Application.Interfaces;

namespace OtoGaleri.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ModelsController : ControllerBase
{
    private readonly IModelService _modelService;

    public ModelsController(IModelService modelService)
    {
        _modelService = modelService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var models = await _modelService.GetAllModelsAsync();
        return Ok(models);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var model = await _modelService.GetModelByIdAsync(id);
        if (model == null)
            return NotFound("Model bulunamadı.");

        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateModelDto createModelDto)
    {
        await _modelService.AddModelAsync(createModelDto);
        return Ok("Model başarıyla eklendi.");
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateModelDto updateModelDto)
    {
        await _modelService.UpdateModelAsync(updateModelDto);
        return Ok("Model başarıyla güncellendi.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _modelService.DeleteModelAsync(id);
        return Ok("Model başarıyla silindi.");
    }
}