using Foosball.Microservice.DomainLogic.AggregateRoot;
using Foosball.Microservice.Infrastructure.Commands.Abstraction;
using Foosball.Microservice.Infrastructure.Commands.Implementation;
using Foosball.Microservice.Infrastructure.Data;
using Foosball.Microservice.Infrastructure.Queries.Abstraction;
using Foosball.Microservice.Infrastructure.Queries.Implementation;
using Foosball.Microservice.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;

namespace Foosball.Microservice.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

            var dbConnectionString = Configuration.GetConnectionString("Database");
            services.AddDbContext<FoosballDbContext>(builder => builder.UseSqlServer(dbConnectionString));
            services.AddTransient<IDbConnection, SqlConnection>(_ => new SqlConnection(dbConnectionString));
            services.AddTransient<IRepository<Game>, GameRepository>();
            services.AddTransient<ICreateGameCommand, CreateGameCommand>();
            services.AddTransient<IScoreGoalCommand, ScoreGoalCommand>();
            services.AddTransient<IGetGameDetailsQuery, GetGameDetailsQuery>();
            services.AddTransient<IListGamesQuery, ListGamesQuery>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Foosball API");
            });

            InitializeDatabase(app);
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetRequiredService<FoosballDbContext>().Database.Migrate();
        }
    }
}
