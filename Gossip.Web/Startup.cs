using AutoMapper;
using Gossip.Application.Contracts.Chat;
using Gossip.Application.Services.Chat;
using Gossip.Domain.Repositories.Chat;
using Gossip.SQLite;
using Gossip.SQLite.Repositories.Chat;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gossip.Web
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

            services.AddTransient<IChatService, ChatService>();
            services.AddScoped<IChannelRepository, ChannelRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
        
            services.AddDbContext<GossipContext>();

            services.AddAutoMapper();

            services.AddMediatR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
