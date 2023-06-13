using Media.Application.Dto;
using Media.Application.Interfaces.Media;
using Media.Domain.Queries.Media;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Media.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ImageController : ControllerBase
	{
		private readonly IMediaAppService mediaAppService;
		private readonly IMediator mediator;

		public ImageController(IMediaAppService mediaAppService, IMediator mediator)
		{
			this.mediaAppService = mediaAppService;
			this.mediator = mediator;
		}

		[HttpPost,Route("addMedia"),AllowAnonymous]
		public async Task<IActionResult> AddMedia([FromBody] CreateMediaDto mediaDto)
		{
			var result = await mediaAppService.CreateAsync(mediaDto);

			return Ok(result);
		}

		[HttpGet,Route("getMedias")]
		public async Task<IActionResult> GetMedias(Guid ownerId)
		{
			var medias = await mediaAppService.GetAllByOwnerIdAsync(ownerId);

			return Ok(medias);
		}
		
		[HttpGet,Route("getMedia")]
		public async Task<IActionResult> GetMedia(Guid ownerId,string type)
		{
			var medias = await mediaAppService.GetByOwnerIdAndImageType(ownerId,type);

			return Ok(medias);
		}

		[HttpGet,Route("testget"),AllowAnonymous]
		public async Task<IActionResult> GetTest(Guid ownerId)
		{
			await mediator.Send(new GetAllMediasQuery(ownerId));

			return Ok();
		}

	}
}
