using Analytics.Utilities.Dtos.Session;

namespace Analytics.BusinessLogic.Repositories;

public class SessionsRepository : ISessionRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SessionsRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SessionDto> GetAsync(string deviceId)
    {
        var sessionFromDb = await _context.Sessions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.DeviceId == deviceId);

        return sessionFromDb is null 
            ? new SessionDto() 
            : _mapper.Map<SessionDto>(sessionFromDb);
    }

    public async Task<SessionDto> CreateAsync(SessionDto session)
    {
        var sessionToCreate = _mapper.Map<SessionEntity>(session);

        sessionToCreate.LastDeviceHit = DateTime.UtcNow;
        sessionToCreate.CreatedDate = DateTime.UtcNow;

        await _context.Sessions.AddAsync(sessionToCreate);

        await _context.SaveChangesAsync();

        return _mapper.Map<SessionDto>(sessionToCreate);
    }

    public async Task<SessionDto> UpdateAsync(SessionDto session)
    {
        var sessionToUpdate = _mapper.Map<SessionEntity>(session);

        sessionToUpdate.LastDeviceHit = DateTime.UtcNow;
        sessionToUpdate.LastModified = DateTime.UtcNow;

        _context.Sessions.Update(sessionToUpdate);

        await _context.SaveChangesAsync();

        return _mapper.Map<SessionDto>(sessionToUpdate);
    }
}
