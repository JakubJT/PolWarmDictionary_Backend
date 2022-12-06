using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Logging.AzureAppServices;
using DAL;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using ApplicationServices.MapperProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Logging.AddConfiguration(
    builder.Configuration.GetSection("Logging"));
builder.Logging.AddAzureWebAppDiagnostics();
builder.Services.Configure<AzureFileLoggerOptions>(options =>
{
    options.FileName = "azure-diagnostics-";
    options.FileSizeLimit = 50 * 1024;
    options.RetainedFileCountLimit = 5;
});
builder.Services.Configure<AzureBlobLoggerOptions>(options =>
{
    options.BlobName = "log.txt";
});

builder.Services.AddControllers();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(ApplicationServices.MapperProfiles.WordProfile));

builder.Services.AddDbContext<DictionaryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(builder.Environment.IsDevelopment() ? "DictionaryDatabaseDevelop" : "DictionaryDatabase")));

builder.Services.AddSingleton<WebAPI.GraphClient>();
builder.Services.AddScoped<WordRepository>();
builder.Services.AddScoped<PartOfSpeechRepository>();
builder.Services.AddScoped<WordGroupRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddAutoMapper(typeof(WordProfile), typeof(PartOfSpeechProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DictionaryContext>();
    DbInitializer.Initialize(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseCors(policy =>
    policy.WithOrigins("https://localhost:7179", "https://localhost:5001")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithHeaders(HeaderNames.ContentType));
}
else if (app.Environment.IsProduction())
{
    app.UseCors(policy =>
    policy.WithOrigins("https://polwarmdictionary.azurewebsites.net")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType));
}

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-development");
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
