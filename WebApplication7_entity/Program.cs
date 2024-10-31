
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication7_petPals;
using WebApplication7_petPals.Mapping;
using WebApplication7_petPals.Midlware;
using WebApplication7_petPals.Services.Cart;
using WebApplication7_petPals.Services.Catogory;
using WebApplication7_petPals.Services.JWT_id;
using WebApplication7_petPals.Services.Login;
using WebApplication7_petPals.Services.Orders;
using WebApplication7_petPals.Services.Products;
using WebApplication7_petPals.Services.Register;
using WebApplication7_petPals.Services.Users;
using WebApplication7_petPals.Services.Wishlist;

namespace WebApplication7_entity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddScoped<AppDbContext>();
            builder.Services.AddAutoMapper(typeof(AppMapper));
            builder.Services.AddScoped<IUserInterface, UserService>();
            builder.Services.AddScoped<IRegisterInterface, RegisterService>();
            builder.Services.AddScoped<ILoginInterface, LoginService>();
            builder.Services.AddScoped<ICatogryInterface,CatogoryServices>();
            builder.Services.AddScoped<IProductInterface, ProductService>();
            builder.Services.AddScoped<ICartInterface,CartServices>();
            builder.Services.AddScoped<IJwtIdInterface, JwtIdService>();
            builder.Services.AddScoped<IOrderInterface,OrderService>();
            builder.Services.AddScoped<IWishlistInterface, WishlistService>();



            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"])),
                    ValidateLifetime = true, // Set this to true to validate token expiration
                    ValidateIssuer = false, // You can set this to true if you are validating the issuer
                    ValidateAudience = false // Set to true if you are validating the audience
                };
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())    
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<JwtMidlware>();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
