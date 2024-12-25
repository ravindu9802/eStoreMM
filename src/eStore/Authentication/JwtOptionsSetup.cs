using Microsoft.Extensions.Options;
using Users.Infrastructure.Authentication;

namespace eStore.Authentication;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private const string SectionName = "JwtConfig";
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}