<h1 align="center">Project applying onion architecture</h1>

:construction: Project in construction :construction:
</h4>
### Steps to start the project 📋

First install with the manage packages install the following plugins:
* MediatR - Install this plugin in the WebAPI.
* MediatR.ExtensionMicrosoft.DependencyInjection - Install this plugin in core/application layer.
* AutoMapper.Extensions.Microsoft.DependencyInjection | AutoMapper - Installing these plugins in the core/application layer will allow us to do object mapping.
* FluentValidation - Install this nuget in the core/application layer, this nuget will allow us to do strongly typed validations.
* FluentValidation.DependencyInjectionExtensions - Install this nuget in the core/application layer 
* EntityFrameworkCore - Installed in the persistence layer with nuggets this will help us to have an ORM with some database.
* Npgsql.EntityFrameworkCore.PostgreSQL - Installed in the persistence layer with nuggets this will be used to handle queries, relations and connection with postgresql.
* Microsoft.Extensions.Options.ConfigurationExtensions - Install on the application layer.
* Ardalis.Specification - This nugget will be installed in the application layer and will allow us to manage the Repository Pattern that helps us to manage the management with the database through a generic repository.
* Ardalis.Specification.EntityFrameworkCore- This nugget will be installed in the Persistence layer and will allow us to manage the Repository Pattern which helps us to manage the database management through a generic repository.