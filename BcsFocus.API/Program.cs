using BcsFocus.API.Models;
using BcsFocus.API.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<BcsStoreDbSettings>(
    builder.Configuration.GetSection(nameof(BcsStoreDbSettings))
);

builder.Services.AddSingleton<IBcsStoreDbSettings>(
    sp => sp.GetRequiredService<IOptions<BcsStoreDbSettings>>().Value
);

builder.Services.AddSingleton<IMongoClient>(
    s => new MongoClient(builder.Configuration.GetValue<string>("BcsStoreDbSettings:ConnectionString"))
);

builder.Services.AddScoped<IModuleService, ModuleService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    }        
);
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();