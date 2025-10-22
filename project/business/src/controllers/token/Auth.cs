using System.Text;
using System.Security.Cryptography;

public class Auth {

    private const int it = 1000;
    private const int password_length = 32;

    public static string get_password_hashed(string password, int salt) {

        byte[] salt_bytes = BitConverter.GetBytes(salt);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt_bytes, Auth.it, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(Auth.password_length);

        StringBuilder sb = new StringBuilder(Auth.password_length * 2);
        foreach (byte b in hash)
            sb.Append(b.ToString("x2"));

        return sb.ToString();
        
    }

    public static bool verify_password(string password_to_be_checked, string password_stored, int salt) {

        string password_to_be_checked_hashed = get_password_hashed(password_to_be_checked, salt);
        return string.Equals(password_stored, password_to_be_checked_hashed, StringComparison.Ordinal);
        
    }


}

