using AutoMapper;
using Events.User.Address;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using User.Domain.Interfaces;
using User.Domain.Models;

namespace User.Domain.Commands.Address
{
    //will addvaldation middleware
    public class AddressCommandHandler : CommandHandler,
                                        IRequestHandler<CreateUserAddressCommand, ValidationResult>,
                                        IRequestHandler<UpdateUserAddressCommand, ValidationResult>,
                                        IRequestHandler<DeleteUserAddressCommand, ValidationResult>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public AddressCommandHandler(IAddressRepository addressRepository, IUserRepository userRepository, IMapper mapper)
        {
            this.addressRepository = addressRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<ValidationResult> Handle(CreateUserAddressCommand request, CancellationToken cancellationToken)
        {
            var userAddress = mapper.Map<AddressModel>(request);

            if (!await userRepository.IsUserExist(request.UserId))
            {
                AddError("the user not exist");
                return ValidationResult;
            }

            userAddress.AddDomainEvent(mapper.Map<UserAddressCreatedEvent>(userAddress));

            addressRepository.Add(userAddress);

            return await Commit(addressRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateUserAddressCommand request, CancellationToken cancellationToken)
        {
            var userAddress = await addressRepository.GetById(request.Id);

            if (userAddress == null)
            {
                AddError("address not found");
                return ValidationResult;
            }

            userAddress = mapper.Map(request, userAddress);
            userAddress.AddDomainEvent(mapper.Map<UserAddressUpdatedEvent>(userAddress));

            addressRepository.Update(userAddress);

            return await Commit(addressRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(DeleteUserAddressCommand request, CancellationToken cancellationToken)
        {
            var userAddress = await addressRepository.GetById(request.Id);

            if (userAddress is null)
            {
                AddError("The address doesn't exists.");
                return ValidationResult;
            }

            userAddress.AddDomainEvent(mapper.Map<UserAddressDeletedEvent>(userAddress));

            addressRepository.Delete(userAddress);

            return await Commit(addressRepository.UnitOfWork);
        }
    }
}
