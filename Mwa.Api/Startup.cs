using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Mwa.Api.Security;
using Mwa.Domain.Comand.Handlers;
using Mwa.Domain.Repositories;
using Mwa.Domain.Services;
using Mwa.Infra.Contexts;
using Mwa.Infra.Repositories;
using Mwa.Infra.Services;
using Mwa.Infra.Transactions;
using Mwa.Shared;

namespace Mwa.Api
{
    public class Startup
    {
        private const string ISSUER = "c1f51f42";
        private const string AUDIENCE = "c6bbbb645014";
        private const string SECRET_KEY = "c1f51f42-5727-4d15-b787-c6bbbb645014";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private readonly SymmetricSecurityKey _signinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));
        
        public void ConfigureServices(IServiceCollection services)
        {




            //blinda a api para apenas acesso autorizado
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddCors();
            ///definir um contrato de autorização
            services.AddAuthorization(options =>{
                options.AddPolicy("User", policy => policy.RequireClaim("modernStore", "User"));
                options.AddPolicy("Admin", policy => policy.RequireClaim("modernStore", "Admin"));
            });

            services.Configure<TokenOptions>(options =>{
                options.Issuer = ISSUER;
                options.Audience = AUDIENCE;
                options.SigningCredentials = new SigningCredentials(_signinKey, SecurityAlgorithms.HmacSha256);
            });

             var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = ISSUER,

                ValidateAudience = true,
                ValidAudience = AUDIENCE,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signinKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,


                ClockSkew = TimeSpan.Zero

            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                
               
                options.TokenValidationParameters = tokenValidationParameters;
              
            });


            services.AddScoped<MwaDataContext, MwaDataContext>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICustumerRepository, CustomerRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<CustomerComandHandler, CustomerComandHandler>();
            services.AddTransient<OrdeComandHandler, OrdeComandHandler>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUoW, UoW>();

            Settings.ConnectionString = Configuration.GetConnectionString("CS");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
          

           

            // app.UseJwtBearerAuthentication( new JwtBearerOptions
            // {
            //     AutomaticAuthenticate = true,
            //     AutomaticChallenge = true,
            //     TokenValidationParameters = tokenValidationParameters
            // });
             app.UseAuthentication();

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });
            
            app.UseMvc();

           
        }
    }
}
