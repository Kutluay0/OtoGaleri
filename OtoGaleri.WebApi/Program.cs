using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using OtoGaleri.Application.Interfaces;
using OtoGaleri.Application.Mapping;
using OtoGaleri.Application.Services;
using OtoGaleri.Core.Interfaces;
using OtoGaleri.Core.Options;
using OtoGaleri.Infrastructure.Context;
using OtoGaleri.Infrastructure.Repositories;
using OtoGaleri.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabanı ve Repository Servis Kayıtları
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddHttpContextAccessor();

// 2. Application & Infrastructure Servisleri ve AutoMapper Kaydı
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISaleService, SaleService>();

// JWT Servis Kayıtları
builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("Jwt"));
var tokenOptions = builder.Configuration.GetSection("Jwt").Get<TokenOptions>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// 3. JWT Authentication Yapılandırması
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenOptions?.Issuer,
        ValidAudience = tokenOptions?.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions?.Key ?? string.Empty))
    };
});

// 4. Web API & Swagger Konfigürasyonu
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

// Swagger Güvenlik Tanımlamaları (Microsoft.OpenApi 3.x Sözdizimi)
// Swagger Güvenlik Tanımlamaları (Microsoft.OpenApi 3.x Tam Uyumlu)
// Swagger Güvenlik Tanımlamaları (Microsoft.OpenApi 3.x Tam Uyumlu)
// Swagger Güvenlik Tanımlamaları
// Swagger Güvenlik Tanımlamaları (Microsoft.OpenApi 3.x Tam Uyumlu & Header Bağlantılı)
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OtoGaleri Web API",
        Version = "v1"
    });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Aşağıdaki kutuya 'Bearer' yazıp bir boşluk bıraktıktan sonra Token değerinizi girin.\r\n\r\nÖrnek: 'Bearer eyJhbGciOiJIUzI1Ni...'"
    };

    options.AddSecurityDefinition("Bearer", securityScheme);

    options.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", doc),
            new List<string>()
        }
    });
});

var app = builder.Build();

// 5. HTTP İstek Hattı (Middleware) Konfigürasyonu
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();