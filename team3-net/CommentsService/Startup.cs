using CommentsService.Entities;
using CommentsService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CommentsService
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
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            }
          );
            services.AddSwaggerGen();


            services.AddDbContext<CommentsDbContext>(
                  options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

            services.AddScoped<IRepository<Item>, Repository<Item>>();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CommentsService v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
