using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WebApplication1.Models;
using WebApplication1.Services;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;


namespace WebApplication1
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Swagger document generation services
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basic crud operation", Version = "v1" });
            });

            var mongoDbSettings = _configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoDbSettings.DatabaseName);
            services.AddSingleton(database);


            services.AddScoped<UserService>();

            services.AddAuthorization();

            services.AddControllers();

            //  cors services adding
            services.AddCors();

            // if you want to send  a data from xml 
            //  services.AddControllers(x=>x.RespectBrowserAcceptHeader=true).AddXmlDataContractSerializerFormatters();
           //  options.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
           //  options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            // Other configurations...
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", " API Name v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();


            //  end point cors error handling                                           
            app.UseCors(x => x.WithOrigins("http://localhost:4200","http://example.com").AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowedToAllowWildcardSubdomains());

            // Allow controllers to handle requests
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            // This is where you would use app.Run() 
            // without app.run it will running
            app.Run(async context =>
            {
                // Handle the request and produce a response
                await context.Response.WriteAsync("Hello from the end of the pipeline!");
            });

        }
    }
}
