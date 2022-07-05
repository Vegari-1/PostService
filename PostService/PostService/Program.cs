using PostService.Middlewares;
using PostService.Repository;
using PostService.Repository.Interface;
using PostService.Service;
using PostService.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DislinktDbConnection")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

//repositories
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IReactionRepository, ReactionRepository>();

//services
builder.Services.AddScoped<IPostService, PostsService>();
builder.Services.AddScoped<IReactionService, ReactionService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();

