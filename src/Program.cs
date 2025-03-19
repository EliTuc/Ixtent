using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using MatchHype.Platform.Exceptions;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using MatchHype.Api.Features.DataProvider;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.EnableAnnotations());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddHttpClient();
builder.Services.AddScoped<CampaignService>();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 1;
        options.Window = TimeSpan.FromSeconds(60);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// app.UseExceptionHandleMiddleware();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
//app.UseExceptionHandler(exceptionHandlerApp
//  => exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context)));


app.UseExceptionHandler(
             options =>
             {
                 options.Run(
                     async context =>
                     {
                         var ex = context.Features.Get<IExceptionHandlerFeature>();
                         if (ex != null)
                         {
                             context.Response.StatusCode = (int)GetErrorCode(ex.Error);
                             context.Response.ContentType = "text/plain";
                             var err = $"Error: {ex.Error.Message}";
                             await context.Response.WriteAsync(err).ConfigureAwait(false);
                         }
                     });
             }
         );

var baseApi = app.MapGroup("/api").WithOpenApi();
var externalApi = baseApi.MapGroup("/v2");


baseApi.MapGet("/csv/generate/{campaignName}/{matchDate}/{language}", async (string campaignName, string matchDate,string language, CampaignService campaignService) =>
{

    try
    {
        // přidat data do eventRecords

        await campaignService.GenerateRoundInitialCsv(campaignName, matchDate, language); ;

        return Results.Ok("Data processed and CSV generated successfully.");
    }
    catch (HttpRequestException ex)
    {
        return Results.Problem(detail: ex.Message, statusCode: StatusCodes.Status502BadGateway);
    }
    catch (Exception ex)
    {
        return Results.Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
    }
});



app.UseRateLimiter();
app.Run();




static HttpStatusCode GetErrorCode(Exception e)
{
    switch (e)
    {
        case FootballException _:
            return HttpStatusCode.BadRequest;

        default:
            return HttpStatusCode.InternalServerError;
    }
}


public class ExceptionHandleMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleException(ex, httpContext);
        }
    }

    private async Task HandleException(Exception ex, HttpContext httpContext)
    {


        if (ex is InvalidOperationException)
        {
            httpContext.Response.StatusCode = 400; //HTTP status code
                                                   //httpContext.Response.WriteAsync("Invalid operation");
                                                   //httpContext.Response.WriteAsync("Invalid operation");             

        }
        else if (ex is ArgumentException)
        {
            await httpContext.Response.WriteAsync("Invalid argument");
        }
        else
        {
            await httpContext.Response.WriteAsync("Unknown error");
        }


    }

}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class ExceptionHandleMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandleMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandleMiddleware>();
    }
}
