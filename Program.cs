using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.ML.OnnxRuntime;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200",
        policy => policy.WithOrigins("http://localhost:4200") 
                         .AllowAnyHeader()
                         .AllowAnyMethod()
                         .AllowCredentials());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost4200");

app.UseAuthorization();

app.MapControllers();

app.Run();
