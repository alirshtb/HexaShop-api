using HexaShop.ApiEndPoint.Models.Dtos.IdentityDtos;
using HexaShop.Common.Exceptions;
using HexaShop.Common;
using HexaShop.EndPoint.Models.ViewModels.AccountController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using HexaShop.ApiEndPoint.DynamicAuthorization.JWT;
using MediatR;
using Microsoft.AspNetCore.Identity;
using HexaShop.Domain;
using HexaShop.Common.CommonDtos;
using HexaShop.Application.Features.AppUserFeatures.Requests.Commands;
using HexaShop.Application.Dtos.AppUserDtos.Commands;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Common.CommonExtenstionMethods;

namespace HexaShop.EndPoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {

        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public UserAccountController(UserManager<AppIdentityUser> userManager,
                                 SignInManager<AppIdentityUser> signInManager,
                                 IMediator mediator, 
                                 IMapper mapper, 
                                 IJWTService jwtService,
                                 IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mediator = mediator;
            _mapper = mapper;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }





        [HttpGet]
        public IActionResult AccessDenied()
        {
            return BadRequest(ApplicationMessages.AccessDenied);
        }

        /// <summary>
        /// sign up user
        /// </summary>
        /// <param name="signUpViewModel"></param>
        /// <returns></returns>
        /// <exception cref="InvalidModelStateException"></exception>
        [HttpPost]
        public async Task<ActionResult<RequestTokenResultDto>> SignUp([FromBody] SignUpViewModel signUpViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new InvalidModelStateException(signUpViewModel, ApplicationMessages.InValidInformation);
                }

                // --- if user with current email doesn't exists --- //
                if (await IsUserExists(signUpViewModel.Email) == false)
                {
                    // --- create hexa identity user --- //
                    var createIdentityUserResult = await CreateUser(signUpViewModel);


                    if (!createIdentityUserResult.Succeeded)
                    {
                        throw new Exception(createIdentityUserResult.Errors.Select(e => e.Description).ToString());
                    }

                    // --- if hexa identity user creation is succeeded --- //
                    var createHexaUserResult = await CreateAppUser(signUpViewModel);

                    // --- return result --- //
                    createHexaUserResult.ThrowException<int>();

                    // --- request token for user --- //
                    var requestTokenResult = _jwtService.CreateTokenAsync(new RequestTokenDto()
                    {
                        UserName = signUpViewModel.Email,
                        Password = signUpViewModel.Password
                    });

                    return Ok(requestTokenResult);

                }
                else
                {

                    var appUser = await _unitOfWork.AppUserRepository.GetAsync(signUpViewModel.Email);

                    if (appUser != null)
                    {
                        throw new Exception(ApplicationMessages.DuplicateEmail);
                    }

                    var createHexaUserResult = await CreateAppUser(signUpViewModel);

                    // --- return result --- //
                    createHexaUserResult.ThrowException<int>();

                    // --- request token for user --- //
                    var requestTokenResult = await _jwtService.CreateTokenAsync(new RequestTokenDto()
                    {
                        UserName = signUpViewModel.Email,
                        Password = signUpViewModel.Password
                    });

                    return Ok(requestTokenResult);

                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// sign in user
        /// </summary>
        /// <param name="signInViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInViewModel signInViewModel)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(signInViewModel.UserName);

                if (user == null)
                {
                    throw new NotFoundException(ApplicationMessages.InValidInformation);
                }

                if (!user.IsActive)
                {
                    throw new Exception(ApplicationMessages.UserIsNotActive);
                }

                if (!await IsUserPasswordCurrect(user, signInViewModel.Password))
                {
                    throw new InvalidModelStateException(signInViewModel, ApplicationMessages.InValidInformation);
                }

                await _signInManager.SignInAsync(user, signInViewModel.RememberMe);


                // --- request token for user --- //
                var requestTokenResult = await _jwtService.CreateTokenAsync(new RequestTokenDto()
                {
                    UserName = signInViewModel.UserName,
                    Password = signInViewModel.Password
                });

                return Ok(requestTokenResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// change user actvity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ChangeUserActivity(string id)
        {
            try
            {
                var appIdentityUser = await _userManager.FindByIdAsync(id);
                if (appIdentityUser == null)
                {
                    throw new NotFoundException(ApplicationMessages.UserNotFound);
                }

                appIdentityUser.IsActive = !appIdentityUser.IsActive;

                await _userManager.UpdateAsync(appIdentityUser);

                var activityMessage = appIdentityUser.IsActive == true ? "فعال" : "غیر فعال";
                var message = string.Format(ApplicationMessages.ChangeUserActivity, activityMessage);

                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        #region Private Methods 

        /// <summary>
        /// check user password.
        /// </summary>
        /// <param name="hexaIdentityUser"></param>
        /// <param name="password"></param>
        /// <returns>true if user password is currect.</returns>
        private async Task<bool> IsUserPasswordCurrect(AppIdentityUser hexaIdentityUser, string password)
        {
            var checkResult = await _signInManager.CheckPasswordSignInAsync(hexaIdentityUser, password, false);

            return checkResult.Succeeded;
        }

        /// <summary>
        /// check That User Exist Or Not
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private async Task<bool> IsUserExists(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        /// <summary>
        /// create user Account
        /// </summary>
        /// <param name="signUpViewModel"></param>
        /// <returns></returns>
        private async Task<IdentityResult> CreateUser(SignUpViewModel signUpViewModel)
        {


            var appIdentityUser = new AppIdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = signUpViewModel.Email,
                FirstName = signUpViewModel.FirstName,
                LastName = signUpViewModel.LastName,
                UserName = signUpViewModel.Email,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                NormalizedEmail = signUpViewModel.Email.ToUpper(),
                NormalizedUserName = signUpViewModel.Email.ToUpper(),
            };

            var passwordHasher = new PasswordHasher<AppIdentityUser>();
            appIdentityUser.PasswordHash = passwordHasher.HashPassword(appIdentityUser, signUpViewModel.Password);

            var createResult = await _userManager.CreateAsync(appIdentityUser);

            return createResult;

        }

        /// <summary>
        /// Create app user
        /// </summary>
        /// <param name="signUpViewModel"></param>
        /// <returns></returns>
        private async Task<ResultDto<int>> CreateAppUser(SignUpViewModel signUpViewModel)
        {
            var appIdentityUser = await _userManager.FindByEmailAsync(signUpViewModel.Email);

            // --- create hexa user and set hexa identity userid --- //
            var createHexaUserCommandRequest = new CreateAppUserCR()
            {
                CreateAppUserDto = _mapper.Map<CreateAppUserDto>(signUpViewModel),
                AppIdentityUserId = appIdentityUser.Id
            };

            var createAppUserResult = await _mediator.Send(createHexaUserCommandRequest);

            return createAppUserResult;
        }

        #endregion Private Methods

    }
}
