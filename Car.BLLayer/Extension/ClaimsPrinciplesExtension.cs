using System.Security.Claims;

namespace Car.BLLayer.Extension
{
    public static class ClaimsPrinciplesExtension
    {
        public static string RetieveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }
    }
}
