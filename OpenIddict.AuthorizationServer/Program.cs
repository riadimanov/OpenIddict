using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenIddict.AuthorizationServer.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => options.LoginPath = "/account/login");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
//});
//OpenIddict servisini uygulamaya ekliyoruz.
builder.Services.AddOpenIddict()
    //OpenIddict core/çekirdek yapýlandýrmalarý gerçekleþtiriliyor.
    .AddCore(options =>
    {
        //Entity Framework Core kullanýlacaðý bildiriliyor.
        options.UseEntityFrameworkCore()
               //Kullanýlacak context nesnesi bildiriliyor.
               .UseDbContext<ApplicationDbContext>();
    })
    //OpenIddict server yapýlandýrmalarý gerçekleþtiriliyor.
    .AddServer(options =>
    {
        //Token talebinde bulunulacak endpoint'i set ediyoruz.
        options.SetTokenEndpointUris("/connect/token")
           .SetAuthorizationEndpointUris("/connect/authorize")
           .SetLogoutEndpointUris("/connect/logout")
           //Kullanýcý bilgilerini edinebilmek için userinfo endpoint'ini set ediyoruz.
           .SetUserinfoEndpointUris("/connect/userinfo");
           
        //Akýþ türü olarak Client Credentials Flow'u etkinleþtiriyoruz.
        options.AllowClientCredentialsFlow()
                //Authorization Code Flow'u etkileþtiriyoruz.
               .AllowAuthorizationCodeFlow()
               .AllowRefreshTokenFlow()
               .RequireProofKeyForCodeExchange(); 
        //Signing ve encryption sertifikalarýný ekliyoruz.
        options.AddEphemeralEncryptionKey()
               .AddEphemeralSigningKey()
               //Normalde OpenIddict üretilecek token'ý güvenlik amacýyla þifreli bir þekilde bizlere sunmaktadýr.
               //Haliyle jwt.io sayfasýnda bu token'ý çözümleyip görmek istediðimizde þifresinden dolayý
               //incelemede bulunamayýz. Bu DisableAccessTokenEncryption özelliði sayesinde üretilen access token'ýn
               //þifrelenmesini iptal ediyoruz.
               .DisableAccessTokenEncryption();
        //OpenIddict Server servislerini IoC Container'a ekliyoruz.
        options.UseAspNetCore()
           .EnableTokenEndpointPassthrough()
           .EnableAuthorizationEndpointPassthrough()
           .EnableLogoutEndpointPassthrough()
           .EnableUserinfoEndpointPassthrough();
        //options.UseAspNetCore()
        //       .EnableTokenEndpointPassthrough()
        //       //EnableAuthorizationEndpointPassthrough: OpenID Connect request'lerinin Authorization Endpoint için aktifleþtirilmesini saðlar.
        //       .EnableAuthorizationEndpointPassthrough();
        //Yetkileri(scope) belirliyoruz.
        options.RegisterScopes("read", "write");
    });

//OpenIddict'i SQL Server'ý kullanacak þekilde yapýlandýrýyoruz.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
    //OpenIddict tarafýndan ihtiyaç duyulan Entity sýnýflarýný kaydediyoruz.
    options.UseOpenIddict();
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();