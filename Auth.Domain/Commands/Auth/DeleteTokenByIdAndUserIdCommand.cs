using FluentValidation.Results;
using MediatR;

namespace Auth.Domain.Commands.Auth
{
	public class DeleteTokenByIdAndUserIdCommand:IRequest<ValidationResult>
	{
		public DeleteTokenByIdAndUserIdCommand(Guid id, Guid userId)
		{
			Id = id;
			UserId = userId;
		}

		public Guid Id { get; set; }
		public Guid UserId { get; set; }
	}
}
