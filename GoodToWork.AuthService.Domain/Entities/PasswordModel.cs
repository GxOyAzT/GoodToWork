using System.Security.Cryptography;
using System.Text;

namespace GoodToWork.AuthService.Domain.Entities;

public class PasswordModel
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string Password { get; set; }

    public string HashPassword 
    { 
        get 
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(Password);
                byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                return hash;
            }
        } 
    }
}
