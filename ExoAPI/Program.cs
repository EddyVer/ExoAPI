using AutoMapper;
using ExoAPI.Mapping;
using ExoAPI.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ExoAPI.Service.CSVService;
using ExoAPI.Service.UserService;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using ExoAPI.Service.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IExcelService, ExcelService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(option => {
    option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standart Authorization header using the Bearer scheme(\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    option.OperationFilter<SecurityRequirementsOperationFilter>(); 
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer= false,
        ValidateAudience = false
    };
});

builder.Services.AddSingleton<ProduitContext>();
builder.Services.AddSingleton<UsersContext>();

builder.Services.AddDbContext<BusinessContext>();

builder.Services.AddAutoMapper(cfg => { cfg.AddProfile<MappingData>(); });

var app = builder.Build();

app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
// Configure the HTTP request pipeline.
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