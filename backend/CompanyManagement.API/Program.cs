using CompanyManagement.API.Contracts;
using CompanyManagement.API.DBContexts;
using CompanyManagement.API.Services;

using CompanyManagementService.Mappings;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Reflection;
using System.Text;

//const string ALLOW_SPECIFIC_ORIGINS = "_allowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

//Host.CreateDefaultBuilder(args)
//    .ConfigureWebHostDefaults
//    (
//        webBuilder =>
//        {
//            webBuilder.UseStartup<Startup>();
//        }
//    )
//    .Build()
//    .Run();

var services = builder.Services;
var configuration = builder.Configuration;

//Sql Server Connection String
services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

// Add services to the container.
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])),
            ClockSkew = TimeSpan.Zero
        };
    });

IdentityModelEventSource.ShowPII = true;

//services.AddCors(options =>
//{
//    options.AddPolicy(name: ALLOW_SPECIFIC_ORIGINS, builder =>
//    {
//        builder.WithOrigins("*", "http://localhost:53580", "http://localhost:5000")
//        .AllowAnyHeader()
//        .AllowAnyMethod();
//    });
//});

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Company Management API v1",
        Version = "v1",
        Description = "API to communicate with the frontend project"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

services.AddHttpContextAccessor();
services.AddScoped<ITokenService, TokenService>();
services.AddScoped<IUserLoginService, UserLoginService>();
services.AddScoped<ICompanyService, CompanyService>();

services.RegisterMapperProfiles();


var app = builder.Build();

var env = app.Environment;

// Configure the HTTP request pipeline.
if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endPoints =>
{
    endPoints.MapControllers();
});

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/index.html", "API v1");
    options.RoutePrefix = string.Empty;
});

app.Run();
