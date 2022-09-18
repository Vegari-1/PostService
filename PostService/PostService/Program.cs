using PostService.Repository;
using PostService.Repository.Interface;
using PostService.Service;
using PostService.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Builder;
using OpenTracing;
using Jaeger.Reporters;
using Jaeger;
using Jaeger.Senders.Thrift;
using Jaeger.Samplers;
using OpenTracing.Contrib.NetCore.Configuration;
using OpenTracing.Util;
using Prometheus;
using PostService.Middlewares.Exception;
using PostService.Repository.Interface.Sync;
using PostService.Repository.Sync;
using PostService.Service.Sync;
using PostService.Service.Interface.Sync;
using PostService.Middlewares.Events;
using BusService;
using Microsoft.Extensions.Options;
using PostService.Messaging;
using PostSevice.Middlewares.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// DB_HOST from Docker-Compose or Local if null
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");

builder.Services.Configure<AppConfig>(
    builder.Configuration.GetSection("AppConfig"));

// Nats
builder.Services.Configure<MessageBusSettings>(builder.Configuration.GetSection("Nats"));
builder.Services.AddSingleton<IMessageBusSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<MessageBusSettings>>().Value);
builder.Services.AddSingleton<IMessageBusService, MessageBusService>();
builder.Services.AddHostedService<ProfileMessageBusService>();
builder.Services.AddHostedService<ConnectionMessageBusService>();
builder.Services.AddHostedService<EventMessageBusService>();

// Postgres
if (dbHost == null)
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DislinktDbConnection"),
            x => x.MigrationsHistoryTable("__MigrationsHistory", "post")));
else
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(dbHost, x => x.MigrationsHistoryTable("__MigrationsHistory", "post")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

//repositories
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IReactionRepository, ReactionRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IConnectionRepository, ConnectionRepository>();

//services
builder.Services.AddScoped<IPostService, PostsService>();
builder.Services.AddScoped<IReactionService, ReactionService>();
builder.Services.AddScoped<ICommentService, CommentService>();

// Sync Services
builder.Services.AddScoped<IPostSyncService, PostSyncService>();
builder.Services.AddScoped<IProfileSyncService, ProfileSyncService>();
builder.Services.AddScoped<IConnectionSyncService, ConnectionSyncService>();
builder.Services.AddScoped<IEventSyncService, EventSyncService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddOpenTracing();

builder.Services.AddSingleton<ITracer>(sp =>
{
    var serviceName = sp.GetRequiredService<IWebHostEnvironment>().ApplicationName;
    var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
    var reporter = new RemoteReporter.Builder()
                    .WithLoggerFactory(loggerFactory)
                    .WithSender(new UdpSender("host.docker.internal", 6831, 0))
                    .Build();
    var tracer = new Tracer.Builder(serviceName)
        // The constant sampler reports every span.
        .WithSampler(new ConstSampler(true))
        // LoggingReporter prints every reported span to the logging framework.
        .WithLoggerFactory(loggerFactory)
        .WithReporter(reporter)
        .Build();

    GlobalTracer.Register(tracer);

    return tracer;
});

builder.Services.Configure<HttpHandlerDiagnosticOptions>(options =>
        options.OperationNameResolver =
            request => $"{request.Method.Method}: {request?.RequestUri?.AbsoluteUri}");


var app = builder.Build();

// Run all migrations only on Docker container
if (dbHost != null)
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseEventSenderMiddleware();

// Prometheus metrics
app.UseMetricServer();

app.Run();

namespace PostService
{
    public partial class Program { }
}
