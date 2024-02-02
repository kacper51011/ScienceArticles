using EuropePMCServiceConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Infrastructure.Interfaces
{
    public interface IEuropePMCService
    {
        public Task<searchPublicationsResponse> SearchPublicationsAsync();
        public Task<getFulltextXMLResponse> FulltextXMLAsync();

    }
}
