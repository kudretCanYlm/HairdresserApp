﻿using MediatR;
using User.Domain.Interfaces;
using User.Domain.Models;

namespace User.Domain.Queries.Address
{
	public class AddressQueryHandler : IRequestHandler<GetAllUserAddressesQuery, IEnumerable<AddressModel>>,
										IRequestHandler<GetUserAddressByIdQuery, AddressModel>,
										IRequestHandler<GetUserAddressesByUserId,IEnumerable<AddressModel>>
	{
		private readonly IAddressRepository _addressRepository;

		public AddressQueryHandler(IAddressRepository addressRepository)
		{
			_addressRepository = addressRepository;
		}

		public async Task<IEnumerable<AddressModel>> Handle(GetAllUserAddressesQuery request, CancellationToken cancellationToken)
		{
			var userAddresses = await _addressRepository.GetAll();
			return userAddresses;
		}

		public async Task<AddressModel> Handle(GetUserAddressByIdQuery request, CancellationToken cancellationToken)
		{
			var userAddress = await _addressRepository.GetById(request.Id);
			return userAddress;
		}

		public async Task<IEnumerable<AddressModel>> Handle(GetUserAddressesByUserId request, CancellationToken cancellationToken)
		{
			var userAddress = await _addressRepository.GetAllByUserId(request.UserId);
			return userAddress;
		}
	}
}
