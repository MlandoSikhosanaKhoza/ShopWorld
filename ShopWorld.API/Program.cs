using ShopWorld.BusinessLogic;
using ShopWorld.DAL;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")??"";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
#region Required Dependancy Injection
builder.Services.AddRepository(connectionString).AddBusinessLogic();

//The two below will move to Api
builder.Services.AddJwtToken(builder.Configuration["JWT:ValidAudience"]??"", builder.Configuration["JWT:ValidIssuer"]??"", builder.Configuration["JWT:Secret"]??"");
BusinessLogicExtensions.AddCors(builder.Services);

//Set you serialization options
builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
#endregion Required Dependancy Injection
var app = builder.Build();

//Seeding Data
app.Services.CreateScope().ServiceProvider.InitializeData();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
