using System.Text.Json.Serialization;
using white_cloud.web.Data;
using white_cloud.web.Services.Tests;
using white_cloud.web.Services.Tests.TestResultComputers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "localhost",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000");
                          builder.WithHeaders().AllowAnyHeader();
                          builder.WithMethods().AllowAnyMethod();
                      });
});

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITestsRepository, FilesTestsRepository>();
builder.Services.AddTransient<ITestService, TestService>();
builder.Services.AddTransient<ITestResultComputer, SumIntervalsTestComputer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseCors("localhost");
app.UseAuthorization();

app.MapControllers();

app.Run();
