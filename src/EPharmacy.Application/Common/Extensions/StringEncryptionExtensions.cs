using System.Security.Cryptography;

namespace EPharmacy.Application.Common.Extensions;

public static class StringEncryptionExtensions
{
    private static readonly byte[] KEY_64 = { 42, 16, 93, 156, 78, 4, 218, 32 };
    private static readonly byte[] IV_64 = { 55, 103, 246, 79, 36, 99, 167, 3 };

    public static string Encrypt(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        var cryptoProvider = new DESCryptoServiceProvider();
        var ms = new MemoryStream();
        var cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_64, IV_64), CryptoStreamMode.Write);
        var sw = new StreamWriter(cs);

        sw.Write(value);
        sw.Flush();
        cs.FlushFinalBlock();
        ms.Flush();

        // convert back to a string
        return Convert.ToBase64String(ms.GetBuffer(), 0, ms.Length.GetHashCode());
    }

    public static string Decrypt(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        var cryptoProvider = new DESCryptoServiceProvider();

        var buffer = Convert.FromBase64String(value);
        var ms = new MemoryStream(buffer);
        var cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_64, IV_64), CryptoStreamMode.Read);
        var sr = new StreamReader(cs);

        return sr.ReadToEnd();
    }
}