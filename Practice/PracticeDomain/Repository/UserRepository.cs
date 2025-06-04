using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PracticeDomain.DTOs.JWT;
using PracticeDomain.DTOs.UserDto;
using PracticeDomain.EmailService;
using PracticeDomain.IRepository;
using PracticeModel.DataModel;
using PracticeModel.DBcontext;
using PracticeModel.Models.AuthenticationModel;
using PracticeModel.SeedingModels;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;


namespace PacticeService.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BaseContext _context;
      private readonly IEmailService _emailService;
        private readonly JWT _jwt;




        public UserRepository(UserManager<ApplicationUser> userManager, BaseContext context, IEmailService emailService, RoleManager<IdentityRole>  roleManager, IOptions<JWT> jwt
            )  
        {
            _userManager = userManager;
            _context= context;
            _emailService = emailService;
            _roleManager = roleManager;
            _jwt = jwt.Value;


        }
        public async Task RegisterUser(DTOs.UserDto.SignupRequest request) 
        
        {
            if (request is not null)
            {
                var check = await _context.Users.FindAsync(request.Email);
                if(check != null) {
                    throw new Exception("Email Already Exist");
                }
                if (request.Password != request.ConfirmPassword)
                {
                    throw new Exception("Password and Confirm Password do not match.");
                }
                var user = new ApplicationUser
                {

                    FirstName=request.FirstName,
                    LastName=request.LastName,
                    Email=request.Email,
                    UserName=request.Username
                };

              var result=  await _userManager.CreateAsync(user,request.Password);
                if (!result.Succeeded)
                {
                    throw new Exception("Not Created User");
                }
                await _userManager.AddToRoleAsync(user, SeedModel.RoleSeed.Admin);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedUserId = WebUtility.UrlEncode(user.Id);
                var encodedToken = Uri.EscapeDataString(token);

                // Construct confirmation link
                var confirmationLink = $"https://localhost:44352/api/User/ConfirmEmail?userId={encodedUserId}&code={encodedToken}";

                // Send confirmation email
                var subject = "Confirm your email";
                var body = $"<p>Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.</p>";
                await _emailService.SendEmailAsync(user.Email, subject, body);
            }

        }

        public async Task<AuthenticationModel> Login(LoginRequest request)
{

            var authenticationModel = new AuthenticationModel();

            if (request is not null)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if(user is not null)
                {
                    if( await _userManager.CheckPasswordAsync(user, request.Password))

                    {
                        authenticationModel.IsAuthenticated = true;
                        authenticationModel.Message = $"Successfully Login.";
                        JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                        authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                        authenticationModel.Email = user?.Email;
                        authenticationModel.UserName = user?.UserName;
                        var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                        authenticationModel.Roles = rolesList.ToList();
                        return authenticationModel;
                    }
                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
                    return authenticationModel;
                }


            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Request is null";
            return authenticationModel;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
                {
                    var userClaims = await _userManager.GetClaimsAsync(user);
                    var roles = await _userManager.GetRolesAsync(user);

                    var roleClaims = new List<Claim>();

                    for (int i = 0; i < roles.Count; i++)
                    {
                        roleClaims.Add(new Claim("roles", roles[i]));
                    }

                    var claims = new[]
                    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("uid", user.Id)
    }
                    .Union(userClaims)
                    .Union(roleClaims);

                    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
                    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                    var jwtSecurityToken = new JwtSecurityToken(
                        issuer: _jwt.Issuer,
                        audience: _jwt.Audience,
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                        signingCredentials: signingCredentials);
                    return jwtSecurityToken;
                }

            
   
            public async Task ConfirmEmailAsync(string Userid,string code)
        {

            var decodedUserId = Uri.UnescapeDataString(Userid);
            var user = await _userManager.Users.Where(x=> x.Id== decodedUserId && !x.EmailConfirmed).FirstOrDefaultAsync();
            if(user != null)
            {
                var token = Uri.UnescapeDataString(code);
              var result=  await _userManager.ConfirmEmailAsync(user, token);
                if (result != null)
                {
                    throw new Exception($"Email Confirmed Successfully { user.Email }");
                }
                throw new Exception("Email not confirmed ");
            }
            throw new Exception($"Error Occured while confirming { user.Email }");
        }
        }

    }

