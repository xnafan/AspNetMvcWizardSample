namespace AspNetMvcWizardSample.DataAccess;
public class AuthenticationProviderStub : IAuthenticationProvider
{
    public int UserId { get; set; }
    public AuthenticationProviderStub(int userId) => UserId = userId;
    public int GetCurrentUserId() => UserId;
}