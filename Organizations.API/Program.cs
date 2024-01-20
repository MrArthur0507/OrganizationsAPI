using Microsoft.OpenApi.Models;
using Organizations.API.Mapper;
using Organizations.DbProvider.Repositories.Contracts;
using Organizations.DbProvider.Repositories.Implementations;
using Organizations.Models.Models;
using Organizations.Services.Implementations;
using Organizations.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

//mapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// services
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IIndustryService, IndustryService>();

// Db-related
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IIndustryRepostiory, IndustryRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
//

//Account
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserManagement, UserManagement>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
//
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
