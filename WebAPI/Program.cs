using Application;
using Identity;
using Identity.Model;
using Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using Persistence;
using Shared;
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
//Inject the JWT authentication class configuration and JWT configuration in Identity
builder.Services.AddIdentityInfraestructure(builder.Configuration);

var app = builder.Build();
//Insert and register User Roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        //Create automatic user and roles in the database
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        //The role and type of user are configured in the database.
        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultAdminUser.SeedAsync(userManager, roleManager);
        await DefaultBasicUser.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {

        throw ex;
    }
}

//configure connection DataBase
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//We added to make it possible to authenticate through a JWT for each controller
app.UseAuthentication();
app.UseAuthorization();
//We call UseErrorHandlingMiddleware that we created to customize the API type errors from
//the program, since this is where the API is started
app.UseErrorHandlingMiddleware();

app.MapControllers();

app.Run();
