using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Encodings.Web;

var builder = WebApplication.CreateBuilder(args);

var secretKey = "SafeVaultSuperSecretKeyForDevelopmentOnly123!"; 
var keyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateIssuer = false, 
            ValidateAudience = false, 
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

var secureVault = new List<VaultNote>();

// --- ENDPOINT'S ---

app.MapPost("/api/login", (LoginRequest request) =>
{
    if (request.Username == "admin" && request.Password == "admin123")
    {
        var token = GenerateJwtToken("admin", "Admin", keyBytes);
        return Results.Ok(new { Token = token });
    }
    if (request.Username == "user" && request.Password == "user123")
    {
        var token = GenerateJwtToken("user", "StandardUser", keyBytes);
        return Results.Ok(new { Token = token });
    }

    return Results.Unauthorized();
});

app.MapPost("/api/vault", (VaultNote note) =>
{
    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(note);
    bool isValid = Validator.TryValidateObject(note, validationContext, validationResults, true);

    if (!isValid) return Results.BadRequest(validationResults.Select(v => v.ErrorMessage));

    note.Content = HtmlEncoder.Default.Encode(note.Content);

    secureVault.Add(note);
    return Results.Created($"/api/vault/{note.Title}", note);
}).RequireAuthorization(); 

app.MapGet("/api/vault/secrets", () => 
{
    return Results.Ok(secureVault);
}).RequireAuthorization("RequireAdminRole"); 

app.Run();


string GenerateJwtToken(string username, string role, byte[] key)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role) 
        }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class VaultNote
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 50 characters.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Title can only contain alphanumeric characters.")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Content { get; set; } = string.Empty;
}

public partial class Program { }