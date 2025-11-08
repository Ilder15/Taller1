using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Taller.Frontend.AuthenticationProviders;

public class AuthenticationProviderTest : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        await Task.Delay(1000);
        var anonimous = new ClaimsIdentity();
        var admin = new ClaimsIdentity(
        [
            new("FirstName", "Ilder"),
            new("LastName", "Lopez"),
            new(ClaimTypes.Name, "ilder@gmail.com"),
             new(ClaimTypes.Role, "Admin")
        ],
        authenticationType: "test");

        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(admin)));

    }

}