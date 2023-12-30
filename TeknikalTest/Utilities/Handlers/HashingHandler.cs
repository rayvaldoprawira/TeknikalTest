namespace TeknikalTest.Utilities.Handlers;

public class HashingHandler
{
    private static string GenerateSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt();
    }

    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, GenerateSalt());
    }

    public static bool ValidatePassword(string password, string hashPasword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashPasword);
    }
}
