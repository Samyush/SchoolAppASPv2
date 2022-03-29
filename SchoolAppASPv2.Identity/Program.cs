using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolAppASPv2.Identity;
using SchoolAppASPv2.Identity.Data;
using SchoolAppASPv2.Identity.Models;
using SchoolAppASPv2.Identity.Services;
// using SchoolAppASPv2.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.Configure<AuthSettingModel>(builder.Configuration.GetSection("IdentityOptions"));
builder.Services.AddTransient<ILoginService<ApplicationUser>, EFLoginService>();


builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseNpgsql(
                  builder.Configuration.GetConnectionString("SchoolAppAspConnection"),
                  b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
    
});



builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();



var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolAppASPv2.Identity v1");
//        c.RoutePrefix = String.Empty;
//    });
//    app.UseDeveloperExceptionPage();
//}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolAppASPv2.Identity v1");
    c.RoutePrefix = String.Empty;
});
app.UseDeveloperExceptionPage();

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
