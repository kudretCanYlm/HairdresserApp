using Grpc.Auth;
using Grpc.Auth.ClientServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Application.Dto.Address;
using User.Application.Dto.User;
using User.Application.Interfaces.Address;
using User.Application.Interfaces.User;

namespace User.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IUserAppService _userAppService;
		private readonly IAddressAppService _addressAppService;
		private readonly AuthGrpcService _authGrpcService;

		public UserController(IUserAppService userAppService, IAddressAppService addressAppService, AuthGrpcService authGrpcService)
		{
			_userAppService = userAppService;
			_addressAppService = addressAppService;
			_authGrpcService = authGrpcService;
		}

		[HttpPost, Route("Register"), AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
		{
			var result = await _userAppService.CreateAsync(createUserDto);
			
			if (result.IsValid)
				return Ok(result);

			return BadRequest(result);

		}

		[HttpPost,Route("Login"),AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
			var userId = await _userAppService.Login(loginDto);

			if (userId == null)
				return BadRequest("User not found");

			var token = await _authGrpcService.CreateNewUserToken((Guid)userId);
			
			return Ok(token);
		}
		//test et
		[HttpGet,Route("CheckUserAddress")]
		public async Task<IActionResult> CheckUserAddress()
		{
			var userId = GrpcAuthExtension.GetCurrentUserId(HttpContext);

			var result = await _addressAppService.CheckUserAddressByUserId((Guid)userId);

			return Ok(result);
		}
		
		[HttpPost,Route("AddUserAddress")]
		public async Task<IActionResult> AddUserAddress([FromBody] CreateAddressDto createAddressDto)
		{
			createAddressDto.UserId= (Guid)GrpcAuthExtension.GetCurrentUserId(HttpContext);

			var result = await _addressAppService.Create(createAddressDto);

			if (result.IsValid)
				return Ok(result);

			return BadRequest(result);
		}
		
		[HttpGet,Route("GetAllUserAddresses")]
		public async Task<IActionResult> GetAllUserAddresses()
		{
			var userId = GrpcAuthExtension.GetCurrentUserId(HttpContext);

			var result = await _addressAppService.GetAllByUserId((Guid)userId);

			return Ok(result);
		}

		//buraya adres seçme ekle kullanıcının kayıtlı adreserinden birini seçebilir
	}
}
