using System.Security.Cryptography;
using System.Text;
using WorkTracker.Clock.Domain.Ports;

namespace WorkTracker.Clock.Domain.Services;

public class UtilsService : IUtilsService 
{
    public string GenerateHash(string rm)
    {
        using SHA256 sha256Hash = SHA256.Create();
        // ComputeHash - returns byte array
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rm));
        // Convert byte array to a string
        StringBuilder builder = new();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
    }
}