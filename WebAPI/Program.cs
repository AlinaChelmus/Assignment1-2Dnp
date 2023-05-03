using Application.DaoInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using EfcDataAccess;
using EfcDataAccess.DAOs;
using FileData;
using FileData.DAO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<FileContext>();
builder.Services.AddScoped<IUserDao, UserEfcDao>();
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddDbContext<ForumContext>();
builder.Services.AddScoped<IForumDao, ForumEfcDao>();
builder.Services.AddScoped<IForumLogic, ForumLogic>();
var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

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