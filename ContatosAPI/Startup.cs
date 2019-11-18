using ContatosAPI.Models;
using ContatosAPI.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ContatosAPI {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            // para banco de dados in-memory
            services.AddDbContext<ContatosContext>(opt =>
                opt.UseInMemoryDatabase("Contatos"));

            // para SQL Server
            //services.AddDbContext<ContatosContext>(opt =>
            //opt.UseSqlServer(Configuration.GetConnectionString("Connection1")));

            // Serviços síncronos e assíncronos
            services.AddTransient<IContatosRepository, ContatosRepository>();
            services.AddTransient<IContatosRepositoryAsync, ContatosRepositoryAsync>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            /*  Diferença entre Singleton, Scoped e Transient
                https://stackoverflow.com/questions/38138100/addtransient-addscoped-and-addsingleton-services-differences
                Singleton which creates a single instance throughout the application. It creates the instance for the first time and reuses the same object in the all calls.
                Scoped lifetime services are created once per request within the scope. It is equivalent to Singleton in the current scope. 
                eg. in MVC it creates 1 instance per each http request but uses the same instance in the other calls within the same web request.
                Transient lifetime services are created each time they are requested. This lifetime works best for lightweight, stateless services.
             */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
