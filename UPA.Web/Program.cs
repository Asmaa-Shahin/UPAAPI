using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using UPA.Web.Extensions;
using UPA.DAl;

using Microsoft.AspNetCore.Identity;
using UPA.DAL.Models;

using UPA.Extensions;
using Microsoft.AspNetCore.Authorization;
using UPA.Web.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UPAModel>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("UPAConn")));

;

builder.Services.AddApplicationServices();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.AllowAnyOrigin() // Update with your Angular app's URL
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
builder.Services.AddIdentityServices(builder.Configuration);
//builder.Services.AddSwaggerDocumentation();
//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//});
builder.Services.AddSignalR();
var app = builder.Build();
using var host = app.Services.CreateScope();
var services = host.ServiceProvider;
var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
try
{
    var context = services.GetRequiredService<UPAModel>();
    await context.Database.MigrateAsync();

    //var identityContext = services.GetRequiredService<AppIdentityDbContext>();
    //await identityContext.Database.MigrateAsync();
    //var userManger = services.GetRequiredService<UserManager<AppUser>>();

}
catch (Exception ex)
{
    var logger = LoggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "an error occured while migraton");
}
// Configure the HTTP request pipeline.
//app.UseMiddleware<ExceptionMiddleware>();
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseCors(
   "AllowAngularApp"
    );
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();




app.MapHub<ChatHub>("/ChatHub");
//app.Use(async (context, next) =>
//{
//    await next();
//    if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
//    {
//        context.Request.Path = "/index.html";
//        await next();
//    }
//});
app.UseDefaultFiles();
app.UseStaticFiles(); // For the wwwroot folder.
app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "UploadedAttachments")),
    RequestPath = "/UploadedAttachments",
    EnableDirectoryBrowsing = true
});



app.MapControllers();

app.Run();
