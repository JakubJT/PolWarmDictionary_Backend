using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using DAL;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(ApplicationServices.Domain.ResponseBase<>).GetTypeInfo().Assembly);
// builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


builder.Services.AddDbContext<DictionaryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DictionaryContextSQL")));

builder.Services.AddScoped<WordRepository>();
builder.Services.AddScoped<PartOfSpeechRepository>();
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

app.UseCors(policy =>
    policy.WithOrigins("https://localhost:7179", "https://localhost:5001")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
