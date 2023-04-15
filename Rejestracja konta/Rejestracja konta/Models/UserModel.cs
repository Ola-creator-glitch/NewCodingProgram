using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

public class ApplicationUserModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Imię jest wymagane")]
    public string Imie { get; set; }

    [Required(ErrorMessage = "Nazwisko jest wymagane")]
    public string Nazwisko { get; set; }

    [Required(ErrorMessage = "PESEL jest wymagany")]
    public string Pesel { get; set; }

    [Required(ErrorMessage = "E-mail jest wymagany")]
    [EmailAddress(ErrorMessage = "Nieprawidłowy format adresu e-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Telefon jest wymagany")]
    public string Telefon { get; set; }

    [Range(18, 120, ErrorMessage = "Wiek musi być pomiędzy 18 a 120")]
    public int? Wiek { get; set; }

    [RegularExpression(@"^\d+(\.\d{1,3})?$", ErrorMessage = "Nieprawidłowy format liczby")]
    [Range(0, 999999.999, ErrorMessage = "SrednieZuzyciePradu musi być pomiędzy 0 a 999999.999")]
    public decimal? SrednieZuzyciePradu { get; set; }

    [Required(ErrorMessage = "Hasło jest wymagane")]
    public string Haslo { get; set; }

    [Required]
    public string Sol { get; set; }
    public string HasloSalt { get; set; }
    public string KodWeryfikacyjny { get; set; }

    [Required]
    public string SzyfrowaneHaslo
    {
        get
        {
            var salt = CreateSalt();
            var hasloHash = CreateHash(Haslo, salt);
            HasloSalt = hasloHash;
            return hasloHash;
        }
    }

    public string CreateSalt()
    {
        var saltBytes = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        Sol = Convert.ToBase64String(saltBytes);
        return Sol;
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
