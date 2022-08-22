using Authentication.Commands;
using Authentication.Models.IdentityModels;
using Authentication.Models.RegisterModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {

            ResponseViewModel responseViewModel = new ResponseViewModel();

            try
            {
                if (!ModelState.IsValid)
                {
                    responseViewModel.IsSuccess = false;
                    responseViewModel.Message = "Given Credentials are missing or incorrect!!!";

                    return BadRequest(responseViewModel);
                }

                var response = await _mediator.Send(new RegisterCommand()
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword
                });

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(new ResponseViewModel()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                });
            }      
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var response = new ResponseViewModel();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message =  "Given Credentials are missing or incorrect!!!";
                    return BadRequest(response);
                }

                response = await _mediator.Send(new LoginCommand()
                {
                    Email = model.Email,
                    Password = model.Password,
                });

                if (!response.IsSuccess)
                {
                    return Unauthorized();
                }

                return Ok(response);
            }
            catch(Exception ex)
            {

            }

            return Ok();
        }



    }
}
