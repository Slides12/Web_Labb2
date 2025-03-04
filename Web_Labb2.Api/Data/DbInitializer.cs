using System.Security.Cryptography;
using System.Text;
using Web_Labb2.Shared.Models;

public static class DbInitializer
{
    public static void Seed(APIDBContext context)
    {
        if (!context.UserEntitys.Any())
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes("YourSuperSecretKeyForJwtSigning"));

            context.UserEntitys.AddRange(new List<User>
            {
                new User
                {
                    Username = "admin",
                    PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes("password"))),
                    Role = "Admin"
                },
                new User
                {
                    Username = "user",
                    PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes("password"))),
                    Role = "User"
                }
            });

            context.SaveChanges();
        }
    }
}
