using MediatR;
using ScienceArticles.Application.Dtos.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Commands.RegisterUser
{
    public record RegisterUserCommand(RegisterRequestDto dto): IRequest
    {
    }
}
