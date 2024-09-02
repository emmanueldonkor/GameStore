using Microsoft.AspNetCore.Authorization;

namespace Domain.Authorization;

public class HasScopeRequirement(string issuer, string scope) : IAuthorizationRequirement
{
    public string Issuer { get; } = issuer;
    public string Scope { get; } = scope;
}