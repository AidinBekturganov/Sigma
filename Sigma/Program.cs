using Sigma.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.MainServices();
builder.UseAutoMappers();
builder.UseRepositories();
builder.UseServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();