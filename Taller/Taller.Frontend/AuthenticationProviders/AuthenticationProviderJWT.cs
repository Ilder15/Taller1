using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Taller.Frontend.Helpers;
using Taller.Frontend.Services;

namespace Taller.Frontend.AuthenticationProviders;

public class AuthenticationProviderJWT : AuthenticationStateProvider, ILoginService
{
    private readonly IJSRuntime _jSRuntime;
    private readonly HttpClient _httpClient;
    private readonly string _tokenKey;
    private readonly AuthenticationState _anonimous;

    public AuthenticationProviderJWT(IJSRuntime jSRuntime, HttpClient httpClient)
    {
        _jSRuntime = jSRuntime;
        _httpClient = httpClient;
        _tokenKey = "TOKEN_KEY";
        _anonimous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public async Task LoginAsync(string token)
    {
        await _jSRuntime.SetLocalStorage(_tokenKey, token);
        var authState = BuildAuthenticationState(token);
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    public async Task LogoutAsync()
    {
        await _jSRuntime.RemoveLocalStorage(_tokenKey);
        _httpClient.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged(Task.FromResult(_anonimous));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var token = await _jSRuntime.GetLocalStorage(_tokenKey);
            if (token is null)
            {
                return _anonimous;
            }
            return BuildAuthenticationState(token.ToString()!);
        }
        catch (InvalidOperationException)
        {
            return _anonimous;
        }
    }

    private AuthenticationState BuildAuthenticationState(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        var claims = ParseClaimsFromJWT(token);
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
    }

    private IEnumerable<Claim> ParseClaimsFromJWT(string token)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        // Leer el token
        var unserializedToken = jwtSecurityTokenHandler.ReadToken(token) as JwtSecurityToken;

        // Si la lectura falló o no es un JWT válido
        if (unserializedToken == null) return Enumerable.Empty<Claim>();

        // Crear una lista de Claims para el ClaimsIdentity
        var claims = new List<Claim>();

        // 1. Añadir todos los claims originales
        claims.AddRange(unserializedToken.Claims);

        // 2. ¡EL PASO CLAVE! Mapear el claim de rol.
        // Asumo que el claim de rol en tu JWT se llama 'role'. Si es diferente (ej: 'roles'), cámbialo aquí.
        var roleClaims = claims.Where(c => c.Type == "role" || c.Type == "Role").ToList();

        foreach (var roleClaim in roleClaims)
        {
            // Reemplazar o añadir el claim de rol al tipo estándar (ClaimTypes.Role)
            claims.Add(new Claim(ClaimTypes.Role, roleClaim.Value));
        }

        return claims;
    }
}