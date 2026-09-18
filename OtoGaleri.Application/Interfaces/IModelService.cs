using OtoGaleri.Application.DTOs.ModelDtos;

namespace OtoGaleri.Application.Interfaces;

public interface IModelService
{
    Task<IEnumerable<ModelDto>> GetAllModelsAsync();
    Task<ModelDto?> GetModelByIdAsync(int id);
    Task AddModelAsync(CreateModelDto modelDto);
    Task UpdateModelAsync(UpdateModelDto modelDto);
    Task DeleteModelAsync(int id);
}