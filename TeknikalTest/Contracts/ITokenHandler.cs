using System.Security.Claims;

namespace TeknikalTest.Contracts
{
    public interface ITokenHandler
    {
        public string GenerateToken(IEnumerable<Claim> claims);
    }
}
