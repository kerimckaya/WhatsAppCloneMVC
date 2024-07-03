using System.IdentityModel.Tokens.Jwt;

namespace BaseAPI.Services
{
    public class UserService : IUserService
    {

        public int GetUserIdWithToken(string token)
        {
            try
            {
                //string[] token = ((string?)request.Headers.Authorization ?? "").Split(' ');
                JwtSecurityTokenHandler handler = new();
                JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(token);
                int kullId = 0;
                int.TryParse(jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value ?? "-1", out kullId);
                return kullId;
            }
            catch (Exception)
            {
                return 0; 
                throw;
            }
            
        }
        public int GetGecmisIslemGunWithToken(HttpRequest request)
        {
            string[] token = ((string?)request.Headers["Authorization"] ?? "").Split(' ');
            JwtSecurityTokenHandler handler = new();
            JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(token[1]);
            return int.Parse(jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "GecmisIslemGun")?.Value ?? "0");
        }

    }
}
