using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publisher.Infrastructure.Messaging;
using Publisher.Infrastructure.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace Publisher.WebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Publisher Sample", Version = "v1" });
            });
            string publisherserviceBusconnection = Configuration["AppSettings:PublisherServiceBusconnection"];
            string publisherTopicName = Configuration["AppSettings:PublisherTopicName"];
            var topicClient = new TopicClient(publisherserviceBusconnection, publisherTopicName, RetryPolicy.Default);
            services.AddSingleton<IMessagingClient>(new AzureTopicClient(topicClient));
            services.AddTransient<IUsersCommandService, UsersCommandService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Publisher Sample");
            });
           
        }
    }
}
