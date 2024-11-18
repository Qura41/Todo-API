// Program.cs

using TodoAPI.AppDataContext;
using TodoAPI.Interfaces;
using TodoAPI.Middleware;
using TodoAPI.Models;
using TodoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
builder.Services.AddSingleton<TodoDbContext>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddLogging();

builder.Services.AddScoped<ITodoServices, TodoServices>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalApp",
        builder => builder.AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
});

var app = builder.Build();

{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider;
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();
app.UseAuthorization();

app.UseCors("AllowLocalApp");

app.MapControllers();

app.Run();