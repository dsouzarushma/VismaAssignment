using AutoMapper;
using FakeBloggingSystem.DBConfiguration;
using FakeBloggingSystem.DBContext;
using FakeBloggingSystem.Helper;
using FakeBloggingSystem.Repositories;
using FakeBloggingSystem.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Reflection;

namespace FakeBloggingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            }).AddXmlDataContractSerializerFormatters();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            builder.Services.AddDbContext<BlogDBContext>();
            builder.Services.AddTransient<IPostService,PostService>();
            builder.Services.AddTransient<IPostRepository,PostRepository>();
            builder.Services.AddSingleton<AuthorData>();
            var autoMapper = new MapperConfiguration(item => item.AddProfile(new AutoMapperHandler()));
            IMapper mapper=autoMapper.CreateMapper();
            builder.Services.AddSingleton(mapper);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}