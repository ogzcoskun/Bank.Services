using BankServices.Client.Commands;
using BankServices.Client.Data;
using BankServices.Client.Models;
using BankServices.Client.OutModels;
using BankServices.Client.Queries;
using BankServices.Client.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Client.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class MoneyTransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AuthContext _context;
        private readonly IUserService _userService;

        public MoneyTransactionController(IMediator mediator, AuthContext context, IUserService userService)
        {
            _mediator = mediator;
            _context = context;
            _userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> SendMoney([FromBody] MoneyTransactionModel model)
        {
            try
            {
                // Check if the token Id and Users Id match(Muthis hareket)
                var accessToken =  Request.Headers[HeaderNames.Authorization].ToString();
                accessToken = accessToken.Split(" ")[1]; // Gives the token without "Bearer"  

                var userSend =  _context.AspNetUserTokens.Where(x => x.UserId.Contains(model.UserId)).ToList();

                if (userSend[0].Value.ToString() == accessToken)
                {
                    var response = await _mediator.Send(new SendMoneyCommand()
                    {
                        Amount = model.Amount,
                        UserId = model.UserId,
                        ToSendId = model.ToSendId,
                    });

                    return Ok(response);
                }
                else
                {
                    return Ok(new ServiceResponse()
                    {
                        Success = false,
                        Message = "Given user id and token doesn't match!!!",
                    });
                }    
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }     
        }

        [HttpGet]

        public async Task<IActionResult> GetAmount()
        {

            try
            {
                // Check if the token Id and Users Id match(Muthis hareket)
                var accessToken = Request.Headers[HeaderNames.Authorization].ToString();
                var UserId = await _userService.GetUserId(accessToken);

                
                    var response = await _mediator.Send(new GetAmountQuery()
                    {
                        Id = UserId
                    });

                    return Ok(response); 
                    
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAmountInForeignCurrency([FromQuery] string currency)
        {

            //Learned From My Mistakes not gonna get the id from request i'll get the id directly with the access token(Bir de böbürlendim :( )
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString();
            var UserId = await _userService.GetUserId(accessToken);

            var response = await _mediator.Send(new GetAmountInDifferentCurrencyQuery() 
            { 
                Id = UserId,
                Currency = currency,
            });

            return Ok(response);
        }

    }
}
