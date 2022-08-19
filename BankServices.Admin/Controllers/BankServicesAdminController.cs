using BankServices.Admin.Commands;
using BankServices.Admin.Models;
using BankServices.Admin.Queries;
using IdentityServer3.Core.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace BankServices.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]/[action]")]
    public class BankServicesAdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;

        public BankServicesAdminController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config;
        }

        
        [HttpGet]       
        public async Task<IActionResult> GetAllBankAccounts()
        {
            try
            {
                var response = await _mediator.Send(new GetAllBankAccountsQuery());

                return Ok(response);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        // Money Cheat
        [HttpPost]
        public async Task<IActionResult> AddMoney([FromQuery]Guid id, double amount)
        {
            
            try
            {
                var response = await _mediator.Send(new AddMoneyCommand() 
                {
                    Id = id,
                    Amount = amount
                });
                return Ok(response);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchUsers([FromQuery] string key)
        {

            //Engine yazıp proje çalıştığında Sql Server'daki bilgileri Redis'e yazıp ardından search'u rediste yapmak gerekiyor.
            try
            {
                var response = await _mediator.Send(new SearchUserQuery()
                {
                    Key = key
                });


                return Ok(response);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
