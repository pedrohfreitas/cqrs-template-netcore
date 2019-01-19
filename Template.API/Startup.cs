using System.IO;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Template.Domain.AutoMapper;
using Template.Domain.TemplateContext.Handlers;
using Template.Domain.TemplateContext.Repositories;
using Template.Infra.TemplateContext.Repositories;
using Template.Shared;
using Template.Shared.Security;

namespace Template.API
{
    public class Startup
    {
        private const string ISSUER_DEV = "d6asd432hj";
        private const string AUDIENCE_DEV = "31289132jk";
        private const string SECRET_KEY_DEV = "C3C1CFEF-C680-478E-A9BC-6B2BA2C7588A";

        private const string ISSUER_QA = "udda872jd1";
        private const string AUDIENCE_QA = "890daski81";
        private const string SECRET_KEY_QA = "C1C3F1C4-2067-4C17-B0A2-3A681F9D35EB";

        private const string ISSUER_PROD = "kdjd91210";
        private const string AUDIENCE_PROD = "kdiao1132j";
        private const string SECRET_KEY_PROD = "C1C3F1C4-2067-4C17-B0A2-3A681F9D35EB";

        private IHostingEnvironment enviroment;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();

            enviroment = env;

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConnectionStrings.TemplateConnection = Configuration.GetConnectionString("GempConnection");

            if (enviroment.IsStaging() || enviroment.IsProduction())
            {
                services.AddMvc(config =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                                     .RequireAuthenticatedUser()
                                     .Build();
                    config.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });
            }
            else
            {
                services.AddMvc().AddJsonOptions(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });
            }

            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(enviroment.IsProduction() ? SECRET_KEY_PROD : enviroment.IsStaging() ? SECRET_KEY_QA : SECRET_KEY_DEV));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.TokenValidationParameters =
                           new TokenValidationParameters
                           {
                               ValidateIssuer = true,
                               ValidateAudience = true,
                               ValidateLifetime = true,
                               ValidateIssuerSigningKey = true,

                               ValidIssuer = enviroment.IsProduction() ? ISSUER_PROD : enviroment.IsStaging() ? ISSUER_QA : ISSUER_DEV,
                               ValidAudience = enviroment.IsProduction() ? AUDIENCE_PROD : enviroment.IsStaging() ? AUDIENCE_QA : AUDIENCE_DEV,
                               IssuerSigningKey = _signingKey
                           };
                  });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Gerente", policy => policy.RequireClaim("Perfil", "Gerente"));
                options.AddPolicy("RH", policy => policy.RequireClaim("Perfil", "RH"));
                options.AddPolicy("Secretaria", policy => policy.RequireClaim("Perfil", "Secretaria"));
                options.AddPolicy("Cliente", policy => policy.RequireClaim("Perfil", "Cliente"));
                options.AddPolicy("Colaborador", policy => policy.RequireClaim("Perfil", "Colaborador"));
            });

            services.Configure<TokenOptions>(options =>
            {
                options.Issuer = enviroment.IsProduction() ? ISSUER_PROD : enviroment.IsStaging() ? ISSUER_QA : ISSUER_DEV ;
                options.Audience = enviroment.IsProduction() ? AUDIENCE_PROD : enviroment.IsStaging() ? AUDIENCE_QA : AUDIENCE_DEV;
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAutoMapper();
            AutoMapperConfiguration.RegisterMappings();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = $"Gemp API - {enviroment.EnvironmentName}", Version = "v1" });
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Swagger.xml");
                c.IncludeXmlComments(filePath);
            });


            services.AddCors(o => o.AddPolicy("Cors", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Template API");
            });

            app.UseAuthentication();

            app.UseSwagger();

            app.UseMvc();
        }

        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IAuthenticateRepository, AuthenticateRepository>();
            services.AddTransient<AuthenticateUserHandler, AuthenticateUserHandler>();
            services.AddTransient<ICargoRepository, CargoRepository>();
            services.AddTransient<CargoHandler, CargoHandler>();
            services.AddTransient<IFuncionarioRepository, FuncionarioRepository>();
            services.AddTransient<FuncionarioHandler, FuncionarioHandler>();
        }
    }
}
