using BCrypt.Net;
using MediatR;
using ScienceArticles.Application.Exceptions;
using ScienceArticles.Domain.Aggregates;
using ScienceArticles.Domain.Interfaces;

namespace ScienceArticles.Application.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;

        }
        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.dto.Password != request.dto.ConfirmPassword)
                {
                    new ArgumentNotValidException("Passwords are not the same!");
                }

                var userWithSameName = await _userRepository.GetUserByUsernameAsync(request.dto.UserName);
                if (userWithSameName != null)
                {
                    new ArgumentNotValidException("User with that username already exists");
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.dto.Password);

                var newUser = User.Create(request.dto.UserName, hashedPassword);

                await _userRepository.RegisterUserAsync(newUser);
                await _unitOfWork.SaveChangesAsync();

                return;

            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
