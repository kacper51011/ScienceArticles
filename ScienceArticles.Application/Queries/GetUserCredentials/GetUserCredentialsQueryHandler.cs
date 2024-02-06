using BCrypt.Net;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ScienceArticles.Application.Settings;
using ScienceArticles.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ScienceArticles.Application.Queries.GetUserCredentials
{
    public class GetUserCredentialsQueryHandler : IRequestHandler<GetUserCredentialsQuery, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOptions<JWTSettings> _jwtSettings;

        public GetUserCredentialsQueryHandler(IUserRepository userRepository, IOptions<JWTSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings;
        }

        public async Task<string> Handle(GetUserCredentialsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByUsernameAsync(request.dto.Login);
                if (user == null)
                {
                    throw new ArgumentException("Username or password wasn`t correct");
                }
                else
                {
                    var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.dto.Password, user.Password);
                    if (!isPasswordValid)
                    {
                        throw new ArgumentException("Username or password wasn`t correct");
                    }

                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.SigningKey));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var securityToken = new JwtSecurityToken(_jwtSettings.Value.Issuer,
                        _jwtSettings.Value.Audience,
                        null,
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: credentials);

                    var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
                    return token;

                }

                

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
