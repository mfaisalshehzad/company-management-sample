//using System.Reflection;
//using System.Text;

//using CompanyManagement.API.DBContexts;

//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Identity.Web;
//using Microsoft.IdentityModel.Logging;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;

//namespace CompanyManagement.API
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        private readonly string AllowSpecificOrigins = "_allowSpecificOrigins";

//        // This method gets called by the runtime. Use this method to add services to the container.
//        public void ConfigureServices(IServiceCollection services)
//        {
//            //Sql Server Connection String
//            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

//            services.AddCors(options =>
//            {
//                options.AddPolicy(name: AllowSpecificOrigins, builder =>
//                    {
//                        builder.WithOrigins("*", "http://localhost:53580", "http://localhost:5000")
//                        .AllowAnyHeader()
//                        .AllowAnyMethod();
//                    });
//            });

//            services.AddControllers();

//            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//            .AddJwtBearer(options =>
//            {
//                options.RequireHttpsMetadata = false;
//                options.SaveToken = true;
//                options.TokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuer = true,
//                    ValidateAudience = true,
//                    ValidateLifetime = true,
//                    ValidateIssuerSigningKey = true,
//                    ValidIssuer = Configuration["Jwt:Issuer"],
//                    ValidAudience = Configuration["Jwt:Audience"],
//                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
//                    ClockSkew = TimeSpan.Zero
//                };
//            });

//            IdentityModelEventSource.ShowPII = true;

//            services.AddSwaggerGen(options =>
//            {
//                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
//                {
//                    Title = "Admin API v1",
//                    Version = "v1",
//                    Description = "API to communicate with Admin Client Project"
//                });
//                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
//                {
//                    Name = "Authorization",
//                    Type = SecuritySchemeType.ApiKey,
//                    Scheme = "Bearer",
//                    BearerFormat = "JWT",
//                    In = ParameterLocation.Header,
//                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
//                });

//                options.AddSecurityRequirement(new OpenApiSecurityRequirement
//                {
//                    {
//                          new OpenApiSecurityScheme
//                            {
//                                Reference = new OpenApiReference
//                                {
//                                    Type = ReferenceType.SecurityScheme,
//                                    Id = "Bearer"
//                                }
//                            },
//                            new string[] {}

//                    }
//                });
//                // Set the comments path for the Swagger JSON and UI.
//                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//                options.IncludeXmlComments(xmlPath);
//            });

//            // Add services to the container.
//            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

//            builder.Services.AddControllers();
//            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();

//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();

//            app.UseAuthorization();

//            app.MapControllers();

//            app.Run();
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }

//            app.UseRouting();

//            app.UseCors(AllowSpecificOrigins);

//            app.UseAuthentication();

//            app.UseAuthorization();

//            app.UseEndpoints(endPoints =>
//            {
//                endPoints.MapControllers();
//            });

//            app.UseSwagger();
//            app.UseSwaggerUI(options =>
//            {
//                options.SwaggerEndpoint("/swagger/v2/swagger.json", "API v1");
//                options.RoutePrefix = string.Empty;
//            });
//        }
//    }
//}
