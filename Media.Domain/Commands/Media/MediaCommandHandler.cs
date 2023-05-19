using AutoMapper;
using Events.Media;
using FluentValidation.Results;
using Media.Domain.Interfaces;
using Media.Domain.Models;
using MediatR;
using NetDevPack.Messaging;

namespace Media.Domain.Commands.Media
{
	public class MediaCommandHandler : CommandHandler,
										IRequestHandler<CreateMediaCommand, ValidationResult>,
										IRequestHandler<DeleteMediaCommand, ValidationResult>,
										IRequestHandler<UpdateMediaCommand, ValidationResult>
	{
		private readonly IMediaRepository mediaRepository;
		private readonly IMapper mapper;

		public MediaCommandHandler(IMediaRepository mediaRepository, IMapper mapper)
		{
			this.mediaRepository = mediaRepository;
			this.mapper = mapper;
		}

		public async Task<ValidationResult> Handle(CreateMediaCommand request, CancellationToken cancellationToken)
		{
			var media = mapper.Map<MediaModel>(request);

			mediaRepository.Add(media);

			media.AddDomainEvent(mapper.Map<MediaCreatedEvent>(media));

			return await Commit(mediaRepository.UnitOfWork);
		}

		public async Task<ValidationResult> Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
		{
			var media = await mediaRepository.GetById(request.Id);

			if (media is null)
			{
				AddError("The media doesn't exists.");
				return ValidationResult;
			}

			media.AddDomainEvent(mapper.Map<MediaDeletedEvent>(media));

			mediaRepository.Delete(media);

			return await Commit(mediaRepository.UnitOfWork);
		}

		public async Task<ValidationResult> Handle(UpdateMediaCommand request, CancellationToken cancellationToken)
		{
			var media = await mediaRepository.GetById(request.Id);

			if (media == null)
			{
				AddError("media not found");
				return ValidationResult;
			}

			media = mapper.Map(request, media);

			media.AddDomainEvent(mapper.Map<MediaUpdatedEvent>(media));

			mediaRepository.Update(media);

			return await Commit(mediaRepository.UnitOfWork);
		}
	}
}
