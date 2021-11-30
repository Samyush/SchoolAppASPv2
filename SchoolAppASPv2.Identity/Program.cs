using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolAppASPv2.Identity;
using SchoolAppASPv2.Identity.Data;
using SchoolAppASPv2.Identity.Models;
using SchoolAppASPv2.Identity.Services;
using System.Reflection;
using static OpenIddict.Abstractions.OpenIddictConstants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.Configure<AuthSettingModel>(builder.Configuration.GetSection("IdentityOptions"));
builder.Services.AddTransient<ILoginService<ApplicationUser>, EFLoginService>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{

    // Configure the context to use Microsoft SQL Server.
    options.UseSqlServer(builder.Configuration["ConnectionString"], sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
    });

    // Register the entity sets needed by OpenIddict.
    // Note: use the generic overload if you need
    // to replace the default OpenIddict entities.
    options.UseOpenIddict();
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.ClaimsIdentity.UserNameClaimType = Claims.Name;
    options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
    options.ClaimsIdentity.RoleClaimType = Claims.Role;
    options.SignIn.RequireConfirmedAccount = false;
});

builder.Services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                           .UseDbContext<ApplicationDbContext>();
                }).AddServer(options =>
                {
                    options.SetAuthorizationEndpointUris("/connect/authorize")
                           .SetLogoutEndpointUris("/connect/logout")
                            .SetTokenEndpointUris("/connect/token")
                           .SetIntrospectionEndpointUris("/connect/introspect")
                           .SetUserinfoEndpointUris("/connect/userinfo");

                    options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);

                    options.AllowImplicitFlow()
                             .AllowAuthorizationCodeFlow()
                             .AllowClientCredentialsFlow();

                    // Register the signing and encryption credentials.
                    //options.AddDevelopmentEncryptionCertificate()
                    //       .AddDevelopmentSigningCertificate();

                    options.AddEphemeralSigningKey()
                         .AddEphemeralEncryptionKey();


                    // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                    options.UseAspNetCore()
                    .DisableTransportSecurityRequirement()
                            .EnableTokenEndpointPassthrough()
                            .EnableAuthorizationEndpointPassthrough()
                           .EnableLogoutEndpointPassthrough()
                           .EnableUserinfoEndpointPassthrough()
                           .EnableStatusCodePagesIntegration();
                }).AddValidation(options =>
                {
                    // Import the configuration from the local OpenIddict server instance.
                    options.UseLocalServer();

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();


                });



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolAppASPv2.Identity v1");
        c.RoutePrefix = String.Empty;
    });
    app.UseDeveloperExceptionPage();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
