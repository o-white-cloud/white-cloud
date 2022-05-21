using System.Text.Json.Serialization;
using white_cloud.identity;
using white_cloud.web.Models.Settings;
using white_cloud.web.Services;
using white_cloud.web.Services.Tests;
using white_cloud.web.Services.Tests.TestResultComputers;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: "localhost",
//                      builder =>
//                      {
//                          builder.WithOrigins("http://localhost:3000");
//                          builder.WithHeaders().AllowAnyHeader();
//                          builder.WithMethods().AllowAnyMethod();
//                      });
//});

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddOidcIdentityProvidersInfo(builder.Configuration);
builder.Services.AddIdentity(builder.Configuration);
builder.Services.AddSingleton(builder.Configuration.GetSection("Email").Get<EmailSettings>());


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromFile("white-cloud-web-api-2a69b8767db0.json").CreateWithUser("office@white-cloud.ro").CreateScoped(Google.Apis.Gmail.v1.GmailService.Scope.GmailSend);
builder.Services.AddSingleton(credential);
builder.Services.AddHttpClient();
builder.Services.AddTransient<IEmailService, GmailService>();
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

app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.Lax
});
//app.UseCors("localhost");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


var identitySeeder = app.Services.GetService<IdentitySeeder>();
if (identitySeeder != null)
{
    await identitySeeder.Seed();
}

app.Run();
