using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

public class ApplicationApplicationUser
{
    [Key]
    public int ApplicationUserId { get; set; }

    [Required(ErrorMessage = "Imię jest wymagane")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Nazwisko jest wymagane")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "PESEL jest wymagany")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "PESEL musi składać się z 11 cyfr")]
    public string Pesel { get; set; }

    [Required(ErrorMessage = "E-mail jest wymagany")]
    [EmailAddress(ErrorMessage = "Nieprawidłowy format adresu e-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Telefon jest wymagany")]
    [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "Numer telefonu musi składać się z 9 cyfr")]
    public string Phone { get; set; }

    [Range(18, 120, ErrorMessage = "Wiek musi być pomiędzy 18 a 120")]
    public int? Age { get; set; }

    [RegularExpression(@"^\d+(\.\d{1,3})?$", ErrorMessage = "Nieprawidłowy format liczby")]
    [Range(0, 999999.999, ErrorMessage = "SrednieZuzyciePradu musi być pomiędzy 0 a 999999.999")]
    public decimal? AverageElectricityUsage { get; set; }

    [Required(ErrorMessage = "Hasło jest wymagane")]
    public string Password { get; set; }

    [Required]
    public string Salt { get; set; }

    [Required]
    public string PasswordHash
    {
        get
        {
            var salt = CreateSalt();
            var passwordHash = CreateHash(Password, salt);
            Salt = salt;
            return passwordHash;
        }
    }

    public string VerificationCode { get; set; }
    public int Id { get; internal set; }

    public string CreateSalt()
    {
        var saltBytes = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    private string CreateHash(string input, string salt)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
        using (var sha256 = SHA256.Create())
        {
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
