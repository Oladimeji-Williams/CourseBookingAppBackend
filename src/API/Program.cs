using CourseBookingAppBackend.src.Domain.Entities;
using CourseBookingAppBackend.src.Infrastructure.Data;
using CourseBookingAppBackend.src.Infrastructure.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using CourseBookingAppBackend.src.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Load the appropriate .env file
if (builder.Environment.IsDevelopment())
    Env.Load(".env.development");
else
    Env.Load(".env");

// Add environment variables so IConfiguration can see them
builder.Configuration.AddEnvironmentVariables();


var config = builder.Configuration;
bool isSeeding = args.Contains("--seed");
builder.Services
    .AddMyDatabase(config)
    .AddMyRepositories()
    .AddMyCors()
    .AddMyServices()
    .AddMyExtensions(config)
    .AddMyJwtAuthentication(config)
    .AddMySwagger()
    .AddMyCloudinary()
    .AddMyValidators()
    .AddMyEmailService(config);
builder.Services.AddMyControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddMyValidators();


builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// ---------------- SEEDING MODE ----------------
if (isSeeding)
{
    Console.WriteLine("🔧 Seeder mode detected. Starting database seeding...");

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var hasher = scope.ServiceProvider.GetRequiredService<PasswordHasher<User>>(); // ✅ inject hasher

    // Ensure database is up to date
    await context.Database.MigrateAsync();

    // Clear existing data safely
    if (await context.Enrollments.AnyAsync())
        context.Enrollments.RemoveRange(context.Enrollments);

    if (await context.Users.AnyAsync())
        context.Users.RemoveRange(context.Users);

    if (await context.Courses.AnyAsync())
        context.Courses.RemoveRange(context.Courses);

    await context.SaveChangesAsync();

    // Seed data
    await DataSeeder.SeedAsync(context, hasher);

    Console.WriteLine("✅ Seeding complete!");
    return;
}


if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExpensesApp API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();