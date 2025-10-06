using Microsoft.AspNetCore.Identity;

namespace Web2_TranThienEm_BT.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
