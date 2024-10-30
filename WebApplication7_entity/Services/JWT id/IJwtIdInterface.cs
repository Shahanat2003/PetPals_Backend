namespace WebApplication7_petPals.Services.JWT_id
{
    public interface IJwtIdInterface
    {
        int GetUserFromToken(string token);
    }
}
