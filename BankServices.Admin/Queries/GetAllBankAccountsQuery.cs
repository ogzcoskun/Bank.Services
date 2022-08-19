using BankServices.Admin.Data;
using BankServices.Admin.Models;
using MediatR;
using System.Collections.Generic;

namespace BankServices.Admin.Queries
{
    public class GetAllBankAccountsQuery : IRequest<List<BankAccounts>>
    {

    }
}
