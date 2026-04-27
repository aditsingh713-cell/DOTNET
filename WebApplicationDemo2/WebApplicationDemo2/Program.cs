
using Microsoft.EntityFrameworkCore;
using WebApplicationDemo2.Models;

namespace WebApplicationDemo2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<MovieContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllers();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            

           


            app.MapControllers();

            app.Run();
        }
    }
}
