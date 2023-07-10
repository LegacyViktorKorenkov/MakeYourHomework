namespace MakeYourHomework.AuthService.Services.Abstraction;

public interface ITokenService <TIn, TOut> where TIn : class
{
    Task<TOut> CreateTokenAsync(TIn tokenSubject);
}
