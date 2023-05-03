<h1 align="center">Project applying onion architecture</h1>

:construction: Project in construction :construction:

**#Steps to start the project 📋**

First install with the manage packages install the following plugins:
* MediatR - Install this plugin in the WebAPI and Application layer.
* AutoMapper.Extensions.Microsoft.DependencyInjection | AutoMapper - Installing these plugins in the core/application layer will allow us to do object mapping.
* FluentValidation - Install this nuget in the core/application layer, this nuget will allow us to do strongly typed validations.
* FluentValidation.DependencyInjectionExtensions - Install this nuget in the core/application layer 
* EntityFrameworkCore - Installed in the persistence layer with nuggets this will help us to have an ORM with some database.
* Npgsql.EntityFrameworkCore.PostgreSQL - Installed in the persistence and Identity layer with nuggets this will be used to handle queries, relations and connection with postgresql.
* Microsoft.Extensions.Options.ConfigurationExtensions - Install on the application and Shared layer.
* Ardalis.Specification - This nugget will be installed in the application layer and will allow us to manage the Repository Pattern that helps us to manage the management with the database through a generic repository.
* Ardalis.Specification.EntityFrameworkCore- This nugget will be installed in the Persistence layer and will allow us to manage the Repository Pattern which helps us to manage the database management through a generic repository.
* Microsoft.EntityFrameworkCore.Tools - We install this nugget in the WepApi layer which will help us for the migration with the DB.
* Microsoft.Extensions.Caching.StackExchangeRedis - We install this nugget in the persistence layer 
* Microsoft.AspNetCore.Authentication.JwtBearer - Installed with the nuget package in the Identity layer this will help with JWT key management.
* Microsoft.AspNetCore.Identity.EntityFrameworkCore - Installed in the Identity layer for entity creation and management.
* Microsoft.EntityFrameworkCore - Installed in the Identity layer for entity creation and management.
* Microsoft.EntityFrameworkCore.Design - Installed in the Identity layer for entity creation and management.
* Microsoft.VisualStudio.Web.CodeGeneration.Design - Installed in the Identity layer for entity creation and management.
* System.IdentityModel.Tokens.Jwt - Installed in the Identity layer for entity creation and management.

**#Steps to start the migrations BD ⌨**

For migrations we must open the package manager console and set Persistence as the default project.
we place the following command in the console:

**add-migration MyFirstMigration -o Migrations**

This will generate a configuration class for our database to create the tables based on the relationships and configurations that we placed in the Configuration folder of the persistence layer.

If everything is correct we place the following command:

**update-database**

This to execute the configuration class in the database.

**add-migration InitialMigration -Context IdentityContext**

This command will generate the migration of the Identity classes to the database, unlike the first one the -Context is specified because it has two contexts in this case we rely on IdentityContext.

**update-database -Context IdentityContext**

If everything went well in the first migration then this second command is executed to carry out the migration to the database, unlike the first command, in this one the context is specified since there are two
