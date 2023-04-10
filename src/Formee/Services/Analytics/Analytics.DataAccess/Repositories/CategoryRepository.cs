namespace Analytics.BusinessLogic.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CategoryRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<CategoryEntity?> GetByIdAsync(int id)
    {
        return await _db.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id 
                                      && c.IsDeleted != true);
    }

    public async Task<List<CategoryEntity?>> GetAllAsync()
    {
        return await _db.Categories
            .AsNoTracking()
            .Where(c => c.IsDeleted != true)
            .ToListAsync();
    }

    public async Task<CreateCategoryDto> CreateAsync(CreateCategoryDto category)
    {
        var categoryToCreate = _mapper.Map<CategoryEntity>(category);

        await _db.Categories.AddAsync(categoryToCreate);

        await _db.SaveChangesAsync();

        return _mapper.Map<CreateCategoryDto>(categoryToCreate);
    }

    public async Task<UpdateCategoryDto> UpdateAsync(UpdateCategoryDto category)
    {

        var categoryToUpdate = _mapper.Map<CategoryEntity>(category);

        categoryToUpdate.IdModified = true;
        categoryToUpdate.LastModifiedDate = DateTime.Now;

        _db.Categories.Update(categoryToUpdate);

        await _db.SaveChangesAsync();

        return _mapper.Map<UpdateCategoryDto>(categoryToUpdate);
    }

    public async Task<DeleteCategoryDto> DeleteAsync(int id)
    {
        var categoryToDelete = await GetByIdAsync(id);

        if (categoryToDelete is null)
        {
            return new DeleteCategoryDto();
        }

        categoryToDelete.IdModified = true;
        categoryToDelete.LastModifiedDate = DateTime.Now;

        categoryToDelete.IsDeleted = true;
        categoryToDelete.DeletedDate = DateTime.Now;

        _db.Categories.Update(categoryToDelete);

        await _db.SaveChangesAsync();

        return _mapper.Map<DeleteCategoryDto>(categoryToDelete);

    }
}
