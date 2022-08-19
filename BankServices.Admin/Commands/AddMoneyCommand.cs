using BankServices.Admin.Models;
using MediatR;
using System;

namespace BankServices.Admin.Commands
{
    public class AddMoneyCommand :  IRequest<ServiceResponse>
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
    }
}
