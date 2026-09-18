using AutoMapper;
using OtoGaleri.Application.DTOs.ModelDtos;
using OtoGaleri.Application.Interfaces;
using OtoGaleri.Core.Entities;
using OtoGaleri.Core.Interfaces;

namespace OtoGaleri.Application.Services;

public class ModelService : IModelService
{
    private readonly IGenericRepository<Model> _modelRepository;
    private readonly IMapper _mapper;

    public ModelService(IGenericRepository<Model> modelRepository, IMapper mapper)
    {
        _modelRepository = modelRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ModelDto>> GetAllModelsAsync()
    {
        var models = await _modelRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ModelDto>>(models);
    }

    public async Task<ModelDto?> GetModelByIdAsync(int id)
    {
        var model = await _modelRepository.GetByIdAsync(id);
        return model == null ? null : _mapper.Map<ModelDto>(model);
    }

    public async Task AddModelAsync(CreateModelDto modelDto)
    {
        var model = _mapper.Map<Model>(modelDto);
        await _modelRepository.AddAsync(model);
        await _modelRepository.SaveChangesAsync();
    }

    public async Task UpdateModelAsync(UpdateModelDto modelDto)
    {
        var model = await _modelRepository.GetByIdAsync(modelDto.Id);
        if (model != null)
        {
            _mapper.Map(modelDto, model);
            _modelRepository.Update(model);
            await _modelRepository.SaveChangesAsync();
        }
    }

    public async Task DeleteModelAsync(int id)
    {
        var model = await _modelRepository.GetByIdAsync(id);
        if (model != null)
        {
            model.IsDeleted = true;
            _modelRepository.Update(model);
            await _modelRepository.SaveChangesAsync();
        }
    }
}