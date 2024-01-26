using Microsoft.IdentityModel.Tokens;

namespace Core.Security.Encryption;

public static class SigningCredentialHelper
{
    public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey) =>
        new(securityKey, SecurityAlgorithms.HmacSha512Signature);
}