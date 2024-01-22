using UserModel = TaskHubAPI.Models.User;

namespace TaskHubAPI.Services.Token
{
    public interface ITokenService
    {
        string GenerateToken(UserModel user);
    }
}