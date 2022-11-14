using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using DAL;
using MediatR;
using System.Reflection;
using ApplicationServices.MapperProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(ApplicationServices.MapperProfiles.WordProfile));
// builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


builder.Services.AddDbContext<DictionaryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(builder.Environment.IsDevelopment() ? "DictionaryDatabaseDevelop" : "DictionaryDatabase")));

builder.Services.AddScoped<WordRepository>();
builder.Services.AddScoped<PartOfSpeechRepository>();
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
    .WithHeaders(HeaderNames.ContentType));
}
else if (app.Environment.IsDevelopment())
{
    app.UseCors(policy =>
    policy.WithOrigins("https://polwarmdictionary.azurewebsites.net")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType));

    app.UseHttpsRedirection();
}


if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-development");
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseAuthorization();

app.MapControllers();

app.Run();
