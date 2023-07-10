using System.Net;

namespace MakeYourHomework.AuthService.Models;

public class AuthResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public string JwToken { get; set; }
}
