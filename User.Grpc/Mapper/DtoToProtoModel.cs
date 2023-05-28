using AutoMapper;
using Grpc.User.Protos;
using User.Application.Dto.Address;

namespace User.GRPC.Mapper
{
	public class DtoToProtoModel:Profile
	{
		public DtoToProtoModel()
		{
			CreateMap<AddressDto, UserAddress>()
					.ForMember(x => x.Id, x => x.MapFrom(x => x.Id.ToString()));

		}
	}
}
