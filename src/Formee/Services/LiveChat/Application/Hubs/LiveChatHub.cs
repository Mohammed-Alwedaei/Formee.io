using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.Hubs;

public class LiveChatHub : Hub
{
    private readonly IAgentRepository _agentRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly ILogger<LiveChatHub> _logger;

    public LiveChatHub(IAgentRepository agentRepository, 
        ISessionRepository sessionRepository, ILogger<LiveChatHub> logger)
    {
        _agentRepository = agentRepository;
        _sessionRepository = sessionRepository;
        _logger = logger;
    }

    public async Task SendMessage(string userId, string message)
    {
        await Clients.Caller.SendAsync("ReceiveMessage", 
            userId,
            message);
    }

    public async Task StartSession(string fullname, string email)
    {
        var visitor = new VisitorEntity
        {
            FullName = fullname,
            Email = email
        };

        var session = new SessionEntity
        {
            Visitor = visitor
        };

        await _sessionRepository.CreateAsync(session);

        _logger.LogInformation("session started");
    }
}