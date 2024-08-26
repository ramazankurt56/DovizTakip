using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DovizTakipServer.Infrastructure.Options;
public sealed class JwtTokenOptionsSetup(
    IOptions<JwtOptions> jwtOptions,
    ILogger<JwtTokenOptionsSetup> logger,
    IConfiguration configuration
    ) : IPostConfigureOptions<JwtBearerOptions>
{
    public void PostConfigure(string? name, JwtBearerOptions options)
    {

        var jwtConfiguration = configuration.GetSection("Jwt").Value;

        logger.LogInformation("JwtTokenOptionsSetup.PostConfigure {options} {secretKey}", options, jwtOptions.Value.SecretKey);

        logger.LogInformation("JwtTokenOptionsSetup.PostConfigure jwt config from configuration {configuration}", jwtConfiguration);
        options.TokenValidationParameters.ValidateIssuer = true;
        options.TokenValidationParameters.ValidateAudience = true;
        options.TokenValidationParameters.ValidateLifetime = true;
        options.TokenValidationParameters.ValidateIssuerSigningKey = true;
        options.TokenValidationParameters.ValidIssuer = jwtOptions.Value.Issuer;
        options.TokenValidationParameters.ValidAudience = jwtOptions.Value.Audience;
        options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));
    }
}