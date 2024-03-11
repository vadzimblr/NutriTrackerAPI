using Controllers;
using Controllers.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NutriTrackerAPI.Extensions;

NewtonsoftJsonPatchInputFormatter getNewtonSoftInputFormatter()
{
    var inputFormater = new ServiceCollection().AddLogging().AddMvc()
        .AddNewtonsoftJson().Services.BuildServiceProvider().GetRequiredService<IOptions<MvcOptions>>()
        .Value.InputFormatters.OfType<NewtonsoftJsonPatchInputFormatter>().First();
    return inputFormater;
}
var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureMapper();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.AddAuthentication();
builder.Services.ConfigureJwtAuthentication(builder.Configuration);
//builder.Services.ConfigureAuthPolicy();
builder.Services.AddControllers().AddApplicationPart(typeof(AssemblyReference).Assembly);
builder.Services.AddControllers(opt =>
{
    opt.InputFormatters.Insert(0, getNewtonSoftInputFormatter());
    
});
builder.Services.AddScoped<ProductEditPermissionFilter>();
builder.Services.AddScoped<ValidationFilter>();
builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.ConfigureExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.MapControllers();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.Run();