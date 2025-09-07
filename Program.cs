using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog.Web;
using TestAPI.Data;
using TestAPI.Entities;
using TestAPI.Entities.Journals;
using TestAPI.Handlers;
using TestAPI.Repositories;
using TestAPI.Services.Journals;
using TestAPI.Services.Trees;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext with PostgreSQL connection
builder.Services.AddDbContext<TreeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

// Add services to the container.
builder.Services.AddTransient<ITreeNodeService, TreeNodeService>();
builder.Services.AddTransient<IJournalService, JournalService>();

builder.Services.AddTransient<IBaseRepository<Tree>, BaseRepository<Tree>>();
builder.Services.AddTransient<IBaseRepository<TreeNode>, BaseRepository<TreeNode>>();
builder.Services.AddTransient<IBaseRepository<Journal>, BaseRepository<Journal>>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger", Version = "0.0.1" });
    
    options.TagActionsBy(api =>
    {
        if (api.GroupName != null)
            return [api.GroupName];

        var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
        if (controllerActionDescriptor != null)
            return [controllerActionDescriptor.ControllerName];
        
        return ["default"];
    });

    options.DocInclusionPredicate((name, api) => true);
});

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
    options.AppendTrailingSlash = true;
});

builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();