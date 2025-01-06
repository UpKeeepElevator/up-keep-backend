using System.Security.Cryptography;
using System.Text;

namespace UpKeep.Data.DTO.Core.Usuarios;

public class AuthUsuario
{
    public string cuenta { get; set; }
    public string contrasena { get; set; }


    private const int keySize = 64;
    private const int iterations = 350000;
    private HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public void HashPasword(byte[] salt)
    {
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(contrasena),
            salt,
            iterations,
            hashAlgorithm,
            keySize);
        contrasena = Convert.ToHexString(hash);
    }

    public byte[] GetNewSalt() => RandomNumberGenerator.GetBytes(keySize);
}