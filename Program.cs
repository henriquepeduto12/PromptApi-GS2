var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<PromptApi.Repository.IPromptRepository, PromptApi.Repository.PromptRepository>();
builder.Services.AddScoped<PromptApi.Services.IPromptService, PromptApi.Services.PromptService>();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var feature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        var payload = new { message = "Erro interno do servidor", requestId = context.TraceIdentifier, timestamp = DateTime.UtcNow };
        await context.Response.WriteAsJsonAsync(payload);
    });
});

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
