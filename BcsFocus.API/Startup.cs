using BcsFocus.API.Services;
using MongoDB.Driver;

namespace BcsFocus.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(
                s => new MongoClient(Configuration["BcsStoreDbSettings:ConnectionString"])
            );

            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ITopicService, TopicService>();

            services.AddControllers();

            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(MappingProfile));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}