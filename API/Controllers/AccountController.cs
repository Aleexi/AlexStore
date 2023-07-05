
using System.Security.Claims;
using API.DTO;
using API.Errors;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : SuperController
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        
        private readonly InterfaceTokenService tokenService;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager, InterfaceTokenService tokenService, IMapper mapper)
        {
            this.mapper = mapper;
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetLoggedInUser() 
        {
            /* Get email from JWToken Claims, Email field */
            var email = User.FindFirstValue(ClaimTypes.Email);

            /* Find the user logged in */
            var user = await this.userManager.FindByEmailAsync(email);

            return new UserDTO {
                Email = user.Email,
                DisplayName = user.DisplayName,
                JWToken = this.tokenService.CreateToken(user)
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<Boolean>> CheckEmail([FromQuery] string email)
        {
            return await this.userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<AddressDTO> GetAddress() 
        {
            /* Get email from JWToken */
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await this.userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);

            return this.mapper.Map<Address, AddressDTO>(user.Address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO address)
        {
            /* Get email from JWToken */
            var email = User.FindFirstValue(ClaimTypes.Email);

            /* Find user in database together with the address */
            var user = await this.userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);

            /* Update the Address of the user locally */
            user.Address = this.mapper.Map<AddressDTO, Address>(address);

            var updateResult = await this.userManager.UpdateAsync(user);

            if (updateResult.Succeeded) {
                /* Returns the newly updated address of the user in AddressDTO format,
                   if it was updated successfully                                   */
                return Ok(this.mapper.Map<Address, AddressDTO>(user.Address));
            }
            
            return BadRequest("Couldn't update the Address of the user");
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO) 
        {
            var user = await this.userManager.FindByEmailAsync(loginDTO.Email);

            /* If user is not found */
            if (user == null) {
                return Unauthorized(new ApiResponse(401));
            }

            /* If user was found, sign in the user if Email & Password combination is correct */ 
            var signInResult = await this.signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (!signInResult.Succeeded) 
            {
                return Unauthorized(new ApiResponse(401));
            }

            /* If login information was correct */
            return new UserDTO {
                Email = user.Email,
                DisplayName = user.DisplayName,
                JWToken = this.tokenService.CreateToken(user)
            };
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var userExist = await this.userManager.FindByEmailAsync(registerDTO.Email);

            /* If user wants to register an Email that is already in use */
            if (userExist != null) {
                return new BadRequestObjectResult(new ApiValidationErrorResponse{
                    Errors = new []{"Email Address Is In Use Already"}});
            }

            var user = new AppUser
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.Email
            };

            var createUserResult = await this.userManager.CreateAsync(user, registerDTO.Password);

            if (!createUserResult.Succeeded) {
                return BadRequest(new ApiResponse(400));
            }

            return new UserDTO {
                Email = user.Email,
                DisplayName = user.DisplayName,
                JWToken = this.tokenService.CreateToken(user)
            };
        }
    }
}