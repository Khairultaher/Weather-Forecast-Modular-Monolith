using System.Security.Cryptography;
using System.Text;

namespace Shared;

public class DataEncryptionService : IDataEncryptionService {
    private readonly string _encryptionKey = "zx89bc45a9d5a1q5"; // 16 bytes for AES-128
    private readonly string _initializationVector = "d5as45a65dc25fd4"; // 16 bytes
    private readonly string _hmacKey = "supersecretkey"; // Secret key for HMAC

    // AES Encryption
    public string GenerateEncryptedString_Aes(string plainText, string key = "", string initVector = "") {
        key = string.IsNullOrEmpty(key) ? _encryptionKey : key;
        initVector = string.IsNullOrEmpty(initVector) ? _initializationVector : initVector;

        using Aes aesAlg = Aes.Create();
        aesAlg.Key = Encoding.UTF8.GetBytes(key);
        aesAlg.IV = Encoding.UTF8.GetBytes(initVector);

        using MemoryStream msEncrypt = new MemoryStream();
        using CryptoStream csEncrypt = new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write);
        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) {
            swEncrypt.Write(plainText);
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    // AES Decryption
    public string GenerateDecryptedString_Aes(string cipherText, string key = "", string initVector = "") {
        key = string.IsNullOrEmpty(key) ? _encryptionKey : key;
        initVector = string.IsNullOrEmpty(initVector) ? _initializationVector : initVector;

        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        using Aes aesAlg = Aes.Create();
        aesAlg.Key = Encoding.UTF8.GetBytes(key);
        aesAlg.IV = Encoding.UTF8.GetBytes(initVector);

        using MemoryStream msDecrypt = new MemoryStream(cipherBytes);
        using CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();
    }

    // Hashing with SHA-256
    public string GenerateHash(string input) {
        using SHA256 sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower(); // Convert to hex string
    }

    // HMACSHA256 for Message Authentication (with a secret key)
    public string GenerateHmacHash(string input, string key = "") {
        key = string.IsNullOrEmpty(key) ? _hmacKey : key;
        using HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
        byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}
