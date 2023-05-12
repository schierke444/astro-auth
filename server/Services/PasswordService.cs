using System.Security.Cryptography;
using System.Text;
using System;

namespace server.Services;

public sealed class PasswordService : IPasswordService
{
    private const string Salt = "9003A697CA6F038B5140A9A86D000899E1521C4B29BE5996E452882E2103D2404AEB3F2EB89DECB63310D8F6B3B02FF15323CE8DE4F9F7547641D5A2FFB1F698";
    private const int KeySize = 64;
    private const int Iterations = 350000;
    private readonly HashAlgorithmName _hashAlgorithm;
    public PasswordService()
    {   
        _hashAlgorithm = HashAlgorithmName.SHA512;
    }
    public string HashPassword(string password)
    {
        
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            Encoding.UTF8.GetBytes(Salt),
            Iterations,
            _hashAlgorithm,
            KeySize
            );

        return Convert.ToHexString(hash);
    }

    public bool VerifyPassword(string password, string hash)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
            password,
            Encoding.UTF8.GetBytes(Salt),
            Iterations,
            _hashAlgorithm,
            KeySize
            );
        
        return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
    }
}
