using Analytics.Utilities.Dtos.Category;
using Analytics.Utilities.Entities;

namespace Analytics.BusinessLogic.Repositories.IRepository;
public interface ICategoryRepository
{
    Task<CategoryEntity?> GetByIdAsync(int id);

    Task<List<CategoryEntity?>> GetAllAsync();

    Task<CreateCategoryDto> CreateAsync(CreateCategoryDto category);

    Task<UpdateCategoryDto> UpdateAsync(UpdateCategoryDto category);

    Task<DeleteCategoryDto> DeleteAsync(int id);
}
