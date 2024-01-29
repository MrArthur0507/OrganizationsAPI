using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Organizations.API.Jobs;
using Organizations.API.Mapper;
using Organizations.API.Middlewares;
using Organizations.API.Services;
using Organizations.DbProvider.Config.Contracts;
using Organizations.DbProvider.Config.Implementations;
using Organizations.DbProvider.Queries.Contracts;
using Organizations.DbProvider.Queries.Implementations;
using Organizations.DbProvider.Repositories;
using Organizations.DbProvider.Repositories.Contracts;
using Organizations.DbProvider.Repositories.Implementations;
using Organizations.DbProvider.Services.Contracts;
using Organizations.DbProvider.Services.Implementations;
using Organizations.DbProvider.Tools.Contracts;
using Organizations.DbProvider.Tools.Implementations;
using Organizations.Models.Models;
using Organizations.Services.Implementations;
using Organizations.Services.Interfaces;
using Organizations.Services.Statistics.Contracts;
using Organizations.Services.Statistics.Implementations;
using Organizations.Services.Utilities;
using Quartz;
using System.Text;

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

//response caching
builder.Services.AddResponseCaching();
builder.Services.AddSingleton<IIpFilterService, IpFilterService>();
//mapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddMemoryCache();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<IReportService, ReportService>();
// services
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IIndustryService, IndustryService>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
// stat services
builder.Services.AddScoped<ICountryStatisticService, CountryStatisticService>();
builder.Services.AddScoped<IIndustryStatisticService, IndustryStatisticService>();
builder.Services.AddScoped<IDailyTotalService, DailyTotalService>();
builder.Services.AddScoped<IDateGetter, DateGetter>();
builder.Services.AddScoped<IOrganizationStatisticService, OrganizationStatisticService>();
// Db-related

builder.Services.AddSingleton<IConfigLoader, ConfigLoader>();
builder.Services.AddSingleton<ITableCreationConfig, TableCreationConfig>();
builder.Services.AddSingleton<IDbManager, SqliteDbManager>();
builder.Services.AddSingleton<ITableManager, SqliteTableManager>();
builder.Services.AddScoped<IOrganizationQuery, OrganizationQuery>();

builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IIndustryRepostiory, IndustryRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITotalCount, TotalCount>();
builder.Services.AddScoped<IOrganizationIdAssigner, OrganizationIdAssigner>();
builder.Services.AddSingleton<Organizations.DbProvider.Tools.Contracts.ILogger, SqliteLogger>();
builder.Services.AddScoped<ICountryQuery, CountryQuery>();
builder.Services.AddScoped<IIndustryQuery, IndustryQuery>();
//

//Account
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserManagement, UserManagement>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
//

//quartz jobs
builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("DailyTotalJob");
    q.AddJob<DailyTotalJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("DailyTotalJob-trigger")
        .StartNow()
        //.WithCronSchedule("0 0 0 * * ?"))
        .WithCronSchedule("0/5 * * * * ?"));

});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

//jwt and policies
builder.Services.AddAuthorization(options => {
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Admin");
    });
});
builder.Services.AddAuthentication().AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Secret-Key").Value))
        };
        
    });


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var dbInit = services.GetRequiredService<IDbManager>();
    dbInit.LoadDb();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<IpFilterMiddleware>();
app.UseMiddleware<CustomHeaderMiddleware>();
app.UseResponseCaching();
app.MapControllers();
app.Run();


