using MediatR;
using ScienceArticles.Application.Dtos.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Queries.GetUserCredentials
{
    public record GetUserCredentialsQuery(LoginRequestDto dto): IRequest<string>;

}
