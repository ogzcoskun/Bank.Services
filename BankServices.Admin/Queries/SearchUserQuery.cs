using BankServices.Admin.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankServices.Admin.Queries
{
    public class SearchUserQuery :  IRequest<List<UserModel>>
    {
        public string Key { get; set; }
    }
}
