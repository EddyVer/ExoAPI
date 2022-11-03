using AutoMapper;
using ExoAPI.Mapping;
using ExoAPI.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ProduitContext>();
builder.Services.AddSingleton<UsersContext>();
//builder.Services.AddSingleton<EntrepotContext>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();