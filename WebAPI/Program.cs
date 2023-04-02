using Application;
using Persistence;
using Persistence.Contexts;
using Shared;
using System.Reflection;
using WebAPI.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
//We call our registration system where the configurations for the controllers are located.
builder.Services.AddApiVersioningExtension();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//The Application AddApplicationLayer layer reference is called
builder.Services.AddApplicationLayer();
//We register or configure all the services that will use a connection to the database, in case they will use some kind of call from appsettings.json
//We inject the class that calls persistence and we send it the appsettings.json configuration
builder.Services.AddSharedInfraestructure(builder.Configuration);
//We inject the class that calls persistence and we send it the appsettings.json configuration
builder.Services.AddPersistenceInfraestructure(builder.Configuration);

var app = builder.Build();
//
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//We call UseErrorHandlingMiddleware that we created to customize the API type errors from
//the program, since this is where the API is started
app.UseErrorHandlingMiddleware();

app.MapControllers();

app.Run();
