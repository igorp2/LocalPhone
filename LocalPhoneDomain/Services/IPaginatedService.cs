using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPhoneDomain.Services
{
    public interface IPaginatedService
    {
        public PaginationInformations PaginationInfos { get; }
    }
}
