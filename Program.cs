using System.Text;
using Microsoft.EntityFrameworkCore;
using api_gerenciamento_tarefas.Application.Common;
using api_gerenciamento_tarefas.Application.Features.Projects.Interfaces;
using api_gerenciamento_tarefas.Data;
using api_gerenciamento_tarefas.Application.Interfaces;
using api_gerenciamento_tarefas.Infraestructure.Data;
using api_gerenciamento_tarefas.Application.Features.Projects.Services;
using api_gerenciamento_tarefas.Application.Features.Users.Interfaces;
using FluentValidation.AspNetCore;
using api_gerenciamento_tarefas.Infraestructure.Repositories;
using api_gerenciamento_tarefas.Application.Features.Users.Services;
using api_gerenciamento_tarefas.Application.Features.Tasks.Interfaces;
using api_gerenciamento_tarefas.Application.Features.Tasks.Services;
using api_gerenciamento_tarefas.Application.Features.SubTasks.Interfaces;
using api_gerenciamento_tarefas.Application.Features.SubTasks.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
// using Microsoft.OpenApi.Models; // removed to avoid compile-time dependency issues

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddFluentValidationAutoValidation();

var jwtSettings = new JwtSettings();
builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);

var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

// configurações do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Configure Swagger security via reflection to avoid direct compile-time
    // dependency on Microsoft.OpenApi.Models (some environments may not resolve it).
    try
    {
        var asmName = "Microsoft.OpenApi";
        var schemeType = Type.GetType("Microsoft.OpenApi.Models.OpenApiSecurityScheme, " + asmName);
        var requirementType = Type.GetType("Microsoft.OpenApi.Models.OpenApiSecurityRequirement, " + asmName);
        var referenceType = Type.GetType("Microsoft.OpenApi.Models.OpenApiReference, " + asmName);
        var referenceEnumType = Type.GetType("Microsoft.OpenApi.Models.ReferenceType, " + asmName);
        var parameterLocationType = Type.GetType("Microsoft.OpenApi.Models.ParameterLocation, " + asmName);
        var securitySchemeTypeEnum = Type.GetType("Microsoft.OpenApi.Models.SecuritySchemeType, " + asmName);

        if (schemeType != null && requirementType != null && referenceType != null && referenceEnumType != null)
        {
            var scheme = Activator.CreateInstance(schemeType);
            schemeType.GetProperty("Description")?.SetValue(scheme, "Use 'Bearer {token}'");
            schemeType.GetProperty("Name")?.SetValue(scheme, "Authorization");
            if (parameterLocationType != null)
            {
                var headerVal = Enum.Parse(parameterLocationType, "Header");
                schemeType.GetProperty("In")?.SetValue(scheme, headerVal);
            }
            if (securitySchemeTypeEnum != null)
            {
                var apiKeyVal = Enum.Parse(securitySchemeTypeEnum, "ApiKey");
                schemeType.GetProperty("Type")?.SetValue(scheme, apiKeyVal);
            }
            schemeType.GetProperty("Scheme")?.SetValue(scheme, "Bearer");

            // call AddSecurityDefinition(string, OpenApiSecurityScheme)
            var addSecDef = options.GetType().GetMethod("AddSecurityDefinition");
            addSecDef?.Invoke(options, new object[] { "Bearer", scheme });

            // create reference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            var reference = Activator.CreateInstance(referenceType);
            var refSecurityScheme = Enum.Parse(referenceEnumType, "SecurityScheme");
            referenceType.GetProperty("Type")?.SetValue(reference, refSecurityScheme);
            referenceType.GetProperty("Id")?.SetValue(reference, "Bearer");

            // create a scheme with Reference set
            var schemeWithRef = Activator.CreateInstance(schemeType);
            schemeType.GetProperty("Reference")?.SetValue(schemeWithRef, reference);

            // create OpenApiSecurityRequirement and add (schemeWithRef, new List<string>())
            var requirement = Activator.CreateInstance(requirementType);
            var addMethod = requirementType.GetMethod("Add", new Type[] { schemeType, typeof(IList<string>) });
            if (addMethod != null)
            {
                addMethod.Invoke(requirement, new object[] { schemeWithRef, new List<string>() });
            }

            var addSecReq = options.GetType().GetMethod("AddSecurityRequirement");
            addSecReq?.Invoke(options, new object[] { requirement });
        }
    }
    catch
    {
        // if reflection setup fails, let Swagger run without the security definition
    }
});

// Repositórios
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskItemRepository, TaskRepository>();
builder.Services.AddScoped<ISubtaskRepository, SubtaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();
builder.Services.AddScoped<SubTaskService>();
builder.Services.AddScoped<UsersService>();

// Controller
builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();