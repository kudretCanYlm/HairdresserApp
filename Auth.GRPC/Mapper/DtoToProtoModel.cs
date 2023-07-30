using Auth.Application.Dto.Auth;
using AutoMapper;
using Grpc.Auth.Protos;
namespace Auth.GRPC.Mapper
{
	public class DtoToProtoModel : Profile
	{
		public DtoToProtoModel()
		{
			CreateMap<UserAuthSessionDto, GetTokensForReviewResponse>()
				.ForMember(x => x.TokenId, x => x.MapFrom(x => x.Id.ToString()))
				.ForMember(x => x.TokenTop10, x => x.MapFrom(x => x.Token.Substring(0, 10)))
				.ForMember(x => x.TokenExpiringTime, x => x.MapFrom(x => x.TokenExpiringTime.ToString()));
		}
	}
}
