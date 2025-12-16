using LinkDev.Talabat.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Application.Services.Auth;
using LinkDev.Talabat.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;

                //opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                opt.SignIn.RequireConfirmedPhoneNumber = true;
                opt.SignIn.RequireConfirmedEmail = true;
                opt.SignIn.RequireConfirmedAccount = true;

                /// opt.Password.RequireDigit = true;
                /// opt.Password.RequireLowercase = true;
                /// opt.Password.RequireUppercase = true;
                /// //opt.Password.RequireNonAlphanumeric = true; // $@#%
                /// opt.Password.RequiredUniqueChars = 1;
                /// opt.Password.RequiredLength = 6;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 10; // by default is 5
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10); // by default is 5 minutes

                #region 
                //opt.Stores.ProtectPersonalData = true;
                //opt.Stores.SchemaVersion = ;
                //opt.Stores.MaxLengthForKeys = 128;

                //opt.ClaimsIdentity.EmailClaimType = "email";
                //opt.ClaimsIdentity.UserIdClaimType = "uid";
                //opt.ClaimsIdentity.UserNameClaimType = "username";
                //opt.ClaimsIdentity.RoleClaimType = "role";

                //opt.Tokens.AuthenticatorIssuer = "LinkDev.Talabat";
                //opt.Tokens.ChangeEmailTokenProvider = TokenOptions.DefaultEmailProvider;
                //opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                //opt.Tokens.ChangePhoneNumberTokenProvider = TokenOptions.DefaultEmailProvider; 
                #endregion

            }).AddEntityFrameworkStores<StoreIdentityDbContext>();

            services.AddScoped<IAuthService, AuthService>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));


            services.AddScoped(typeof(Func<IAuthService>), (serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<IAuthService>(); // I think here we still need to userManager or no , i don't know
            });


            //services.AddAuthentication();
            //services.AddAuthentication("Hamada");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };

            });

            return services;
        }
    }
}
