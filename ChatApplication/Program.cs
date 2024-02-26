using ChatApplication.Context_Files;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using ChatApplication.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using ChatApplication.Firebase.ChatApplication.Firebase;

var builder = WebApplication.CreateBuilder(args);

var firebaseConfigPath = "Firebase/Config.json"; // Update with your JSON file path
try
{
    var firebaseCredential = GoogleCredential.FromFile(firebaseConfigPath);
    FirebaseApp.Create(new AppOptions
    {
        Credential = firebaseCredential
    });
}
catch (Exception ex)
{
    Console.WriteLine($"FirebaseApp initialization error: {ex.Message}");
}

builder.Services.AddControllers();
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnect")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Firebase controller to services
builder.Services.AddSingleton<FirebaseConfig>(); // Assuming FirebaseConfig is registered as a singleton

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapUsersEndpoints();

app.Run();
