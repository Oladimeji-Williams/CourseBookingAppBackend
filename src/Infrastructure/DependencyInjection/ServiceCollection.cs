using System.Text;
using CloudinaryDotNet;
using CourseBookingAppBackend.src.Application.Validators;
using CourseBookingAppBackend.src.Domain.Entities;
using CourseBookingAppBackend.src.Infrastructure.Data;
using CourseBookingAppBackend.src.Infrastructure.Data.Interceptors;
using CourseBookingAppBackend.src.Infrastructure.Repositories;
using CourseBookingAppBackend.src.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using CourseBookingAppBackend.src.Application.Queries.Users;
using CourseBookingAppBackend.src.Application.Commands.Users;
using CourseBookingAppBackend.src.Application.Abstractions.Security;
using CourseBookingAppBackend.src.Application.Queries.Courses;
using CourseBookingAppBackend.src.Application.Commands.Courses;
using CourseBookingAppBackend.src.Application.Abstractions.Images;
using CourseBookingAppBackend.src.Application.Commands.Enrollments;
using CourseBookingAppBackend.src.Application.Queries.Enrollments;
using CourseBookingAppBackend.src.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.Application.Commands.Auth.Login;
using CourseBookingAppBackend.src.Application.Commands.Auth.Register;
using CourseBookingAppBackend.src.Infrastructure.Persistence;
using CourseBookingAppBackend.src.Application.Abstractions.Email;


namespace CourseBookingAppBackend.src.Infrastructure.DependencyInjection;

public static class ServiceCollection
{
  public static IServiceCollection AddMyDatabase(this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<AppDbContext>((serviceProvider, options) =>
    {
      options.UseSqlServer(connectionString);
      var interceptor = serviceProvider.GetRequiredService<TimeStampInterceptor>();
      options.AddInterceptors(interceptor);
    });
    return services;
  }
  public static IServiceCollection AddMyRepositories(this IServiceCollection services)
  {
    services.AddScoped<IAuthRepository, AuthRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<ICourseRepository, CourseRepository>();
    services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

    return services;
  }
  public static IServiceCollection AddMyServices(this IServiceCollection services)
  {
    services.AddScoped<IPasswordService, PasswordService>();
    services.AddScoped<GetUsersQueryHandler>();
    services.AddScoped<GetUserByIdQueryHandler>();

    services.AddScoped<ChangePasswordCommandHandler>();
    services.AddScoped<UpdateUserCommandHandler>();
    services.AddScoped<ChangeUserRoleCommandHandler>();
    services.AddScoped<DeleteUserCommandHandler>();
    services.AddScoped<UploadUserImageCommandHandler>();
    services.AddScoped<GetCoursesQueryHandler>();
    services.AddScoped<GetCourseByIdQueryHandler>();
    services.AddScoped<CreateCourseCommandHandler>();
    services.AddScoped<UpdateCourseCommandHandler>();
    services.AddScoped<DeleteCourseCommandHandler>();
    services.AddScoped<UploadCourseImageCommandHandler>();
    services.AddScoped<EnrollInCourseCommandHandler>();
    services.AddScoped<CancelEnrollmentCommandHandler>();
    services.AddScoped<GetMyEnrollmentsQueryHandler>();
    services.AddScoped<LoginCommandHandler>();
    services.AddScoped<RegisterCommandHandler>();



    services.AddScoped<IImageRepository, CloudinaryImageRepository>();



    return services;
  }
  public static IServiceCollection AddMyCors(this IServiceCollection services)
  {
    services.AddCors(options =>
    {
      options.AddPolicy("AllowAll", policy =>
      {
        policy.WithOrigins(
          "http://localhost:4200",
          "https://happy-smoke-0b506f81e.6.azurestaticapps.net"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
      });
    });
    return services;
  }

  public static IServiceCollection AddMyCloudinary(this IServiceCollection services)
  {
    services.AddSingleton(provider =>
    {
      var config = provider.GetRequiredService<IConfiguration>();

      var account = new Account(
      config["Cloudinary:CloudName"],
      config["Cloudinary:ApiKey"],
      config["Cloudinary:ApiSecret"]
      );

      return new Cloudinary(account);
    });
    return services;
  }
  public static IServiceCollection AddMyJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
  {
    // Read values from appsettings.json
    var jwtKey = configuration["Jwt:Key"];
    var jwtIssuer = configuration["Jwt:Issuer"];
    var jwtAudience = configuration["Jwt:Audience"];

    if (string.IsNullOrWhiteSpace(jwtKey))
      throw new InvalidOperationException("JWT Key is missing in configuration.");

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                          Encoding.UTF8.GetBytes(jwtKey)
                      )
          };
        });

    return services;
  }
  public static IServiceCollection AddMySwagger(this IServiceCollection services)
  {
    services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new() { Title = "CourseBookingAppAPI", Version = "v1" });

      c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        In = ParameterLocation.Header,
        Description = "Enter JWT token like: Bearer {your token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
      });

      c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
        });
    });
    return services;
  }
  public static IServiceCollection AddMyExtensions(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
    services.AddSingleton(provider =>
    {
      var config = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;
      return new Cloudinary(new Account(config.CloudName, config.ApiKey, config.ApiSecret));
    });
    services.AddScoped<ITokenService, TokenService>();
    services.AddSingleton<TimeStampInterceptor>();
    services.AddSingleton<PasswordHasher<User>>();
    return services;
  }
  public static IServiceCollection AddMyValidators(this IServiceCollection services)
  {
    services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
    return services;
  }
  public static IServiceCollection AddMyEmailService(
      this IServiceCollection services,
      IConfiguration configuration)
  {
    services.AddScoped<IEmailService, SmtpEmailService>();
    return services;
  }
}







