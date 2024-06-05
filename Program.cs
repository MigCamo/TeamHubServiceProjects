using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TeamHubServiceProjects.DTOs;
using TeamHubServiceProjects.Entities;
using TeamHubServiceProjects.Gateways.Interfaces;
using TeamHubServiceProjects.Gateways.Providers;
using TeamHubServiceProjects.UseCases.Interfaces;
using TeamHubServiceProjects.UseCases.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*
builder.Services.AddAuthentication(options => {    
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;    
}).AddJwtBearer(options => {
    var config = builder.Configuration;
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    var llave = config["JWTSettings:Key"];
    options.TokenValidationParameters = new TokenValidationParameters() {
        ValidIssuer = config["JWTSettings:Issuer"],
        ValidAudience = config["JWTSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(llave)),        
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
*/

builder.Services.AddScoped<IProjectManagement, ProjectManagement>();
builder.Services.AddScoped<IProjectServices, ProjectServices>();
builder.Services.AddDbContext<TeamHubContext>(options => {
    var connectionString = builder.Configuration
                           .GetConnectionString("MySQLCursos")?? "DefaultConnectionString";
    options.UseMySQL(connectionString);
});

//builder.Services.AddAuthorization();
/*
builder.Services.AddAuthorizationBuilder()
  .AddPolicy("usuario_valido", policy =>
        policy
            .RequireRole("Administrador")
            .RequireClaim("scope", "CursosAPP"));
builder.Services.AddCors();
*/

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors();
//app.UseAuthentication();
//app.UseAuthorization();

app.MapGet("/TeamHub/Projects/MyProjects/{studentID}", (IProjectManagement projectManagement, int studentID) =>
{
        return projectManagement.GetAllProjectsByStuden(studentID);
})
.WithName("GetListaProyectos")
//.RequireAuthorization("usuario_valido")
.WithOpenApi();

app.MapGet("/TeamHub/Projects/{idProject}", (IProjectManagement projectManagement, int idProject) =>
{
        return projectManagement.GetProjectByID(idProject);
})
.WithName("ObtenerProyecto")
//.RequireAuthorization("usuario_valido")
.WithOpenApi();


app.MapPost("/TeamHub/Projects/AddProject", (IProjectManagement projectManagement, AddProjectRequestDTO request) =>
{
    return projectManagement.AddProject(request.ProjectNew, request.StudentID);
})
.WithName("AgregarProyecto")
//.RequireAuthorization("usuario_valido")
.WithOpenApi();

app.MapPost("/TeamHub/Projects/UpdateProject", (IProjectManagement projectManagement, project projectUpdate) =>
{
    return projectManagement.UpdateProject(projectUpdate);
})
.WithName("UpdateProyect")
//.RequireAuthorization("usuario_valido")
.WithOpenApi();

app.MapDelete("/Projects", (IProjectManagement projectManagement, int idProject) =>
{
    return projectManagement.RemoveProject(idProject);
})
.WithName("DeleteProject")
//.RequireAuthorization("usuario_valido")
.WithOpenApi();

app.MapGet("/TeamHub/Projects/Project/{idProject}", (IProjectManagement projectManagement, int idProject) =>
{
    return projectManagement.GetProject(idProject);
})
.WithName("GetProject")
//.RequireAuthorization("usuario_valido")
.WithOpenApi();

app.MapGet("/TeamHub/Projects/Project/Tasks/{idProject}", (IProjectManagement projectManagement, int idProject) =>
{
    return projectManagement.GetTasksByProject(idProject);
})
.WithName("GetProjectTasks")
//.RequireAuthorization("usuario_valido")
.WithOpenApi();

 
app.Run();

