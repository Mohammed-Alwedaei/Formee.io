using System.IdentityModel.Tokens.Jwt;
using Identity.BusinessLogic.Services.IServices;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Identity.BusinessLogic.Contexts;
using Identity.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.BusinessLogic.Services;

public class JwtManager : IJwtManager
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public JwtManager(IConfiguration configuration, ApplicationDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<string> GetAsync(string issuedFor)
    {
        var expireTime = DateTime.UtcNow.AddHours(-1);

        var tokenFromDb = await _context.AccessToken
            .FirstOrDefaultAsync(t => t.IssuedFor == issuedFor 
                                      && t.IsExpired == false);

        //If token is not available create on
        if (tokenFromDb == null)
        {
            //Generate new token
            return await CreateAsync(issuedFor);
        }

        /* if token exists check the date is expired or not
         * if expired mark token as expired and issue a new one
         */
        if (tokenFromDb != null && 
            DateTime.Compare(tokenFromDb.CreatedDate, expireTime) < 0)
        {
            tokenFromDb.IsExpired = true;

            _context.AccessToken.Update(tokenFromDb);

            await _context.SaveChangesAsync();

            return await CreateAsync(issuedFor);
        }

        /*
         * The token is not expired decrypt and return the token
         */

        return tokenFromDb.Token;
    }

    public async Task<string> CreateAsync(string issuedFor)
    {
        var audience = _configuration["Identity:Audience"];
        var issuer = _configuration["Identity:Issuer"];

        var token = GenerateAndEncryptToken(issuedFor, issuer, audience);

        var accessTokenModel = new AccessTokenEntity
        {
            Token = token,
            IssuedFor = issuedFor,
            IsExpired = false,
            CreatedDate = DateTime.UtcNow,
        };

        await _context.AccessToken.AddAsync(accessTokenModel);

        await _context.SaveChangesAsync();

        return token;
    }

    public async Task<string> MarkAsExpiredAsync(Guid issuedFor)
    {
        throw new NotImplementedException();
    }

    private string GenerateAndEncryptToken(string userId,
        string issuer,
        string audience)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim("authority", "dev-pnxnfhh8.us.auth0.com"),
        };

        var secretKey = _configuration["Identity:SecretKey"];
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var handler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = handler.CreateJwtSecurityToken(
                issuer: issuer,
                audience: audience, 
                new ClaimsIdentity(claims),
                null,
                 DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow,
                 signingCredentials: signingCredentials);

        return handler.WriteToken(jwtSecurityToken);
    }

    private string DecryptToken(string token)
    {
        throw new NotImplementedException();
    }
}
