using Badminton.Contract;
using Badminton.DataAccess;
using BadmintonApi.Middleware;
using BadmintonAPI;
using BadmintonAPI.Extension;
using BadmintonAPI.Filter;
using BadmintonAPI.Middleware;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using System.Reflection;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy =>
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .WithExposedHeaders("Content-Disposition")
));

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiResponseFilter>();
    options.Filters.Add<ApiExceptionFilter>();
});

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
    logging.AddNLog();
});

builder.Services.AddDbContext<BadmintonDbContext>(opt =>
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x =>
        {
            x.MigrationsAssembly("BadmintonAPI");
        })
    .LogTo(Console.WriteLine, LogLevel.Information)
);

builder.Services.AddJwtService(builder.Configuration);

builder.Services.AddService(builder.Configuration);

builder.Services.AddScoped<IUserContext, UserContext>();

builder.Services.AddEndpointsApiExplorer();

//
// .NET10 新 OpenAPI
//
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer(
        async (document, context, cancellationToken) =>
        {
            document.Info.Title = "Badminton API";

            document.Info.Version = "v1";

            document.Info.Description =
                "羽球場預約系統 API";
        });
});

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.StatusCode = 500;

        var feature =
            context.Features
            .Get<IExceptionHandlerPathFeature>();

        if (feature?.Error is Exception ex)
        {
            var logger =
                NLog.LogManager.GetCurrentClassLogger();

            logger.Error(
                ex,
                JsonConvert.SerializeObject(ex));

            await context.Response.WriteAsJsonAsync(
                new
                {
                    error = ex.Message
                });
        }
    });
});

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseMiddleware<AuthenticationMiddleware>();

app.UseMiddleware<AuthorizationMiddleware>();

app.UseMiddleware<HttpHandleMiddleware>();

app.MapControllers();

app.Run();